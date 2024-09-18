using Domain.ConfigurationModel;
using Domain.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel.EmpLeaveApplication
{
    public class EmpLeaveApplicationVm
    {
        public long Id { get; set; }

        [DisplayName("From Date: ")]
        public string FromDateStr { get; set; }
        public DateTime FromDate { get; set; }

        [DisplayName("To Date: ")]
        public string ToDateStr { get; set; }
        public DateTime ToDate { get; set; }

        [DisplayName("Submit Date: ")]
        public string SubmitDateStr { get; set; }
        public DateTime SubmitDate { get; set; }

        [StringLength(500, ErrorMessage = "Reason can not be more than 500 characters")]
        [DisplayName("Reason: ")]
        public string Reason { get; set; }

        [DisplayName("Status: ")]
        public int Status { get; set; } // 0 = Pending, 1 = Approve, 2 = Rejected, 3 = Cancel
        public string StatusText => Status switch { 0 => "Pending", 1 => "Approve", 2 => "Rejected", 3 => "Cancel", _ => "" };

        [StringLength(120, ErrorMessage = "File Doc can not be more than 120 characters")]
        [DisplayName("File Doc: ")]
        public string FileDoc { get; set; }

        [DisplayName("Review Date: ")]
        public string ReviewDateStr { get; set; }
        public DateTime? ReviewDate { get; set; }

        [StringLength(200, ErrorMessage = "Review Remarks can not be more than 200 characters")]
        [DisplayName("Review Remarks: ")]
        public string ReviewRemarks { get; set; }

        [DisplayName("Approve Date: ")]
        public string ApproveDateStr { get; set; }
        public DateTime? ApproveDate { get; set; }

        [StringLength(200, ErrorMessage = "Approval Remarks can not be more than 200 characters")]
        [DisplayName("Approval Remarks: ")]
        public string ApprovalRemarks { get; set; }
        public short Length { get; set; }
        public short ApproveLength { get; set; }

        //----------------------FK--------------------
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmpDesignation { get; set; }
        public string EmpPhotoUrl { get; set; }
        public long LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public int? LeaveTypeBalance { get; set; }
        public int LeaveTaken { get; set; }
        public long SubmitById { get; set; }
        public string SubmitByName { get; set; }
        public long? ReviewById { get; set; }
        public string ReviewByName { get; set; }
        public long? ApprovedById { get; set; }
        public string ApprovedByName { get; set; }
        public long ActionById { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdatedById { get; set; }
        public IEnumerable<SelectListItem> EmployeeLookUp { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeLookUp { get; set; }

        #region File Upload

        public IFormFile LeaveFile { get; set; }

        public AppFile AppFile { get; set; }

        public string FolderPath => $"\\EmployeeLeaveApplication";

        public AppFile GetLeaveApplicationFileToUploadFolder()
        {
            if (AppFile != null) { return AppFile; }

            var fileUpload = new AppFileUploadHelper
            {
                FormFile = LeaveFile?.IsNullOrEmpty() == true ? null : LeaveFile,
                FolderPath = FolderPath,
                FileNamePrefix = "EMPLEAVEAPP"
            };
            AppFile = Utility.Utility.ConvertFormFileToAppFile(fileUpload)?.FirstOrDefault();
            if (LeaveFile != null) FileDoc = Utility.Utility.GetFileSmallPath(AppFile?.FileUrl);
            return AppFile;
        }

        #endregion
    }
}
