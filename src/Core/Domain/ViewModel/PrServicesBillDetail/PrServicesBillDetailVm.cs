namespace Domain.ViewModel.PrServicesBillDetail;

public class PrServicesBillDetailVm
{
    public long Id { get; set; }
    public int Qty { get; set; }
    public double Rate { get; set; }
    public double Amount { get; set; }
    public double Discount { get; set; }
    public double VAT { get; set; }
    public double Tax { get; set; }
    public double NetAmount { get; set; }
    public short ServiceStatus { get; set; } // 0=Pending, 1=Running, 2=Completed
    public string AssignRemarks { get; set; }
    public string SpRemaks { get; set; }
    public string CustomerRemarks { get; set; }
    public int CustomerRating { get; set; } // 0-5
    public long ServiceBillId { get; set; }
    public string ServiceBillNo { get; set; }
    public long ServiceId { get; set; }
    public string ServiceName { get; set; }
    public long? ProviderId { get; set; }
    public string ProviderName { get; set; }
    public long? AssignById { get; set; }
    public string AssignByName { get; set; }
}
