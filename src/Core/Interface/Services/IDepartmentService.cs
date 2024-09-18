using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Department;
using Interface.Base;

namespace Interface.Services
{
    public interface IDepartmentService : IService<Department>
    {
        Task<DataTablePagination<DepartmentSearchVm, DepartmentSearchVm>>
                  SearchAsync(DataTablePagination<DepartmentSearchVm, DepartmentSearchVm> model);
    }
}
