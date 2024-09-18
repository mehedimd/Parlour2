using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Admin;

public class Branch : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(120)]
    public string BranchName { get; set; }

    [Required]
    [StringLength(100)]
    public string BranchCode { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(50)]
    public string Mobile { get; set; }

    [StringLength(100)]
    public string Address { get; set; }

    [StringLength(250)]
    public string Remarks { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

    //------------------FK-------------------------

    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
