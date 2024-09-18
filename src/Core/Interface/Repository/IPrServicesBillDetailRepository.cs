using Domain.Entities.Parlour;
using Domain.ViewModel.PrServicesBillDetail;
using Interface.Base;

namespace Interface.Repository;

public interface IPrServicesBillDetailRepository : IRepository<PrServicesBillDetail>
{
    Task<List<PrServicesBillDetailVm>> GetBillDetailsByBillIdAsync(long billId);
}