using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveSetup;
using Interface.Base;

namespace Interface.Services
{
    public interface ILeaveSetupService : IService<LeaveSetup>
    {
        Task<DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm>>
                              SearchAsync(DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm> model);
    }
}
