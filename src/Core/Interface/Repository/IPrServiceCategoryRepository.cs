using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IPrServiceCategoryRepository: IRepository<PrServiceCategory>
    {
        Task<DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm>>
      SearchAsync(DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm> model);
    }
}
