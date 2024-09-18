using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveSetup;
using Interface.Base;

namespace Interface.Repository
{
    public interface ILeaveSetupRepository : IRepository<LeaveSetup>
    {
        Task<DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm>>
                                  SearchAsync(DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm> model);
    }
}
