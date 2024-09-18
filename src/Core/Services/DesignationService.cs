using AutoMapper;
using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Designation;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services
{
    public class DesignationService : BaseService<Designation>, IDesignationService
    {
        #region Config
        private IDesignationRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public DesignationService(IDesignationRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
            : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<DesignationSearchVm, DesignationSearchVm>> SearchAsync(DataTablePagination<DesignationSearchVm, DesignationSearchVm> model)
        {
            var dataList = await Repository.SearchAsync(model);
            return dataList;
        }

        #endregion
    }
}
