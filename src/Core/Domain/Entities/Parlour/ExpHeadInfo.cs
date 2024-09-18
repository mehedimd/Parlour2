using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class ExpHeadInfo : IAuditable
{
    public long Id { get; set; }
    public string HeadName { get; set; }
    public string HeadCode { get; set; }

    [StringLength(1)]
    public string ExpType { get; set; } // D=Daily, M=Monthly, E=Event    

    [StringLength(350)]
    public string Remarks { get; set; }
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
