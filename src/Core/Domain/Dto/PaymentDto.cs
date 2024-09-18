namespace Domain.Dto;

public class ServicePaymentDto
{
    public string TranDateStr { get; set; }
    public string PayMode { get; set; } // COD, Bkash , Cheque, Cash
    public double Amount { get; set; }
    public string Remarks { get; set; }
    public string ChequeNo { get; set; }
    public string AccountNo { get; set; }
    public long ServiceBillId { get; set; }
}
