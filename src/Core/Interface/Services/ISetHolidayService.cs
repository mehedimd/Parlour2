using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.SetHoliday;
using Interface.Base;

namespace Interface.Services;

public interface ISetHolidayService : IService<SetHoliday>
{
    Task<DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm>>
                                SearchAsync(DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm> model);
}
