using Domain.ModelInterface;

namespace Domain.ViewModel.EmpLeaveReviewer
{
    public class EmpLeaveReviewerSearchVm : IDataTableSearch
    {
        public long Id { get; set; }
        //---------------------FK----------------
        public long DepartmentId { get; set; }
        public long? EmployeeId { get; set; }
        public long FirstReviewerId { get; set; }
        public long FinalReviewerId { get; set; }
        public long UserId { get; set; }
        public int SerialNo { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanView { get; set; }
        public bool CanDelete { get; set; }
    }
}
