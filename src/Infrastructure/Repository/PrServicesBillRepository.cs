using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility;
using Domain.Utility.Common;
using Domain.ViewModel.PrServicesBill;
using Domain.ViewModel.TranPayment;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository;

public class PrServicesBillRepository : BaseRepository<PrServicesBill>, IPrServicesBillRepository, IDisposable
{
    #region Config
    private ApplicationDbContext Context => Db as ApplicationDbContext;
    private readonly IMapper _iMapper;

    public PrServicesBillRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
    {
        Db = db;
        _iMapper = iMapper;
    }
    #endregion

    #region Search

    public async Task<DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm>>
        SearchAsync(DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm> vm)
    {
        var searchResult = Context.PrServicesBills
            .Include(c => c.Customer)
            .Include(s => s.ServiceShift)
            .AsQueryable().Where(c => !c.IsDeleted);
        var model = vm.SearchModel;

        if (model == null) throw new Exception("Search Department not found");

        if (!string.IsNullOrEmpty(model.FormDateStr))
        {
            var formDate = (DateTime)(!string.IsNullOrEmpty(model.FormDateStr) ? Utility.ConvertStrToDate(model.FormDateStr) : model.BookingDate);
            searchResult = searchResult.Where(c => c.ServiceDate.Date >= formDate.Date);
        }

        if (!string.IsNullOrEmpty(model.ToDateStr))
        {
            var toDate = (DateTime)(!string.IsNullOrEmpty(model.ToDateStr) ? Utility.ConvertStrToDate(model.ToDateStr) : model.BookingDate);
            searchResult = searchResult.Where(c => c.ServiceDate.Date <= toDate.Date);
        }

        if (!string.IsNullOrEmpty(model.ServiceNo))
        {
            searchResult = searchResult.Where(c => c.ServiceNo.ToLower().Contains(model.ServiceNo.ToLower()));
        }

        if (!string.IsNullOrEmpty(model.CustomerMobile))
        {
            searchResult = searchResult.Where(c => c.Customer.Mobile.ToLower().Contains(model.CustomerMobile.ToLower()));
        }

        if (!string.IsNullOrEmpty(vm.Search.Value))
        {
            var value = vm.Search.Value.Trim().ToLower();
            searchResult = searchResult.Where(c => c.ServiceNo.ToLower().Contains(value));
        }

        var totalRecords = await searchResult.CountAsync();
        if (totalRecords > 0)
        {
            vm.recordsTotal = totalRecords;
            vm.recordsFiltered = totalRecords;
            vm.draw = vm.LineDraw ?? 0;

            var data = await searchResult.OrderBy(c => c.ServiceDate)
                                         .Skip(vm.Start)
                                         .Take(vm.Length)
                                         .ToListAsync();

            vm.data = _iMapper.Map<List<PrServicesBillSearchVm>>(data);

            var sl = vm.Start;

            foreach (var searchDto in vm.data)
            {
                var filterData = data.Where(c => c.Id == searchDto.Id).FirstOrDefault();

                searchDto.SerialNo = ++sl;
                searchDto.ServiceShiftName = filterData?.ServiceShift.ShiftName;
                searchDto.CustomerName = filterData?.Customer.CustomerName;
                searchDto.CustomerMobile = filterData?.Customer.Mobile;
                searchDto.CustomerMobile = filterData?.Customer.Mobile;
            }
        }
        return vm;
    }

    #endregion

    #region GetServiceBillByIdAsync

    public async Task<PrServicesBillVm> GetServiceBillByIdAsync(long id)
    {
        var dataModel = await Context.PrServicesBills
            .Include(c => c.Customer)
            .Include(s => s.ServiceShift)
            .Include(d => d.PrServicesBillDetails)
                .ThenInclude(x => x.Service)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (dataModel == null)
            throw new Exception("Bill Not Found...!!");

        var model = _iMapper.Map<PrServicesBillVm>(dataModel);

        model.ServiceShiftName = dataModel.ServiceShift?.ShiftName;
        model.CustomerName = dataModel.Customer.CustomerName;
        model.CustomerMobile = dataModel.Customer.Mobile;
        model.CustomerEmail = dataModel.Customer.Email;
        model.CustomerAddress = dataModel.Customer.Address;

        var paidList = Context.TranPayments.Where(c => c.ServiceBillId == dataModel.Id).ToList();

        model.PaidAmount = paidList.Sum(c => c.Amount);
        string paymodeResult = string.Join(",", paidList.Select(x => x.PayMode).ToList());
        model.PayMode = paymodeResult;

        model.PaymentList = _iMapper.Map<List<TranPaymentVm>>(paidList);

        if (model.PrServicesBillDetails.Count > 0)
        {
            foreach (var detailModel in model.PrServicesBillDetails)
            {
                var filterData = dataModel.PrServicesBillDetails.FirstOrDefault(c => c.Id == detailModel.Id);

                detailModel.ServiceName = filterData?.Service.ServiceName;
            }
        }

        return model;
    }

    #endregion

    #region GetServiceByCategoryId

    public async Task<List<PrServiceInfo>> GetServiceByCategoryId(long? categoryId)
    {
        var serviceList = await Context.PrServiceInfos.Where(x => !x.IsDeleted).ToListAsync();

        if (categoryId > 0)
        {
            serviceList = serviceList.Where(x => x.CategoryId == categoryId).ToList();
        }

        return serviceList;
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        Context.Dispose();
    }

    #endregion
}