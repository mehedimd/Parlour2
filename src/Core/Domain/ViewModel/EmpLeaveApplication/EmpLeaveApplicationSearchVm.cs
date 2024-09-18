using Domain.ModelInterface;

namespace Domain.ViewModel.EmpLeaveApplication
{
    public class EmpLeaveApplicationSearchVm : IDataTableSearch
    {
        public long Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; } // 0 = Pending, 1 = Approve, 2 = Reject, 3 = Cancel
        public string StatusText => Status switch { 0 => "Pending", 1 => "Approve", 2 => "Rejected", 3 => "Cancel", _ => "" };
        public string FileDoc { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewRemarks { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApprovalRemarks { get; set; }

        //---------------FK-------------------
        public long EmployeeId { get; set; }
        public long LeaveTypeId { get; set; }
        public long SubmitById { get; set; }
        public long? ReviewById { get; set; }
        public long? ApprovedById { get; set; }
        public long UserId { get; set; }
        public int SerialNo { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveTypeName { get; set; }
    }
}
