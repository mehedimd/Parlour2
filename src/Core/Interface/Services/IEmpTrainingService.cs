using Domain.Entities;
using Domain.ViewModel.EmpTraining;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpTrainingService : IService<EmpTraining>
    {
        Task<List<EmpTrainingVm>> GetTrainingByEmpId(long empId);

    }
}
