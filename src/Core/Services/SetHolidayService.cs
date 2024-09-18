using AutoMapper;
using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.SetHoliday;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services;

public class SetHolidayService : BaseService<SetHoliday>, ISetHolidayService
{
    #region Config
    private ISetHolidayRepository Repository;
    private readonly IMapper _iMapper;
    private readonly IUnitOfWork _iUnitOfWork;

    public SetHolidayService(ISetHolidayRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork)
        : base(iRepository, iUnitOfWork)
    {
        Repository = iRepository;
        _iMapper = iMapper;
        _iUnitOfWork = iUnitOfWork;
    }
    #endregion

    #region Search

    public async Task<DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm>> SearchAsync(DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm> model)
    {
        var dataList = await Repository.SearchAsync(model);
        return dataList;
    }

    #endregion
}
