using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServiceSpMap : IAuditable
{
    public long Id { get; set; }
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

    public long ServiceBillDetailId { get; set; }
    public PrServicesBillDetail ServiceBillDetail { get; set; }
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
