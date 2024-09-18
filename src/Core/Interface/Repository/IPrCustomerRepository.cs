using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrCustomer;
using Interface.Base;

namespace Interface.Repository;

public interface IPrCustomerRepository : IRepository<PrCustomer>
{
    Task<DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm>>
        SearchAsync(DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm> vm);
}
