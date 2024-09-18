using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.PrServiceCategory;

public class PrServiceCategoryVm
{
    public long Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryCode { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public bool IsEnable { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public long ActionById { get; set; }
    public long? UpdatedById { get; set; }
}
