using Domain.Entities;
using Domain.ViewModel.EmpPosting;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpPostingService : IService<EmpPosting>
    {
        Task<List<EmpPostingVm>> GetPostingByEmpId(long empId);
    }
}
