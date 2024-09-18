using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class PrCustomer : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(30)]
    public string RegistrationNo { get; set; }
    public DateTime RegistrationDate { get; set; }

    [Required]
    [StringLength(150)]
    public string CustomerName { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(30)]
    public string Mobile { get; set; }

    [StringLength(250)]
    public string Address { get; set; }
    
    [StringLength(120)]
    public string PhotoUrl { get; set; }

    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

    //--------------FK--------------

    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
