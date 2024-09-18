using Domain.Entities.Parlour;
using Domain.ViewModel.PrServicesBillDetail;
using Domain.ViewModel.TranPayment;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.ViewModel.PrServicesBill;

public class PrServicesBillVm
{
    public long Id { get; set; }
    public string ServiceNo { get; set; }
    public DateTime ServiceDate { get; set; }
    public string ServiceDateStr { get; set; }
    public DateTime? BookingDate { get; set; }
    public string BookingRemarks { get; set; }
    public double TotalBill { get; set; }
    public double Discount { get; set; }
    public double VAT { get; set; }
    public double Tax { get; set; }
    public double NetAmount { get; set; }
    public short ServiceStatus { get; set; } // 0 = Booking, 1=Running, 2= Completed
    public short BillStatus { get; set; } // 0 = Not Paid, 1 = Partial, 2 = Full
    public DateTime? CompleteTime { get; set; }
    public string CompletedRemarks { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerMobile { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public long ServiceShiftId { get; set; }
    public string ServiceShiftName { get; set; }
    public long EntryById { get; set; }
    public string EntryByName { get; set; }
    public long? CompletedById { get; set; }
    public IEnumerable<SelectListItem> ShiftLookUp { get; set; }
    public IEnumerable<SelectListItem> PrCategoryLookUp { get; set; }
    public IEnumerable<SelectListItem> PayModeLookUp { get; set; }
    public ICollection<PrServicesBillDetailVm> PrServicesBillDetails { get; set; }

    public long? PrCategoryId { get; set; }
    public string PaidDateStr { get; set; }
    public double PaidAmount { get; set; }
    public string PayMode { get; set; }
    public ICollection<TranPaymentVm> PaymentList { get; set; }
    public IEnumerable<SelectListItem> PrServiceLookUp { get; set; }
}
