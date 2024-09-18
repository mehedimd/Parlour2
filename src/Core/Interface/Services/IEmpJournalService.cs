using Domain.Entities;
using Domain.ViewModel.EmpJournal;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpJournalService : IService<EmpJournal>
    {
        Task<List<EmpJournalVm>> GetEmpJournalByEmpId(long empId);
    }
}
