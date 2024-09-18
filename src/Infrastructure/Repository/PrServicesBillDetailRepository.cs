using AutoMapper;
using Domain.Entities.Parlour;
using Domain.ViewModel.PrServicesBill;
using Domain.ViewModel.PrServicesBillDetail;
using Domain.ViewModel.TranPayment;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository;

public class PrServicesBillDetailRepository : BaseRepository<PrServicesBillDetail>, IPrServicesBillDetailRepository, IDisposable
{
    #region Config
    private ApplicationDbContext Context => Db as ApplicationDbContext;
    private readonly IMapper _iMapper;

    public PrServicesBillDetailRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
    {
        Db = db;
        _iMapper = iMapper;
    }
    #endregion

    #region GetBillDetailsByBillIdAsync

    public async Task<List<PrServicesBillDetailVm>> GetBillDetailsByBillIdAsync(long billId)
    {
        var dataModelList = await Context.PrServicesBillDetails
            .Include(b => b.ServiceBill)
            .Include(s => s.Service)
            .Include(p => p.Provider)
            .Include(a => a.AssignBy)
            .Where(c => c.ServiceBillId == billId && !c.IsDeleted)
            .ToListAsync();

        if (!(dataModelList.Count > 0))
            throw new Exception("Bill Details Not Found...!!");

        var dataList = _iMapper.Map<List<PrServicesBillDetailVm>>(dataModelList);

        if (dataList.Count > 0)
        {
            foreach (var detailModel in dataList)
            {
                var filterData = dataModelList.FirstOrDefault(c => c.Id == detailModel.Id);

                detailModel.ServiceName = filterData?.Service.ServiceName;
                detailModel.ProviderName = filterData?.Provider?.Name;
                detailModel.ServiceBillNo = filterData?.ServiceBill?.ServiceNo;
                detailModel.AssignByName = filterData?.ActionBy?.FullName;
            }
        }

        return dataList;
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        Context.Dispose();
    }

    #endregion
}