using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServicesHst : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    public string ServiceName { get; set; }

    [Required]
    [StringLength(30)]
    public string ServiceCode { get; set; }

    [StringLength(350)]
    public string Description { get; set; }
    public double Rate { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long ServiceId { get; set; }
    public PrServiceInfo ServiceInfo { get; set; }
    public long CategoryId { get; set; }
    public PrServiceCategory Category { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
