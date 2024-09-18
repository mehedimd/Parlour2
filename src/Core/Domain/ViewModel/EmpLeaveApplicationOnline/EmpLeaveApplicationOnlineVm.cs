﻿using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.EmpLeaveApplicationOnline
{
    public class EmpLeaveApplicationOnlineVm
    {
        public long Id { get; set; }

        [StringLength(60)]
        public string ApplicationNo { get; set; }
        public DateTime FromDate { get; set; }
        public string FromDateStr { get; set; }
        public DateTime ToDate { get; set; }
        public string ToDateStr { get; set; }
        public DateTime? AprFromDate { get; set; }
        public DateTime? AprToDate { get; set; }
        public DateTime? ActualFromDate { get; set; }
        public DateTime? ActualToDate { get; set; }
        public DateTime SubmitDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }
        public short TotalApprovalLeave { get; set; }
        public short SalaryType { get; set; } // F=Full Salary, H=Half, W=Without
        public DateTime? CancelDate { get; set; }

        [StringLength(150)]
        public string CancelReason { get; set; }
        public short Status { get; set; } // 0 = SUBMISSION, 1 = FIRST REVIEW, 2 = SECOND REVIEW, 3 = THIRD REVIEW …. ,99 = FINAL APPROVE, 98 = REJECT, 97 = SELF CANCEL

        [StringLength(120)]
        public string FileDoc { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        // ---- FK ----

        public long EmployeeId { get; set; }
        public long LeaveTypeId { get; set; }
        public long SubmitById { get; set; }

    }
}
