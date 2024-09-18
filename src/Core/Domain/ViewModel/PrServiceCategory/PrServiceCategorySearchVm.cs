using Domain.ModelInterface;

namespace Domain.ViewModel.PrServiceCategory;

public class PrServiceCategorySearchVm : IDataTableSearch
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
    public long UserId { get; set; }
    public int SerialNo { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanView { get; set; }
    public bool CanDelete { get; set; }
}
