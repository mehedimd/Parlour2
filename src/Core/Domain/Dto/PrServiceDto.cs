namespace Domain.Dto;

public class PrServiceDto
{
    public long Id { get; set; }
    public string ServiceName { get; set; }
    public string ServiceCode { get; set; }
    public double Rate { get; set; }
}

public class SavePrServiceBillDetailDto
{
    public int Qty { get; set; }
    public double Rate { get; set; }
    public double Amount { get; set; }
    public long ServiceBillId { get; set; }
    public long ServiceId { get; set; }
}
