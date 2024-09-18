using AutoMapper;
using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Department;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Config
        private IDepartmentRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public DepartmentService(IDepartmentRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
            : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<DepartmentSearchVm, DepartmentSearchVm>>
            SearchAsync(DataTablePagination<DepartmentSearchVm, DepartmentSearchVm> model)
        {
            var dataList = await Repository.SearchAsync(model);
            return dataList;
        }

        #endregion
    }
}
