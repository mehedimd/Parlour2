using Domain.Entities;
using Domain.ViewModel.EmpExperience;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpExperienceService : IService<EmpExperience>
    {
        Task<List<EmpExperienceVm>> GetEmpExperienceByEmpId(long empId);
    }
}
