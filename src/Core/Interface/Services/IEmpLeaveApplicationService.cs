using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.EmpLeaveApplication;
using Interface.Base;

namespace Interface.Services;

public interface IEmpLeaveApplicationService : IService<EmpLeaveApplication>
{
    Task<DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm>>
                                         SearchAsync(DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm> model);
    Task<EmpLeaveApplicationVm> GetEmployeeAppDataAsync(long id);
    Task<bool> LeaveAddAsync(EmpLeaveApplicationVm vm);
    Task<List<EmpLeaveReportVm>> GetLeaveReportData(EmpLeaveReportVm vm);
    Task<string> LeaveReportHtml(EmpLeaveReportVm vm);
    Task<EmpLeaveBalanceReportVm> EmployeeWiseLeaveBalance(long employeeId, int? searchYear);
    Task<string> EmpLeaveStatementHtml(EmpLeaveBalanceReportVm model);
    Task<LeaveBalanceVm> GetLeaveSetupBalance(long leaveTypeId, long employeeId);
    Task<bool> CancelLeaveApp(long appId);
    Task<List<EmpLeaveTypeLeaveReport>> GetEmployeeLeaveDate(long empId, long typeId);
}
