using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Interface.Base;

namespace Interface.Services;

public interface IPrServiceCategoryService : IService<PrServiceCategory>
{
    Task<DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm>> 
        SearchAsync(DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm> model);
}
