using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Designation;
using Interface.Base;

namespace Interface.Services
{
    public interface IDesignationService : IService<Designation>
    {
        Task<DataTablePagination<DesignationSearchVm, DesignationSearchVm>>
                                    SearchAsync(DataTablePagination<DesignationSearchVm, DesignationSearchVm> model);
    }
}
