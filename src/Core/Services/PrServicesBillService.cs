using AutoMapper;
using Domain.Dto;
using Domain.Entities.Parlour;
using Domain.Enums.AppEnums;
using Domain.Utility;
using Domain.Utility.Common;
using Domain.ViewModel.PrServicesBill;
using Domain.ViewModel.PrServicesBillDetail;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;
using System.Transactions;
using DU = Domain.Utility;

namespace Services;

public class PrServicesBillService : BaseService<PrServicesBill>, IPrServicesBillService
{
    #region Config
    private IPrServicesBillRepository Repository;
    private readonly IMapper _iMapper;
    private readonly IUnitOfWork _iUnitOfWork;
    private readonly IAutoCodeRepository _iAutoCodeRepository;
    private readonly IPrCustomerRepository _iPrCustomerRepository;
    private readonly ITranPaymentRepository _iTranPaymentRepository;
    private readonly IEmployeeRepository _iEmployeeRepository;
    private readonly IBranchRepository _iBranchRepository;
    private readonly IPrServicesBillDetailRepository _iPrServicesBillDetailRepository;

    public PrServicesBillService(IPrServicesBillRepository iRepository,
        IMapper iMapper,
        IUnitOfWork iUnitOfWork,
        IAutoCodeRepository iAutoCodeRepository,
        IPrCustomerRepository iPrCustomerRepository,
        ITranPaymentRepository iTranPaymentRepository,
        IEmployeeRepository iEmployeeRepository,
        IBranchRepository iBranchRepository,
        IPrServicesBillDetailRepository iPrServicesBillDetailRepository)
        : base(iRepository, iUnitOfWork)
    {
        Repository = iRepository;
        _iMapper = iMapper;
        _iUnitOfWork = iUnitOfWork;
        _iAutoCodeRepository = iAutoCodeRepository;
        _iPrCustomerRepository = iPrCustomerRepository;
        _iTranPaymentRepository = iTranPaymentRepository;
        _iEmployeeRepository = iEmployeeRepository;
        _iBranchRepository = iBranchRepository;
        _iPrServicesBillDetailRepository = iPrServicesBillDetailRepository;
    }
    #endregion

    #region BillGenerate

    public async Task<(bool, long)> BillGenerate(PrServicesBillVm vm)
    {
        var billModel = _iMapper.Map<PrServicesBill>(vm);

        billModel.ServiceNo = await GetInvoiceNo();
        billModel.ServiceDate = (DateTime)(!string.IsNullOrEmpty(vm.ServiceDateStr) ? Utility.ConvertStrToDate(vm.ServiceDateStr) : DU.Utility.GetBdDateTimeNow());
        billModel.BookingDate = DU.Utility.GetBdDateTimeNow();
        //billModel.ServiceStatus = (short)ServiceStatusEnum.Booking;
        billModel.ActionById = CurrentUserId;
        billModel.ActionDate = Utility.GetBdDateTimeNow();
        billModel.EntryById = CurrentUserId;

        billModel.BillStatus = (short)BillStatusEnum.Pending;

        var userEmployee = await _iEmployeeRepository.GetFirstOrDefaultAsync(x => x.UserId == CurrentUserId);
        if (userEmployee == null)
        {
            var mainBranch = _iBranchRepository.GetFirstOrDefault();
            if (mainBranch == null)
                throw new Exception("No Main Branch Found...!");

            billModel.BranchId = mainBranch.Id;
        }
        else
        {
            billModel.BranchId = userEmployee.BranchId;
        }


        if (vm.PrServicesBillDetails?.Count > 0 is false)
            throw new Exception("Service Items Information Not Found...!!");

        var billDetails = _iMapper.Map<List<PrServicesBillDetail>>(vm.PrServicesBillDetails);

        if (billDetails.Count > 0)
        {
            foreach (var detail in billDetails)
            {
                detail.ServiceStatus = (short)ServiceStatusEnum.Booking;
                detail.ActionById = CurrentUserId;
                detail.ActionDate = Utility.GetBdDateTimeNow();

                detail.NetAmount = (detail.Amount + detail.VAT + detail.Tax) - detail.Discount;
            }

            billModel.TotalBill = billDetails.Sum(x => x.Amount);
            billModel.NetAmount = (billModel.TotalBill + billModel.VAT + billModel.Tax) - billModel.Discount;

            billModel.PrServicesBillDetails = billDetails;
        }


        PrCustomer? custmerModel = null;

        if (vm.CustomerId > 0)
        {
            var customer = await _iPrCustomerRepository.GetFirstOrDefaultAsync(x => x.Id == vm.CustomerId && !x.IsDeleted);
            if (customer == null)
                throw new Exception("Customer Information Not Found...!!");

            billModel.CustomerId = customer.Id;
        }
        else
        {
            custmerModel = new PrCustomer();

            custmerModel.RegistrationNo = await GetCustomerNo();
            custmerModel.RegistrationDate = Utility.GetBdDateTimeNow();
            custmerModel.CustomerName = vm.CustomerName;
            custmerModel.Mobile = vm.CustomerMobile;
            custmerModel.Address = vm.CustomerAddress;
            custmerModel.Email = vm.CustomerEmail;
            custmerModel.ActionDate = Utility.GetBdDateTimeNow();
            custmerModel.ActionById = CurrentUserId;
            custmerModel.BranchId = billModel.BranchId;
        }

        TranPayment? paymentModel = null;

        if (!string.IsNullOrEmpty(vm.PayMode) && vm.PaidAmount > 0)
        {
            paymentModel = new TranPayment();

            paymentModel.PayMode = vm.PayMode;
            paymentModel.Amount = vm.PaidAmount;
            paymentModel.TranType = TranType.Receive;
            paymentModel.TranDate = Utility.GetBdDateTimeNow();
            paymentModel.Remarks = $"Amount {vm.PaidAmount} is paid.";
            paymentModel.ActionById = CurrentUserId;
            paymentModel.ActionDate = Utility.GetBdDateTimeNow();
            paymentModel.BranchId = billModel.BranchId;

            if (billModel.NetAmount < paymentModel.Amount)
                throw new Exception("Paid Amount Is Higher Than Net Amount...!!");

            if (billModel.NetAmount == paymentModel.Amount)
                billModel.BillStatus = (short)BillStatusEnum.Full;
            else if (billModel.NetAmount > paymentModel.Amount)
                billModel.BillStatus = (short)BillStatusEnum.Partial;
        }

        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (custmerModel != null)
        {
            await _iPrCustomerRepository.AddAsync(custmerModel);
            await _iUnitOfWork.CompleteAsync();

            billModel.CustomerId = custmerModel.Id;
        }

        await Repository.AddAsync(billModel);
        var isAdded = await _iUnitOfWork.CompleteAsync();

        if (paymentModel != null)
        {
            paymentModel.ServiceBillId = billModel.Id;

            await _iTranPaymentRepository.AddAsync(paymentModel);
            var isPaymentAdded = await _iUnitOfWork.CompleteAsync();
            if (!isPaymentAdded) { return (false, 0); }
        }
        else
        {
            if (!isAdded) { return (false, 0); }
        }

        ts.Complete();
        return (true, billModel.Id);
    }

    #endregion

    #region Search

    public async Task<DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm>> SearchAsync(DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm> model)
    {
        var dataList = await Repository.SearchAsync(model);
        return dataList;
    }

    #endregion

    #region GetBillInfo

    public async Task<PrServicesBillVm> GetBillInfoById(long billId)
    {
        var data = await Repository.GetServiceBillByIdAsync(billId);
        return data;
    }

    #endregion

    #region GetInvoiceNo

    public async Task<string> GetInvoiceNo()
    {
        var data = await _iAutoCodeRepository.GetInvoiceMaxAutoCode(TableEnum.PrServicesBills.ToString(), "ServiceNo", prefix: $"S", suffix: $"/{DateTime.Now.ToString("yy")}", howManyDigit: 6, isPrefixFilter: true);
        return data;
    }

    #endregion

    #region GetCustomerNo

    public async Task<string> GetCustomerNo()
    {
        var data = await _iAutoCodeRepository.GetMaxAutoCode(TableEnum.PrCustomers.ToString(), "RegistrationNo", prefix: $"C", howManyDigit: 6);
        return data;
    }

    #endregion

    #region GetServiceByCategoryId

    public async Task<List<PrServiceDto>> GetServiceByCategoryId(long? categoryId)
    {
        var serviceList = await Repository.GetServiceByCategoryId(categoryId);

        var dataList = serviceList.Select(x => new PrServiceDto
        {
            Id = x.Id,
            ServiceName = x.ServiceName,
            ServiceCode = x.ServiceCode,
            Rate = x.Rate
        }).ToList();

        return dataList;
    }

    #endregion

    #region OrderBillHtml

    public async Task<string> GetServiceBillByIdAsyncHtml(long id)
    {
        var data = await GetBillInfoById(id);

        var fullHtml = "";
        fullHtml += "<div><p><u>Bill To :</u></p></div>";
        fullHtml += "<div style='padding-top:5px'>";
        fullHtml += $@"<table class='receipt-table receipt-border'>
                                <tbody>
                                    <tr>
                                        <td style='width:40%;'><b>Schedule</b></td>
                                        <td style='width:60%'>{data.ServiceShiftName}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:40%;'><b>Bill No</b></td>
                                        <td style='width:60%'>{data.ServiceNo}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:40%;'><b>Appointment Date</b></td>
                                        <td style='width:60%'>{data.ServiceDate.ToString("dd-MMM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:40%;'><b>Customer Name</b></td>
                                        <td style='width:60%'>{data.CustomerName}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:40%;'><b>Customer Mobile</b></td>
                                        <td style='width:60%'>{data.CustomerMobile}</td>
                                    </tr>
                                    <tr>
                                        <td style='width:40%;'><b>Sales Person</b></td>
                                        <td style='width:60%'>{data.EntryByName}</td>
                                    </tr>
                                </tbody>
                            </table>";

        fullHtml += "</div>";

        //Order Item
        fullHtml += "<table class='receipt-table-dtl receipt-border' style='width:100%;text-align:center;margin-top:5px;font-size:10px'>";

        fullHtml += "<tbody>";

        if (data.PrServicesBillDetails.Count > 0)
        {
            foreach (var (item, i) in data.PrServicesBillDetails.GetItemWithIndex())
            {
                fullHtml += "<tr>";

                fullHtml += $@"<td class='text-start' style='width:75%;'><b>{item.ServiceName}</b><br/>";

                fullHtml += $@"{item.Qty}x{item.Rate}</td>";

                fullHtml += $@"<td class='text-end' style='width:25%;'>{item.Amount}</td>";

                fullHtml += "</tr>";
            }
        }

        fullHtml += "</tbody>";
        fullHtml += "</table>";

        fullHtml += "<table class='receipt-table-dtl' style='width:100%;text-align:center;margin-top:5px;font-size:10px'>";

        fullHtml += "<tbody>";

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>Sub Total</b></td>";
        fullHtml += $@"<td class='text-end'>{data.TotalBill.ToString("N2")}</td>";
        fullHtml += "</tr>";

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>VAT</b></td>";
        fullHtml += $@"<td class='text-end'>{data.VAT.ToString("N2")}</td>";
        fullHtml += "</tr>";

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>Discount</b></td>";
        fullHtml += $@"<td class='text-end'>{data.Discount.ToString("N2")}</td>";
        fullHtml += "</tr>";

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>Total Amount</b></td>";
        fullHtml += $@"<td class='text-end'>{data.NetAmount.ToString("N2")}</td>";
        fullHtml += "</tr>";

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>Paid Amount</b></td>";
        fullHtml += $@"<td class='text-end'>{data.PaidAmount.ToString("N2")}</td>";
        fullHtml += "</tr>";

        var changeAmount = data.PaidAmount > data.NetAmount ? (data.PaidAmount - data.NetAmount) : 0;

        fullHtml += "<tr>";
        fullHtml += $@"<td class='text-start'><b>Change</b></td>";
        fullHtml += $@"<td class='text-end'>{changeAmount.ToString("N2")}</td>";
        fullHtml += "</tr>";

        fullHtml += "</tbody>";
        fullHtml += "</table>";

        return fullHtml;
    }

    #endregion

    #region BillServiceAdd

    public async Task<bool> BillServiceAddAsync(SavePrServiceBillDetailDto dto)
    {
        if (dto == null || !(dto.ServiceId > 0) || !(dto.ServiceBillId > 0))
            throw new Exception("Billing Information Is Not Correct...!!");

        var bill = await Repository.GetFirstOrDefaultAsync(x => x.Id == dto.ServiceBillId && !x.IsDeleted);

        if (bill == null)
            throw new Exception("Bill Not Found...!!");

        if (bill.BillStatus == (short)BillStatusEnum.Full && bill.ServiceStatus == (short)ServiceStatusEnum.Completed)
            throw new Exception("Bill Already Full Paid & Completed...!!");

        #region Bill Details

        var detailModel = _iMapper.Map<PrServicesBillDetail>(dto);
        detailModel.ActionById = CurrentUserId;
        detailModel.ActionDate = Utility.GetBdDateTimeNow();

        detailModel.Amount = detailModel.Rate * detailModel.Qty;
        detailModel.NetAmount = (detailModel.Amount + detailModel.VAT + detailModel.Tax) - (detailModel.Discount);

        #endregion

        bill.TotalBill += detailModel.Amount;
        bill.NetAmount += detailModel.NetAmount;
        bill.BillStatus = (short)BillStatusEnum.Partial;

        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _iPrServicesBillDetailRepository.AddAsync(detailModel);
        await Repository.UpdateAsync(bill);
        var isExecuted = await _iUnitOfWork.CompleteAsync();
        if (!isExecuted) { return false; }
        ts.Complete();
        return true;
    }

    #endregion

    #region BillPaymentEntry

    public async Task<bool> PaymentEntry(ServicePaymentDto payment)
    {
        if (payment == null || !(payment?.ServiceBillId > 0))
            throw new Exception("Payemnt Information Is Not Correct...!!");

        var bill = await Repository.GetFirstOrDefaultAsync(x => x.Id == payment.ServiceBillId && !x.IsDeleted);

        if (bill == null)
            throw new Exception("Bill Information Not Found...!!");

        if (bill.BillStatus == (short)BillStatusEnum.Full)
            throw new Exception("Can't pay the amount cause payment is fully paid...!!");

        var model = _iMapper.Map<TranPayment>(payment);
        model.BranchId = bill.BranchId;
        model.TranDate = (DateTime)(!string.IsNullOrEmpty(payment.TranDateStr) ? Utility.ConvertStrToDate(payment.TranDateStr) : DU.Utility.GetBdDateTimeNow());
        model.ActionById = CurrentUserId;
        model.ActionDate = Utility.GetBdDateTimeNow();
        model.Remarks = $"Amount {payment.Amount} is paid partialy.";

        var paidList = _iTranPaymentRepository.Get(c => c.ServiceBillId == bill.Id).ToList();
        var alreadyPaidAmount = paidList.Sum(x => x.Amount);

        var totalPayAmount = alreadyPaidAmount + model.Amount;

        if(totalPayAmount > bill.NetAmount)
            throw new Exception("Pay Amount Is Higher Than Payable Amount");

        if (totalPayAmount == bill.NetAmount)
            bill.BillStatus = (short)BillStatusEnum.Full;
        else
            bill.BillStatus = (short)BillStatusEnum.Partial;

        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _iTranPaymentRepository.AddAsync(model);
        await _iUnitOfWork.CompleteAsync();

        Repository.Update(bill);

        var isAdded = await _iUnitOfWork.CompleteAsync();
        if (!isAdded) { return false; }

        ts.Complete();
        return true;
    }

    #endregion

    #region GetBillDetailsByBillId

    public async Task<List<PrServicesBillDetailVm>> GetBillDetailsByBillIdAsync(long billId)
    {
        var dataList = await _iPrServicesBillDetailRepository.GetBillDetailsByBillIdAsync(billId);
        return dataList;
    }

    #endregion

    #region TaskProviderAssign

    #endregion
}
