using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrCustomer;
using Interface.Base;

namespace Interface.Services;

public interface IPrCustomerService : IService<PrCustomer>
{
    Task<DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm>>
                                SearchAsync(DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm> model);
    Task<PrCustomerVm?> GetCustomerByMobile(string mobile);
}
