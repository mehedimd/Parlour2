using Domain.Entities;
using Domain.ViewModel.EmpPosting;
using Interface.Base;

namespace Interface.Repository
{
    public interface IEmpPostingRepository : IRepository<EmpPosting>
    {
        Task<List<EmpPostingVm>> GetPostingByEmpId(long empId);
    }
}
