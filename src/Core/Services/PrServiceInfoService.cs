using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Domain.ViewModel.PrServiceInfo;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PrServiceInfoService : BaseService<PrServiceInfo>, IPrServiceInfoService
    {
        #region Config
        private IPrServiceInfoRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public PrServiceInfoService(IPrServiceInfoRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
            : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm>> SearchAsync(DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm> model)
        {
            var dataList = await Repository.SearchAsync(model);
            return dataList;
        }

        #endregion
    }
}
