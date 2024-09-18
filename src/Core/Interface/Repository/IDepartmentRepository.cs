using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Department;
using Interface.Base;

namespace Interface.Repository;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<DataTablePagination<DepartmentSearchVm, DepartmentSearchVm>>
        SearchAsync(DataTablePagination<DepartmentSearchVm, DepartmentSearchVm> model);
}
