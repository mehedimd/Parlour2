using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveType;
using Interface.Base;

namespace Interface.Services
{
    public interface ILeaveTypeService : IService<LeaveType>
    {
        Task<DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm>>
                                          SearchAsync(DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm> model);
    }
}
