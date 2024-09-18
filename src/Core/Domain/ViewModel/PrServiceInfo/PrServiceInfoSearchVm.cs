using Domain.ModelInterface;

namespace Domain.ViewModel.PrServiceInfo;

public class PrServiceInfoSearchVm : IDataTableSearch
{
    public long Id { get; set; }
    public string ServiceName { get; set; }
    public string ServiceCode { get; set; }
    public string Description { get; set; }
    public double Rate { get; set; }
    public string PhotoUrl { get; set; }
    public bool IsEnable { get; set; }
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    public long UserId { get; set; }
    public int SerialNo { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanView { get; set; }
    public bool CanDelete { get; set; }
}
