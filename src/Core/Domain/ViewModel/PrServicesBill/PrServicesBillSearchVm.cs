using Domain.ModelInterface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.ViewModel.PrServicesBill;

public class PrServicesBillSearchVm : IDataTableSearch
{
    public long Id { get; set; }
    public string ServiceNo { get; set; }
    public DateTime ServiceDate { get; set; }
    public DateTime? BookingDate { get; set; }
    public string BookingRemarks { get; set; }
    public double TotalBill { get; set; }
    public double Discount { get; set; }
    public double VAT { get; set; }
    public double Tax { get; set; }
    public double NetAmount { get; set; }
    public short ServiceStatus { get; set; } // 0 = Booking, 1=Running, 2= Completed
    public string ServiceStatusText => ServiceStatus switch { 0 => "Booking", 1 => "Running", 2 => "Completed", _ => "--" };
    public short BillStatus { get; set; } // 0 = Not Paid, 1 = Partial, 2 = Full
    public string BillStatusText => BillStatus switch { 0 => "Pending", 1 => "Partial Payment", 2 => "Full Paid", _ => "--" };
    public DateTime? CompleteTime { get; set; }
    public string CompletedRemarks { get; set; }
    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerMobile { get; set; }
    public long ServiceShiftId { get; set; }
    public string ServiceShiftName { get; set; }
    public long EntryById { get; set; }
    public long EntryByName { get; set; }
    public long? CompletedById { get; set; }
    public long UserId { get; set; }
    public int SerialNo { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanView { get; set; }
    public bool CanDelete { get; set; }
    public string FormDateStr { get; set; }
    public string ToDateStr { get; set; }
    public IEnumerable<SelectListItem> BillStatusLookUp { get; set; }
    public IEnumerable<SelectListItem> CustomerLookUp { get; set; }
}