using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Admin;

public class Department : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [StringLength(120)]
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
