using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveCf;
using Interface.Base;

namespace Interface.Repository
{
    public interface ILeaveCfRepository : IRepository<LeaveCf>
    {
        Task<DataTablePagination<LeaveCfSearchVm, LeaveCfSearchVm>>
                             SearchAsync(DataTablePagination<LeaveCfSearchVm, LeaveCfSearchVm> model);
        Task<bool> CalculateDailyCf();
        Task<bool> SetupNewYearCf();
    }
}
