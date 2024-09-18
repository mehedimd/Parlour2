using Domain.Entities;
using Domain.ViewModel.EmpDisciplinary;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpDisciplinaryService : IService<EmpDisciplinary>
    {
        Task<List<EmpDisciplinaryVm>> GetEmpDisciplinaryByEmpId(long empId);
    }
}
