using AutoMapper;
using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.EmpLeaveApplication;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.CachingUtility;
using Utility.Export;
using WebMVC.Controllers.Base;
using WebMVC.Models.IdentityModels;
using DU = Domain.Utility;

namespace WebMVC.Controllers
{
    public class EmpLeaveApplicationController : AppBaseController
    {
        #region Config

        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _iMapper;
        private readonly IEmpLeaveApplicationService _iService;
        private readonly DropdownService _dropdownService;
        private readonly IEmployeeService _iEmployeeService;
        private readonly IHttpContextAccessor _ihttpContextAccessor;

        public EmpLeaveApplicationController(IUnitOfWork iUnitOfWork, IMapper iMapper, IEmpLeaveApplicationService iService, DropdownService dropdownService, IEmployeeService iEmployeeService, IHttpContextAccessor ihttpContextAccessor) : base(iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            _iMapper = iMapper;
            _iService = iService;
            _dropdownService = dropdownService;
            _iEmployeeService = iEmployeeService;
            _ihttpContextAccessor = ihttpContextAccessor;
        }

        #endregion

        #region Create

        [HttpGet]
        [Authorize(Permissions.EmpLeaveApplications.Create)]
        public async Task<IActionResult> Create(long? empId)
        {
            var model = new EmpLeaveApplicationVm();
            model.EmployeeLookUp = _dropdownService.GetEmployeeSelectListItems();
            model.LeaveTypeLookUp = _dropdownService.GetDefaultSelectListItem();

            if (empId > 0)
            {
                var employee = await _iEmployeeService.GetEmployeeDataAsync((long)empId);
                if (employee == null)
                {
                    ExceptionMsg("No Employee Found...!");
                    return View();
                }
                model.EmployeeId = employee.Id;
                model.EmployeeName = employee.Name;
                model.EmployeeCode = employee.Code;
                model.EmpDesignation = employee.DesignationName;
                model.EmpPhotoUrl = employee.PhotoUrl;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpLeaveApplicationVm modelVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SaveFailedMsg("Information Is Not Correct");
                    return View("Create", modelVm);
                }

                _iService.CurrentUserId = UserId;

                var leaveFile = modelVm.GetLeaveApplicationFileToUploadFolder();

                //if (leaveFile == null)
                //{
                //    SaveFailedMsg("Leave File is empty");
                //    return View("Create", modelVm);
                //}
                var model = _iMapper.Map<EmpLeaveApplication>(modelVm);
                model.ActionById = UserId;
                model.ActionDate = DU.Utility.GetBdDateTimeNow();

                var isAdded = await _iService.LeaveAddAsync(modelVm);

                if (!isAdded)
                {
                    SaveFailedMsg();
                    return View("Create", modelVm);
                }
                await DU.Utility.UploadFileToFolderAsync(leaveFile);

                SaveSuccessMsg();
                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                ExceptionMsg(e.Message);
                return View("Create", modelVm);
            }
        }

        public async Task<IActionResult> GetLeaveSetupBalanceById(long leaveTypeId, long employeeId)
        {
            try
            {
                if (leaveTypeId == 0) return BadRequest();
                var model = await _iService.GetLeaveSetupBalance(leaveTypeId, employeeId);
                if (model == null) return NotFound();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Search

        [HttpGet]
        [Authorize(Permissions.EmpLeaveApplications.ListView)]
        public IActionResult Search()
        {
            var vm = new EmpLeaveApplicationSearchVm();
            return View(vm);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(IEnumerable<JsonResult>))]
        public async Task<IActionResult>
            Search(DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm> searchVm = null)
        {
            if (searchVm == null) searchVm = new DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm>();
            if (searchVm?.SearchModel == null) searchVm.SearchModel = new EmpLeaveApplicationSearchVm();
            var dataTable = await _iService.SearchAsync(searchVm);
            return dataTable == null ? NotFound() : Ok(dataTable);
        }

        #endregion

        #region Edit

        [HttpGet]
        [Authorize(Permissions.EmpLeaveApplications.Edit)]
        public IActionResult Edit(long id)
        {
            try
            {
                var data = _iService.GetById(id);
                if (data == null)
                {
                    return NotFoundMsg();
                }
                var model = _iMapper.Map<EmpLeaveApplicationVm>(data);
                model.FromDateStr = model.FromDate.ToString("dd/MM/yyyy");
                model.ToDateStr = model.ToDate.ToString("dd/MM/yyyy");
                model.EmployeeLookUp = _dropdownService.GetEmployeeSelectListItems();
                model.LeaveTypeLookUp = _dropdownService.GetEmpLeaveTypeSelectListItems();
                return View(model);
            }
            catch (Exception e)
            {
                ExceptionMsg(e.Message);
                return View("_404");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmpLeaveApplicationVm modelVm)
        {
            modelVm.EmployeeLookUp = _dropdownService.GetEmployeeSelectListItems();
            modelVm.LeaveTypeLookUp = _dropdownService.GetEmpLeaveTypeSelectListItems();

            try
            {
                if (modelVm.Id <= 0 || !ModelState.IsValid) return View(modelVm);
                var model = _iMapper.Map<EmpLeaveApplication>(modelVm);

                model.FromDate = (DateTime)(!string.IsNullOrEmpty(modelVm.FromDateStr) ? DU.Utility.ConvertStrToDate(modelVm.FromDateStr) : model.FromDate);
                model.ToDate = (DateTime)(!string.IsNullOrEmpty(modelVm.ToDateStr) ? DU.Utility.ConvertStrToDate(modelVm.ToDateStr) : model.ToDate);

                var isAdded = await _iService.UpdateAsync(model);
                if (!isAdded)
                {
                    UpdateFailedMsg();
                    return View("Edit", modelVm);
                }
                UpdateSuccessMsg();
                return RedirectToAction("Search");
            }
            catch (Exception ex)
            {
                ExceptionMsg(ex.Message);
                return View(modelVm);
            }
        }

        #endregion

        #region Detail

        [Authorize(Permissions.EmpLeaveApplications.DetailsView)]
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                var model = await _iService.GetEmployeeAppDataAsync(id);
                return View(model);
            }
            catch (Exception e)
            {
                ExceptionMsg(e.Message);
                return View("_404");
            }
        }

        #endregion

        #region Delete

        [HttpGet]
        [Authorize(Permissions.EmpLeaveApplications.Delete)]
        public async Task<IActionResult> Delete(long id)
        {
            var data = await _iService.GetByIdAsync(id);
            if (data == null)
            {
                DeleteFailedMsg();
                return NotFound();
            }
            data.IsDeleted = true;
            DeleteSuccessMsg();
            var isRemove = _iService.Update(data);
            return RedirectToAction("Search");
        }

        #endregion

        #region CancelLeave

        [Authorize(Permissions.EmpLeaveApplications.Create)]
        public async Task<ActionResult> CancelLeave(long id)
        {
            try
            {
                var isCancel = await _iService.CancelLeaveApp(id);
                if (isCancel)
                {
                    SaveSuccessMsg("Leave Cancel Success...!");
                }

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception e)
            {
                ExceptionMsg(e.Message);
                return RedirectToAction("Details", new { id = id });
            }
        }

        #endregion

        #region JsonData

        [HttpPost]
        public IActionResult GetLeaveByGenderJsonData(long employeeId)
        {
            var employee = _iEmployeeService.GetById(employeeId);
            if (employee == null) return NotFound();
            var dataList = _dropdownService.GetEmpLeaveTypeByGender(employee.Gender);
            return Ok(dataList);
        }

        public async Task<IActionResult> GetEmpLeaveDates(long empId, long typeId)
        {
            var dataList = await _iService.GetEmployeeLeaveDate(empId, typeId);
            return Ok(dataList);
        }

        #endregion

        #region LeaveReport
        [Authorize(Permissions.EmpLeaveApplications.ReportView)]
        public ActionResult LeaveReport()
        {
            var model = new EmpLeaveReportVm();
            model.DepatmentLookup = _dropdownService.GetDepartmentSelectListItems();
            model.DesignationLookup = _dropdownService.GetDesignationSelectListItems();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LeaveReport(EmpLeaveReportVm model)
        {
            var data = await _iService.GetLeaveReportData(model);
            return Ok(data);
        }

        #endregion

        #region LeaveReportPrint
        public async Task<ActionResult> LeaveReportPrint(string fromDate, string toDate, long? dptId)
        {
            var model = new EmpLeaveReportVm();
            model.FormDateStr = fromDate;
            model.ToDateStr = toDate;
            model.EmpDepatmentId = dptId;

            string html = await _iService.LeaveReportHtml(model);

            ExportToPDF export = new ExportToPDF(_ihttpContextAccessor);
            ExportDataTitle reportTitle = new ExportDataTitle();
            reportTitle.Header = PrintInfo.CompanyName;
            reportTitle.AddressOne = PrintInfo.CompanyAddress;
            reportTitle.ReportTitle = "Employee Leave Report";
            reportTitle.SignatureList = new List<string>();
            reportTitle.SignatureList.Add("Sign One");
            reportTitle.SignatureList.Add("Sign Two");

            string reportName = "Leave Report_" + DateTime.Now.ToString("dd/MM/yyyy");


            return File(export.ExportContentToPdf(html, reportName, reportTitle: reportTitle, bottom: 50, top: 80, left: 10, right: 10, isLandScape: true), "application/pdf");

        }
        #endregion

        #region EmpLeaveStatement
        [Authorize(Permissions.EmpLeaveApplications.ReportView)]
        public ActionResult EmpLeaveStatement()
        {
            var model = new EmpLeaveBalanceReportVm();
            model.EmployeeLookUp = _dropdownService.GetEmployeeSelectListItems();
            model.YearLookUp = _dropdownService.GetYearSelectListItems();
            model.SelectYear = DateTime.Now.Year;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EmpLeaveStatement(long employeeId, int selectYear)
        {
            var dataList = await _iService.EmployeeWiseLeaveBalance(employeeId, selectYear);
            return Ok(dataList);
        }
        #endregion

        #region EmpLeaveStatementPrint
        public async Task<ActionResult> EmpLeaveStatementPrint(short employeeId, int selectYear)
        {
            var model = new EmpLeaveBalanceReportVm();
            model.EmployeeId = employeeId;
            model.SelectYear = selectYear;
            var html = await _iService.EmpLeaveStatementHtml(model);

            ExportToPDF export = new ExportToPDF(_ihttpContextAccessor);
            ExportDataTitle reportTitle = new ExportDataTitle();
            reportTitle.Header = PrintInfo.CompanyName;
            reportTitle.AddressOne = PrintInfo.CompanyAddress;
            reportTitle.ReportTitle = "Employee Leave Report";
            reportTitle.SignatureList = new List<string>();
            reportTitle.SignatureList.Add("Sign One");
            reportTitle.SignatureList.Add("Sign Two");

            string reportName = "Leave Report_" + DateTime.Now.ToString("dd/MM/yyyy");

            return File(export.ExportContentToPdf(html, reportName, reportTitle: reportTitle, bottom: 50, top: 80, left: 10, right: 10, isLandScape: false), "application/pdf");
        }
        #endregion
    }
}
