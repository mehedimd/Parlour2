using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrServiceCategory : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; }

    [Required]
    [StringLength(30)]
    public string CategoryCode { get; set; }

    [StringLength(350)]
    public string Description { get; set; }
    
    [StringLength(120)]
    public string PhotoUrl { get; set; }
    public bool IsEnable { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
