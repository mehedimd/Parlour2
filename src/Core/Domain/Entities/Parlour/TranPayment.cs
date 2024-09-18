using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Parlour;

public class TranPayment : IAuditable
{
    public long Id { get; set; }

    [StringLength(20)]
    public string TranNo { get; set; }

    [StringLength(1)]
    public string TranType { get; set; } // P=Payment, R=Receive
    public DateTime TranDate { get; set; }

    [StringLength(15)]
    public string PayMode { get; set; } // COD, Bkash , Cheque, Cash
    public double Amount { get; set; }
    public double Discount { get; set; }

    [StringLength(350)]
    public string Remarks { get; set; }

    [StringLength(150)]
    public string ChequeNo { get; set; }

    [StringLength(150)]
    public string AccountNo { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }


    //--------------FK--------------

    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long? ServiceBillId { get; set; }
    public PrServicesBill ServiceBill { get; set; } 
    public long? ExpHeadId { get; set; }
    public ExpHeadInfo ExpHead { get; set; }
    public long? SalesId { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
