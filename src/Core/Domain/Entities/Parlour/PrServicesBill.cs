using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServicesBill : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(30)]
    public string ServiceNo { get; set; }
    public DateTime ServiceDate { get; set; }
    public DateTime? BookingDate { get; set; }

    [StringLength(350)]
    public string BookingRemarks { get; set; }
    public double TotalBill { get; set; }
    public double Discount { get; set; }
    public double VAT { get; set; }
    public double Tax { get; set; }
    public double NetAmount { get; set; }
    public short ServiceStatus { get; set; } // 0 = Booking, 1=Running, 2= Completed
    public short BillStatus { get; set; } // 0 = Not Paid, 1 = Partial, 2 = Full
    public DateTime? CompleteTime { get; set; }

    [StringLength(350)]
    public string CompletedRemarks { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long CustomerId { get; set; }
    public PrCustomer Customer { get; set; }
    public long ServiceShiftId { get; set; }
    public PrShift ServiceShift { get; set; }
    public long EntryById { get; set; }
    public ApplicationUser EntryBy { get; set; }
    public long? CompletedById { get; set; }
    public ApplicationUser CompletedBy { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
    public ICollection<PrServicesBillDetail> PrServicesBillDetails { get; set; }
}
