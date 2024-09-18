using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrCustomer;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services;

public class PrCustomerService : BaseService<PrCustomer>, IPrCustomerService
{
    #region Config
    private IPrCustomerRepository Repository;
    private readonly IMapper _iMapper;
    private readonly IUnitOfWork _iUnitOfWork;

    public PrCustomerService(IPrCustomerRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
        : base(iRepository, iUnitOfWork)
    {
        Repository = iRepository;
        _iMapper = iMapper;
        _iUnitOfWork = iUnitOfWork;
    }
    #endregion

    #region Search

    public async Task<DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm>> SearchAsync(DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm> model)
    {
        var dataList = await Repository.SearchAsync(model);
        return dataList;
    }

    #endregion

    #region GetCustomerByMobile

    public async Task<PrCustomerVm?> GetCustomerByMobile(string mobile)
    {
        var dataModel = await Repository.GetFirstOrDefaultAsync(x => x.Mobile.Equals(mobile) && !x.IsDeleted);

        if (dataModel == null)
            return null;

        var model = _iMapper.Map<PrCustomerVm>(dataModel);

        return model;
    }

    #endregion
}
