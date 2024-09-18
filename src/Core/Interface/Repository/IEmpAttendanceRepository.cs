using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.EmpAttendence;
using Domain.ViewModel.MonthlyAttSheet;
using Interface.Base;

namespace Interface.Repository;

public interface IEmpAttendanceRepository : IRepository<EmpAttendance>
{
    Task<DataTablePagination<EmpAttendanceSearchVm, EmpAttendanceSearchVm>>
        SearchAsync(DataTablePagination<EmpAttendanceSearchVm, EmpAttendanceSearchVm> model);
    Task<MonthlyAttSheetMstVm> GetMonthlyAttendanceData(MonthlyAttSheetMstVm vm);
    Task<DataTablePagination<EmpDailyAttendanceReportVm, EmpDailyAttendanceReportVm>> DailyReportAsync(DataTablePagination<EmpDailyAttendanceReportVm, EmpDailyAttendanceReportVm> vm);
    Task<DataTablePagination<EmpDailyAttendanceReportVm, EmpDailyAttendanceReportVm>> DayWiseReportAsync(DataTablePagination<EmpDailyAttendanceReportVm, EmpDailyAttendanceReportVm> vm);
}
