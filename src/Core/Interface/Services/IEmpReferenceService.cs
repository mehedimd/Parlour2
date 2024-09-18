using Domain.Entities;
using Domain.ViewModel.EmpReference;
using Interface.Base;

namespace Interface.Services
{
    public interface IEmpReferenceService : IService<EmpReference>
    {
        Task<List<EmpReferenceVm>> GetReferenceByEmpId(long empId, short refType);
    }
}
