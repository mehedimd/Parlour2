using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Domain.ViewModel.PrServiceInfo;
using Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface IPrServiceInfoRepository : IRepository<PrServiceInfo>
    {
        Task<DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm>>
      SearchAsync(DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm> model);
    }
}
