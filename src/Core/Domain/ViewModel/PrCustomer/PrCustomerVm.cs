using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.PrCustomer;

public class PrCustomerVm
{
    public long Id { get; set; }

    [StringLength(30)]
    public string RegistrationNo { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string RegistrationDateStr { get; set; }

    [Required]
    [StringLength(150)]
    public string CustomerName { get; set; }

    [StringLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Please Provide 11 Digit Mobile Number")]
    public string Mobile { get; set; }

    [StringLength(250)]
    public string Address { get; set; }

    [StringLength(120)]
    public string PhotoUrl { get; set; }

    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<SelectListItem> BranchListLookup { get; set; }

    //--------------FK--------------
    [Required(ErrorMessage = "Branch name is required")]
    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public long ActionById { get; set; }
    public long? UpdatedById { get; set; }
}