using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services;

public class PrServiceCategoryService : BaseService<PrServiceCategory>, IPrServiceCategoryService
{
    #region Config
    private IPrServiceCategoryRepository Repository;
    private readonly IMapper _iMapper;
    private readonly IUnitOfWork _iUnitOfWork;

    public PrServiceCategoryService(IPrServiceCategoryRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
        : base(iRepository, iUnitOfWork)
    {
        Repository = iRepository;
        _iMapper = iMapper;
        _iUnitOfWork = iUnitOfWork;
    }
    #endregion

    #region Search

    public async Task<DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm>> SearchAsync(DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm> model)
    {
        var dataList = await Repository.SearchAsync(model);
        return dataList;
    }

    #endregion
}
