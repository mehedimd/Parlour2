using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServicesBill;
using Interface.Base;

namespace Interface.Repository;

public interface IPrServicesBillRepository : IRepository<PrServicesBill>
{
    Task<DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm>>
        SearchAsync(DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm> vm);
    Task<List<PrServiceInfo>> GetServiceByCategoryId(long? categoryId);
    Task<PrServicesBillVm> GetServiceBillByIdAsync(long id);
}
