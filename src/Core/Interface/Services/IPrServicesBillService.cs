using Domain.Dto;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServicesBill;
using Interface.Base;

namespace Interface.Services;

public interface IPrServicesBillService : IService<PrServicesBill>
{
    Task<DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm>> 
        SearchAsync(DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm> model);
    Task<string> GetInvoiceNo();
    Task<(bool, long)> BillGenerate(PrServicesBillVm vm);
    Task<List<PrServiceDto>> GetServiceByCategoryId(long? categoryId);
    Task<PrServicesBillVm> GetBillInfoById(long billId);
    Task<string> GetServiceBillByIdAsyncHtml(long id);
    Task<bool> BillServiceAddAsync(SavePrServiceBillDetailDto dto);
    Task<bool> PaymentEntry(ServicePaymentDto payment);
}
