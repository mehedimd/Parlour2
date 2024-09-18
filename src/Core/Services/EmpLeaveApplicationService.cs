using AutoMapper;
using Domain.Entities;
using Domain.Enums.AppEnums;
using Domain.Utility;
using Domain.Utility.Common;
using Domain.ViewModel.EmpLeaveApplication;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;
using System.Transactions;
using DU = Domain.Utility;

namespace Services
{
    public class EmpLeaveApplicationService : BaseService<EmpLeaveApplication>, IEmpLeaveApplicationService
    {
        #region Config
        private readonly IEmpLeaveApplicationRepository Repository;
        private readonly IEmpAttendanceRepository _iEmpAttendanceRepository;
        private readonly IEmployeeRepository _iEmployeeRepository;
        private readonly ILeaveTypeRepository _iLeaveTypeRepository;
        private readonly ILeaveSetupRepository _iLeaveSetupRepository;
        private readonly ILeaveCfRepository _iLeaveCfRepository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public EmpLeaveApplicationService(IEmpLeaveApplicationRepository iRepository, IMapper iMapper,
            IUnitOfWork iUnitOfWork, IEmployeeRepository iEmployeeRepository, ILeaveTypeRepository iLeaveTypeRepository,
            IEmpAttendanceRepository iEmpAttendanceRepository, ILeaveSetupRepository iLeaveSetupRepository, ILeaveCfRepository iLeaveCfRepository) : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
            _iEmpAttendanceRepository = iEmpAttendanceRepository;
            _iEmployeeRepository = iEmployeeRepository;
            _iLeaveTypeRepository = iLeaveTypeRepository;
            _iLeaveSetupRepository = iLeaveSetupRepository;
            _iLeaveCfRepository = iLeaveCfRepository;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm>> SearchAsync(DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm> model)
        {
            var dataList = await Repository.SearchAsync(model);
            return dataList;
        }

        #endregion

        #region LeaveAdd

        public async Task<bool> LeaveAddAsync(EmpLeaveApplicationVm vm)
        {
            var model = _iMapper.Map<EmpLeaveApplication>(vm);

            model.Status = (short)EmpLeaveAppStatusEnum.Approve;
            model.FromDate = (DateTime)(!string.IsNullOrEmpty(vm.FromDateStr) ? DU.Utility.ConvertStrToDate(vm.FromDateStr) : model.FromDate);
            model.ToDate = (DateTime)(!string.IsNullOrEmpty(vm.ToDateStr) ? DU.Utility.ConvertStrToDate(vm.ToDateStr) : model.ToDate);

            model.ApproveLength = vm.Length;
            model.SubmitById = CurrentUserId;
            model.SubmitDate = DU.Utility.GetBdDateTimeNow();

            model.ReviewById = CurrentUserId;
            model.ReviewDate = DU.Utility.GetBdDateTimeNow();

            model.ApprovedById = CurrentUserId;
            model.ApproveDate = DU.Utility.GetBdDateTimeNow();

            model.ActionById = CurrentUserId;
            model.ActionDate = DU.Utility.GetBdDateTimeNow();

            var leaveDates = DateRange(model.FromDate, model.ToDate);

            var empLeaves = new List<EmpAttendance>();
            var updateEmpLeaves = new List<EmpAttendance>();

            if (leaveDates.Count() > 0)
            {
                foreach (var leave in leaveDates)
                {
                    var existAtt = _iEmpAttendanceRepository.GetFirstOrDefault(c => c.EmployeeId == model.EmployeeId && c.AttendDate.Date == leave.Date);

                    if (existAtt != null && (existAtt.Status == EmpAttendanceStatus.Present || existAtt.Status == EmpAttendanceStatus.Leave)) continue;

                    if (existAtt != null && (existAtt.Status == EmpAttendanceStatus.Absent || existAtt.Status == EmpAttendanceStatus.Holiday || existAtt.Status == EmpAttendanceStatus.Offday))
                    {
                        existAtt.Status = EmpAttendanceStatus.Leave;
                        updateEmpLeaves.Add(existAtt);
                        continue;
                    }

                    var leaveModel = new EmpAttendance();

                    leaveModel.AttendDate = leave;
                    leaveModel.Status = EmpAttendanceStatus.Leave;
                    leaveModel.EmployeeId = model.EmployeeId;
                    leaveModel.ActionById = CurrentUserId;
                    leaveModel.ActionDate = DU.Utility.GetBdDateTimeNow();

                    empLeaves.Add(leaveModel);
                }
            }

            if (empLeaves.Count == 0 && updateEmpLeaves.Count == 0) throw new Exception("Leave Already Added..!!");

            var leaveTypeSetup = await _iLeaveSetupRepository.GetFirstOrDefaultAsync(c => c.LeaveTypeId == model.LeaveTypeId);
            if (leaveTypeSetup == null) throw new Exception("Leave Setup Not Found..!!");

            LeaveCf earnLeave = null;

            if (leaveTypeSetup.IsCarryForward)
            {
                var empCf = await _iLeaveCfRepository.GetFirstOrDefaultAsync(c => c.EmployeeId == model.EmployeeId && c.LeaveYear == model.SubmitDate.Year);
                if (empCf == null) throw new Exception("Employee Earn Leave Setup Not Found...!");

                empCf.LeaveEnjoyed = empCf.LeaveEnjoyed + model.Length;

                earnLeave = empCf;
            }

            using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await Repository.AddAsync(model);
            if (earnLeave != null) await _iLeaveCfRepository.UpdateAsync(earnLeave);
            if (empLeaves.Count > 0) await _iEmpAttendanceRepository.AddRangeAsync(empLeaves);
            if (updateEmpLeaves.Count > 0) await _iEmpAttendanceRepository.UpdateRangeAsync(updateEmpLeaves);
            var isAdded = await _iUnitOfWork.CompleteAsync();
            if (!isAdded) return false;
            ts.Complete();
            return true;
        }

        #endregion

        #region GetEmployeeAppData

        public async Task<EmpLeaveApplicationVm> GetEmployeeAppDataAsync(long id)
        {
            var data = await Repository.GetEmployeeAppByIdAsync(id);
            return data;
        }

        #endregion

        #region GetLeaveReportData

        public async Task<List<EmpLeaveReportVm>> GetLeaveReportData(EmpLeaveReportVm vm)
        {
            var data = await Repository.GetLeaveReportData(vm);
            return data;
        }

        #endregion

        #region ReportHtml

        public async Task<string> LeaveReportHtml(EmpLeaveReportVm vm)
        {
            var fullHtml = "";
            var data = await Repository.GetLeaveReportData(vm);

            fullHtml += "<table class='table table-bordered' id='LeaveReportTablePrint' style='width:100%;text-align: center'>";

            fullHtml += "<thead>";
            fullHtml += "<tr>";
            fullHtml += "<th style='width:3%'>Sl.</th>";
            fullHtml += "<th style='width:10%'>Employee </th>";
            fullHtml += "<th style='width:10%'>Department</th>";
            fullHtml += "<th style='width:10%'>Designation</th>";
            fullHtml += "<th style='width:10%'>Join Date</th>";
            fullHtml += "<th style='width:8%'>Casual Leave</th>";
            fullHtml += "<th style='width:8%'>Medical Leave</th>";
            fullHtml += "<th style='width:8%'>Maturnity Leave</th>";
            fullHtml += "<th style='width:8%'>Earn Leave</th>";
            fullHtml += "<th style='width:9%'>Leave Without Pay</th>";
            fullHtml += "<th style='width:8%'>Study Leave</th>";
            fullHtml += "<th style='width:8%'>Total Leave</th>";
            fullHtml += "</tr>";

            fullHtml += "</thead>";

            if (data.Count > 0)
            {
                foreach (var (v, i) in data.GetItemWithIndex())
                {
                    fullHtml += "<tr>";

                    fullHtml += $@"<td>{i + 1}</td>";
                    fullHtml += $@"<td class='text-start'><b>{v.EmployeeName}</b><br/>{v.EmployeeCode}</td>";
                    fullHtml += $@"<td class='text-center'>{v.EmpDepartment}</td>";
                    fullHtml += $@"<td class='text-center'>{v.EmpDesignation}</td>";
                    fullHtml += $@"<td class='text-center'>{v.EmpJoinDate.ToString("d")}</td>";
                    fullHtml += $@"<td class='text-center'>{v.CasualLeave}</td>";
                    fullHtml += $@"<td class='text-center'>{v.MedicalLeave}</ td >";
                    fullHtml += $@"<td class='text-center'>{v.MaturnityLeave}</ td >";
                    fullHtml += $@"<td class='text-center'>{v.EarnLeave}</ td >";
                    fullHtml += $@"<td class='text-center'>{v.LeaveWithoutPay}</ td >";
                    fullHtml += $@"<td class='text-center'>{v.StudyLeave}</ td >";
                    fullHtml += $@"<td class='text-center'>{v.TotalLeave}</ td >";

                    fullHtml += "</tr>";
                }
            }

            fullHtml += "</table>";

            return fullHtml;
        }

        public async Task<string> EmpLeaveStatementHtml(EmpLeaveBalanceReportVm model)
        {
            if (model.EmployeeId == 0) throw new Exception("EmployeeId Not Found..!");
            var result = await EmployeeWiseLeaveBalance(model.EmployeeId, model.SelectYear);

            var fullHtml = "";
            fullHtml += "<div style='padding-bottom: 10px;'>";

            fullHtml += $@"<table>
                                <tbody>
                                    <tr>
                                        <td style='width:10%; font-size:12px'><b>Name : </b></td>
                                        <td style='width:15%; font-size:12px'>{result.EmployeeName}</td>
                                        <td style='width:10%; padding-top:5px; font-size:12px'><b>Designation : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{result.EmpDesignation}</td>
                                        <td style='width:10%; padding-top:5px; font-size:12px'><b>Status : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{result.EmployeeStatusText}</td>
                                        <td style='width:10%; padding-top:5px; font-size:12px'><b>Join Date : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{DU.Utility.ConvertDateToStr(result.JoinDate)}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:10%; font-size:12px'><b>Code : </b></td>
                                        <td style='width:15%; font-size:12px'>{result.Code}</td>
                                        <td style='width:10%; padding-top:5px; font-size:12px'><b>Department : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{result.EmpDepartment}</td>
                                        <td style='width:10%; padding-top:5px; font-size:12px'><b>Leave Year : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{DateTime.Now.Year}</td>
                                        <td colspan='1' style='width:10%; padding-top:5px; font-size:12px'><b>Report Date : </b></td>
                                        <td style='width:15%; padding-top:5px; font-size:12px'>{DU.Utility.ConvertDateToStr(DateTime.Now)}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:10%; font-size:12px'><b>Gender : </b></td>
                                        <td style='width:15%; font-size:12px'>{result.GenderText}</td>
                                    </tr>
                                </tbody>
                            </table>";

            fullHtml += "</div>";

            fullHtml += "<table class='table table-bordered' style='width:100%;text-align:center' style='repeat-header:yes;'>";

            fullHtml += "<thead>";
            fullHtml += "<tr>";
            fullHtml += "<th style='width:5%'>Sl.</th>";
            fullHtml += "<th style='width:20%'>Leave Type</th>";
            fullHtml += "<th style='width:20%'>Current Year Balance</th>";
            fullHtml += "<th style='width:10%'>Carry Forward</th>";
            fullHtml += "<th style='width:10%'>Total</th>";
            fullHtml += "<th style='width:10%'>Taken</th>";
            fullHtml += "<th style='width:10%'>Remaining</th>";
            fullHtml += "</tr>";

            fullHtml += "</thead>";
            var totalLeaveBalance = 0;
            var totalLeaveTaken = 0;
            var totalLeaveRemain = 0;
            var totalCFBalance = 0;
            var totalSum = 0;
            fullHtml += "<tbody>";
            var i = 1;
            if (result.LeaveBalanceVms.Count > 0)
            {
                foreach (var v in result.LeaveBalanceVms)
                {
                    fullHtml += "<tr>";
                    fullHtml += $@"<td class='text-center'>{i++}</td>";
                    fullHtml += $@"<td class='text-start'>{v.LeaveTypeName}</td>";
                    fullHtml += $@"<td class='text-end'>{v.LeaveBalance}</td>";
                    fullHtml += $@"<td class='text-end'>{v.CarryForwardBalance}</td>";
                    fullHtml += $@"<td class='text-end'>{(v.LeaveBalance + v.CarryForwardBalance)}</td>";
                    fullHtml += $@"<td class='text-end'>{v.LeaveTaken}</td>";
                    fullHtml += $@"<td class='text-end'>{v.LeaveRemain}</td>";

                    fullHtml += "</tr>";
                    totalLeaveBalance += v.LeaveBalance;
                    totalCFBalance += v.CarryForwardBalance;
                    totalSum = totalSum + (v.LeaveBalance + v.CarryForwardBalance);
                    totalLeaveTaken += v.LeaveTaken;
                    totalLeaveRemain += v.LeaveRemain;
                }
            }

            fullHtml += "</tbody>";

            fullHtml += "<tfoot>";
            fullHtml += "<tr class='text-align: center'>";
            fullHtml += "<th colspan='2' class='text-end'>Total</th>";
            fullHtml += $@"<td class='text-end'><b>{totalLeaveBalance}</b></td>";
            fullHtml += $@"<td class='text-end'><b>{totalCFBalance}</b></td>";
            fullHtml += $@"<td class='text-end'><b>{totalSum}</b></td>";
            fullHtml += $@"<td class='text-end'><b>{totalLeaveTaken}</b></td>";
            fullHtml += $@"<td class='text-end'><b>{totalLeaveRemain}</b></td>";
            fullHtml += "</tr>";
            fullHtml += "</tfoot>";

            fullHtml += "</table>";

            return fullHtml;
        }

        #endregion

        #region DateRange

        private IEnumerable<DateTime> DateRange(DateTime startingDate, DateTime endingDate)
        {
            if (endingDate < startingDate)
            {
                throw new ArgumentException("endingDate should be after startingDate");
            }
            var ts = endingDate - startingDate;
            for (int i = 0; i <= ts.TotalDays; i++)
            {
                yield return startingDate.AddDays(i);
            }
        }

        #endregion

        #region EmployeeWiseLeaveBalance

        public async Task<EmpLeaveBalanceReportVm> EmployeeWiseLeaveBalance(long employeeId, int? searchYear)
        {
            var employee = await _iEmployeeRepository.GetEmployeeByIdAsync(employeeId);
            if (!employee.IsEnable) throw new Exception("Not Current Employee..!");
            if (employee == null) throw new Exception("Employee Not Found..!");

            var model = new EmpLeaveBalanceReportVm();
            model.EmployeeId = employeeId;
            model.EmployeeName = employee.Name.ToUpper();
            model.EmpDesignation = employee.DesignationName;
            model.EmpDepartment = employee.DepartmentName;
            model.EmpPhotoUrl = employee.PhotoUrl;
            model.Code = employee.Code;
            model.JoinDate = employee.JoinDate;
            model.EmployeeStatus = employee.EmployeeStatus;
            model.Gender = employee.Gender;


            var dtlList = new List<LeaveBalanceVm>();

            var leaveTypeList = _iLeaveTypeRepository.Get(c => !c.IsDeleted).ToList();

            if (employee.Gender == "M")
            {
                leaveTypeList = leaveTypeList.Where(c => c.Gender != "F").ToList();
            }

            if (leaveTypeList.Count > 0)
            {
                foreach (var leaveType in leaveTypeList)
                {
                    var leaveModel = new LeaveBalanceVm();
                    leaveModel.LeaveTypeId = leaveType.Id;
                    leaveModel.LeaveTypeName = leaveType.TypeName;

                    var leaveSetup = _iLeaveSetupRepository.GetFirstOrDefault(c => c.LeaveTypeId == leaveType.Id && c.IsActive);
                    if (leaveSetup == null) throw new Exception("Leave Type Setup Not Found..!");


                    var leaveBalance = leaveSetup.LeaveBalance;
                    leaveModel.CurrentYearBalance = leaveSetup.LeaveBalance;

                    if (leaveSetup.IsCarryForward)
                    {
                        var leaveCfSetup = _iLeaveCfRepository.GetFirstOrDefault(c => c.EmployeeId == employee.Id && c.LeaveTypeId == leaveType.Id && c.LeaveYear == searchYear);
                        //if (leaveCfSetup == null) throw new Exception("Leave Carry Forward Setup Not Found..!");
                        if (leaveCfSetup == null) continue;
                        leaveModel.CarryForwardBalance = leaveCfSetup.CfBalance;
                        leaveModel.CurrentYearBalance = leaveCfSetup.LeaveBalance;

                        leaveBalance = (leaveCfSetup.LeaveBalance + leaveModel.CarryForwardBalance);
                    }

                    leaveModel.LeaveBalance = leaveBalance;

                    if (employee.JoinDate.Year == DateTime.Now.Year)
                    {
                        int year = employee.JoinDate.Year;
                        int month = 12;
                        DateTime lastDT = new DateTime(year, month, 1);

                        var monthDiff = AppUtility.YearMonthDiff(employee.JoinDate, DateTime.Now);

                        double leavePerMonth = leaveBalance / month;
                        var totalMonths = monthDiff.Item1;

                        var result = leavePerMonth * totalMonths;

                        leaveModel.LeaveBalance = Convert.ToInt32(Math.Ceiling(result));
                    }

                    var empLeaveAppList = await Repository.GetAsync(c => c.LeaveTypeId == leaveType.Id && c.EmployeeId == employee.Id && c.Status == (short)EmpLeaveAppStatusEnum.Approve);

                    if (searchYear > 0)
                    {
                        empLeaveAppList = empLeaveAppList.Where(c => c.FromDate.Year == searchYear).ToList();
                    }

                    leaveModel.LeaveTaken = empLeaveAppList.Sum(c => c.Length);
                    leaveModel.LeaveRemain = leaveModel.LeaveBalance - leaveModel.LeaveTaken;

                    dtlList.Add(leaveModel);
                }

                model.LeaveBalanceVms = dtlList;
            }

            return model;
        }

        #endregion

        #region GetLeaveSetupBalance

        public async Task<LeaveBalanceVm> GetLeaveSetupBalance(long leaveTypeId, long employeeId)
        {
            var year = DateTime.Now.Year;
            var dataModel = await EmployeeWiseLeaveBalance(employeeId, year);
            var result = dataModel.LeaveBalanceVms.FirstOrDefault(x => x.LeaveTypeId == leaveTypeId);
            return result;
        }

        #endregion

        #region CancelLeave

        public async Task<bool> CancelLeaveApp(long appId)
        {
            if (appId == 0) throw new Exception("Application Id Not Found..!");
            var application = await Repository.GetFirstOrDefaultAsync(c => c.Id == appId && !c.IsDeleted);
            if (application == null) throw new Exception("Application Not Found..!");

            application.Status = (short)EmpLeaveAppStatusEnum.Cancel;

            if (application.FromDate.Date <= DateTime.Now.Date) throw new Exception("Leave Date Is Already Over");
            var leaveDates = DateRange(application.FromDate, application.ToDate);

            var leaveTypeSetup = await _iLeaveSetupRepository.GetFirstOrDefaultAsync(c => c.LeaveTypeId == application.LeaveTypeId);
            if (leaveTypeSetup == null) throw new Exception("Leave Setup Not Found..!!");

            var deleteLeaveList = new List<EmpAttendance>();

            if (leaveDates.Count() > 0)
            {
                foreach (var leave in leaveDates)
                {
                    var existAtt = _iEmpAttendanceRepository.GetFirstOrDefault(c => c.EmployeeId == application.EmployeeId && c.AttendDate.Date == leave.Date);

                    if (existAtt != null && (existAtt.Status == EmpAttendanceStatus.Present || existAtt.Status == EmpAttendanceStatus.Holiday || existAtt.Status == EmpAttendanceStatus.Offday)) continue;

                    deleteLeaveList.Add(existAtt);
                }
            }

            LeaveCf earnLeave = null;

            if (leaveTypeSetup.IsCarryForward)
            {
                var empCf = await _iLeaveCfRepository.GetFirstOrDefaultAsync(c => c.EmployeeId == application.EmployeeId && c.LeaveYear == application.SubmitDate.Year);
                if (empCf == null) throw new Exception("Employee Earn Leave Setup Not Found...!");

                empCf.LeaveEnjoyed = empCf.LeaveEnjoyed - application.Length;

                earnLeave = empCf;
            }

            using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await Repository.UpdateAsync(application);
            if (earnLeave != null) await _iLeaveCfRepository.UpdateAsync(earnLeave);
            if (deleteLeaveList.Count > 0) _iEmpAttendanceRepository.RemoveRange(deleteLeaveList);

            var isUpdated = await _iUnitOfWork.CompleteAsync();
            if (!isUpdated) return false;
            ts.Complete();

            return true;

        }

        #endregion

        #region EmpLeaveWithLeaveType

        public async Task<List<EmpLeaveTypeLeaveReport>> GetEmployeeLeaveDate(long empId, long typeId)
        {
            if (empId == 0 || typeId == 0) throw new Exception("Employee And Type Not Found..");

            var empAppList = await Repository.GetAsync(c => c.LeaveTypeId == typeId && c.EmployeeId == empId && c.Status == (short)EmpLeaveAppStatusEnum.Approve && !c.IsDeleted);

            var dateList = new List<DateTime>();

            if (empAppList.Count > 0)
            {
                foreach (var app in empAppList)
                {
                    var leaveDates = DateRange(app.FromDate, app.ToDate);

                    if (leaveDates.Count() > 0)
                    {
                        foreach (var leave in leaveDates)
                        {
                            dateList.Add(leave);
                        }
                    }
                }
            }

            var data = dateList.Select(c => new EmpLeaveTypeLeaveReport  { LeaveDateStr = c.ToString("d"), DayStr = c.ToString("dddd"), LeaveDate = c }).ToList();
            
            return data;
        }

        #endregion
    }
}
