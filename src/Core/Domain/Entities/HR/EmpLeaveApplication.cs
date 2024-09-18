using Domain.Entities.Admin;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class EmpLeaveApplication
{
    public long Id { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime SubmitDate { get; set; }

    [StringLength(500)]
    public string Reason { get; set; }
    public short Status { get; set; } // 0 = Pending, 1 = Approve, 2 = Reject, 3 = Cancel
    public short Length { get; set; }
    public short ApproveLength { get; set; }


    [StringLength(120)]
    public string FileDoc { get; set; }
    public DateTime? ReviewDate { get; set; }

    [StringLength(200)]
    public string ReviewRemarks { get; set; }
    public DateTime? ApproveDate { get; set; }

    [StringLength(200)]
    public string ApprovalRemarks { get; set; }



    //---------------FK-------------------
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public long LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; }

    public long SubmitById { get; set; }
    public ApplicationUser SubmitBy { get; set; }

    public long? ReviewById { get; set; }
    public ApplicationUser ReviewBy { get; set; }

    public long? ApprovedById { get; set; }
    public ApplicationUser ApprovedBy { get; set; }

    public long BranchId { get; set; }
    public Branch Branch { get; set; }

    public ApplicationUser ActionBy { get; set; }
    public long ActionById { get; set; }
    public DateTime ActionDate { get; set; }
    public bool IsDeleted { get; set; }
}
