namespace Domain.ViewModel.TranPayment;

public class TranPaymentVm
{
    public long Id { get; set; }
    public string TranNo { get; set; }
    public string TranType { get; set; } // P=Payment, R=Receive
    public DateTime TranDate { get; set; }
    public string PayMode { get; set; } // COD, Bkash , Cheque, Cash
    public double Amount { get; set; }
    public double Discount { get; set; }
    public string Remarks { get; set; }
    public string ChequeNo { get; set; }
    public string AccountNo { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

    public long BranchId { get; set; }
    public string BranchName { get; set; }
    public long? ServiceBillId { get; set; }
    public string ServiceBillNo { get; set; }
    public long? ExpHeadId { get; set; }
    public string ExpHeadName { get; set; }
    public long? SalesId { get; set; }
}
