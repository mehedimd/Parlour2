using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServiceInfo : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(250)]
    public string ServiceName { get; set; }

    [Required]
    [StringLength(30)]
    public string ServiceCode { get; set; }

    [StringLength(350)]
    public string Description { get; set; }
    public double Rate { get; set; }

    [StringLength(120)]
    public string PhotoUrl { get; set; }
    public bool IsEnable { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPackage { get; set; } = false;


    //--------------FK--------------

    public long CategoryId { get; set; }
    public PrServiceCategory Category { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
