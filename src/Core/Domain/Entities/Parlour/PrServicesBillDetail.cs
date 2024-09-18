using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServicesBillDetail : IAuditable
{
    public long Id { get; set; }
    public int Qty { get; set; }
    public double Rate { get; set; }
    public double Amount { get; set; }
    public double Discount { get; set; }
    public double VAT { get; set; }
    public double Tax { get; set; }
    public double NetAmount { get; set; }
    public short ServiceStatus { get; set; } // 0 = Booking, 1=Running, 2= Completed
    
    [StringLength(350)]
    public string AssignRemarks { get; set; }

    [StringLength(350)]
    public string SpRemaks { get; set; }
    public string CustomerRemarks { get; set; }
    public int CustomerRating { get; set; } // 0-5
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long ServiceBillId { get; set; }
    public PrServicesBill ServiceBill { get; set; }
    public long ServiceId { get; set; }
    public PrServiceInfo Service { get; set; }
    public long? ProviderId { get; set; }
    public Employee Provider { get; set; }
    public long? AssignById { get; set; }
    public ApplicationUser AssignBy { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
