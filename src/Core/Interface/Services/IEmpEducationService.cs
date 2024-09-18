using Domain.Entities;
using Domain.ViewModel.EmpEducation;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpEducationService : IService<EmpEducation>
    {
        Task<List<EmpEducationVm>> GetEmpEducationByEmpId(long empId);
    }
}
