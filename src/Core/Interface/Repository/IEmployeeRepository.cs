using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.Employees;
using Interface.Base;

namespace Interface.Repository;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<DataTablePagination<EmployeeSearchVm, EmployeeSearchVm>>
        SearchAsync(DataTablePagination<EmployeeSearchVm, EmployeeSearchVm> model);
    Task<EmployeeVm> GetEmployeeByIdAsync(long id);
    Task<DataTablePagination<EmployeeSearchVm, EmployeeSearchVm>> SearchAllAsync(DataTablePagination<EmployeeSearchVm, EmployeeSearchVm> vm);
}
