using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ViewModel.PrServiceInfo;

public class PrServiceInfoVm
{
    public long Id { get; set; }
    [Required]
    public string ServiceName { get; set; }
    [Required]
    public string ServiceCode { get; set; }
    public string Description { get; set; }
    [Required]
    public double Rate { get; set; }
    public string PhotoUrl { get; set; }
    public bool IsEnable { get; set; }
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem> ServiceCategoryLookUp { get; set; }

}
