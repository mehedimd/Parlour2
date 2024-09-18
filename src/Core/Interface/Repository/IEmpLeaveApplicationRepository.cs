using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.EmpLeaveApplication;
using Interface.Base;

namespace Interface.Repository
{
    public interface IEmpLeaveApplicationRepository : IRepository<EmpLeaveApplication>
    {
        Task<DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm>>
                                SearchAsync(DataTablePagination<EmpLeaveApplicationSearchVm, EmpLeaveApplicationSearchVm> model);
        Task<EmpLeaveApplicationVm> GetEmployeeAppByIdAsync(long id);
        Task<List<EmpLeaveReportVm>> GetLeaveReportData(EmpLeaveReportVm vm);
    }
}
