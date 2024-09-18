using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.PrCustomer;

public class PrCustomerSearchVm : IDataTableSearch
{
    public long Id { get; set; }
    public string RegistrationNo { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string RegistrationDateStr { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string PhotoUrl { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

    //--------------FK--------------

    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public long ActionById { get; set; }
    public long? UpdatedById { get; set; }
    public long UserId { get; set; }
    public int SerialNo { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanView { get; set; }
    public bool CanDelete { get; set; }
}
