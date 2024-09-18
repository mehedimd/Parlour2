using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.SetHoliday;
using Interface.Base;

namespace Interface.Repository
{
    public interface ISetHolidayRepository : IRepository<SetHoliday>
    {
        Task<DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm>>
            SearchAsync(DataTablePagination<SetHolidaySearchVm, SetHolidaySearchVm> model);
        Task<bool> IsHoliday(DateTime date);
    }
}
