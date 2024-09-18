using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveType;
using Interface.Base;

namespace Interface.Repository
{
    public interface ILeaveTypeRepository : IRepository<LeaveType>
    {
        Task<DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm>>
                             SearchAsync(DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm> model);
    }
}
