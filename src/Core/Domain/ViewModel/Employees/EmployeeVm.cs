using Domain.ConfigurationModel;
using Domain.Utility;
using Domain.ViewModel.EmpLeaveApplication;
using Domain.ViewModel.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ViewModel.Employees;

public class EmployeeVm
{
    public long Id { get; set; }

    [DisplayName("Name: *")]
    [StringLength(90, ErrorMessage = "Name can not be more than 90 characters")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }


    [StringLength(30, ErrorMessage = "Code can not be more than 30 characters")]
    [Required(ErrorMessage = "Code is required")]
    [DisplayName("Code: *")]
    public string Code { get; set; }

    [StringLength(35, ErrorMessage = "Email can not be more than 35 characters")]
    [DisplayName("Email: ")]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "Mobile can not be more than 20 characters")]
    [DisplayName("Mobile: ")]
    public string Mobile { get; set; }
    public DateTime JoinDate { get; set; }

    [Required]
    public string JoinDateStr { get; set; }
    public DateTime? RetirementDate { get; set; }

    public string RetirementDateStr { get; set; }
    public DateTime? ProbationDate { get; set; }
    public string ProbationDateStr { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public string ConfirmationDateStr { get; set; }

    [StringLength(120)]
    public string PhotoUrl { get; set; }

    [StringLength(120)]
    public string SignatureUrl { get; set; }

    [StringLength(120)]
    public string NidUrl { get; set; }

    [StringLength(40, ErrorMessage = "NID can not be more than 40 characters")]
    public string NID { get; set; }


    public DateTime? Dob { get; set; }
    public string DobStr { get; set; }

    [StringLength(1)]
    [Required(ErrorMessage = "Gender is required")]
    [DisplayName("Gender: *")]
    public string Gender { get; set; } //M = Male, F = Female
    public string GenderText => Gender switch { "M" => "Male", "F" => "Female", _ => "--" };


    [StringLength(50, ErrorMessage = "Nationality can not be more than 50 characters")]
    [DisplayName("Nationality: ")]
    public string Nationality { get; set; }

    [StringLength(30, ErrorMessage = "Religion can not be more than 30 characters")]
    [DisplayName("Religion: *")]
    public string Religion { get; set; }
    public string ReligionText => Religion switch { "1" => "Islam", "2" => "Hindu", "3" => "Christian", "4" => "Buddist", "5" => "Other", _ => "--" };

    [StringLength(20, ErrorMessage = "Blood Group can not be more than 20 characters")]
    [DisplayName("Blood Group: ")]
    public string BloodGroup { get; set; }


    [StringLength(1)]
    [DisplayName("Marital Status")]
    public string MaritalStatus { get; set; } // U = Unmaried, M = Married
    public string MaritalStatusText => MaritalStatus switch { "U" => "Unmarried", "M" => "Married", _ => "--" };

    [StringLength(60, ErrorMessage = "Fater name can not be more than 60 characters")]
    [DisplayName("Father Name: ")]
    public string FatherName { get; set; }


    [StringLength(60, ErrorMessage = "Mother name can not be more than 60 characters")]
    [DisplayName("Mother Name: ")]
    public string MotherName { get; set; }

    [StringLength(60, ErrorMessage = "Spouse Name can not be more than 60 characters")]
    [DisplayName("Spouse Name: ")]
    public string SpouseName { get; set; }
    public int EmployeeStatus { get; set; } // 1 = Permanent, 2 = Contractual, 3 = Adhoc, 4 = Guest
    public string EmployeeStatusText => EmployeeStatus switch { 1 => "Permanent", 2 => "Contractual", 3 => "Adhoc", 4 => "Guest", _ => "--" };

    [StringLength(120, ErrorMessage = "Present Address can not be more than 120 characters")]
    [DisplayName("Present Address: ")]
    public string PreAddress { get; set; }

    [StringLength(120, ErrorMessage = "Permanent Address can not be more than 120 characters")]
    [DisplayName("Permanent Address: ")]
    public string PerAddress { get; set; }

    [StringLength(350)]
    public string Remarks { get; set; }
    public double Salary { get; set; }
    public int SalaryType { get; set; } // 1 = Monthly, 2 = Class Wise
    public string SalaryTypeText => SalaryType switch { 1 => "Monthly", 2 => "Class Wise", _ => "--" };
    
    [StringLength(100)]
    public string BankAccNo { get; set; }
    public bool IsPfMember { get; set; }
    public bool IsEnable { get; set; }
    public string EmpMachineId { get; set; }

    //---------------------------------------------
    public long DesignationId { get; set; }
    public long? DepartmentId { get; set; }
    public long? AcademicDptId { get; set; }
    public long? UserId { get; set; }
    public DateTime ActionDate { get; set; }
    public long ActionById { get; set; }

    [NotMapped]
    public EmpLeaveBalanceReportVm EmpLeaveInfo { get; set; }
    public IEnumerable<SelectListItem> RoleLookUp { get; set; }
    public IEnumerable<SelectListItem> DesignationLookUp { get; set; }
    public IEnumerable<SelectListItem> DepartmentLookUp { get; set; }
    public IEnumerable<SelectListItem> AcademicDepartmentLookUp { get; set; }
    public IEnumerable<SelectListItem> GenderLookUp { get; set; }
    public IEnumerable<SelectListItem> BloodGroupLookUp { get; set; }
    public IEnumerable<SelectListItem> MaritalStatusLookUp { get; set; }
    public IEnumerable<SelectListItem> EmployeeStatusLookUp { get; set; }
    public IEnumerable<SelectListItem> SalaryTypeLookUp { get; set; }
    public IEnumerable<SelectListItem> ReligionLookUp { get; set; }
    public IEnumerable<SelectListItem> EmpRefTypeLookUp { get; set; }
    public IEnumerable<SelectListItem> PostingTypeLookUp { get; set; }
    public IEnumerable<SelectListItem> ActionTypeLookUp { get; set; }

    public RegisterViewModel GetUserModel() => AppUtility.GetUserModel("", Name, Email, Mobile, AppUtility.DefaultPassword, AppUtility.DefaultPassword, PhotoUrl);

    #region File Upload

    public IFormFile PhotoFile { get; set; }
    public IFormFile SignatureFile { get; set; }
    public IFormFile NidFile { get; set; }

    public AppFile AppFile { get; set; }
    public AppFile AppSignatureFile { get; set; }
    public AppFile AppNidFile { get; set; }

    public string FolderPath => $"\\Employee";

    public string DepartmentName { get; set; }
    public string AcademicDepartmentName { get; set; }
    public string DesignationName { get; set; }
    public DateTime? DisableDate { get; set; }
    public string DisableDateStr { get; set; }
    public string DisableReason { get; set; }

    public AppFile GetAppFileToUploadFolder()
    {
        if (AppFile != null) { return AppFile; }

        var fileUpload = new AppFileUploadHelper
        {
            FormFile = PhotoFile?.IsNullOrEmpty() == true ? null : PhotoFile,
            FolderPath = FolderPath,
            FileNamePrefix = "PHOTO"
        };
        AppFile = Utility.Utility.ConvertFormFileToAppFile(fileUpload)?.FirstOrDefault();
        if (PhotoFile != null) PhotoUrl = Utility.Utility.GetFileSmallPath(AppFile?.FileUrl);
        return AppFile;
    }

    public AppFile GetSignatureFileToUploadFolder()
    {
        if (AppSignatureFile != null) { return AppSignatureFile; }

        var fileUpload = new AppFileUploadHelper
        {
            FormFile = SignatureFile?.IsNullOrEmpty() == true ? null : SignatureFile,
            FolderPath = FolderPath,
            FileNamePrefix = "SIGNATURE"
        };
        AppSignatureFile = Utility.Utility.ConvertFormFileToAppFile(fileUpload)?.FirstOrDefault();
        if (SignatureFile != null) SignatureUrl = Utility.Utility.GetFileSmallPath(AppSignatureFile?.FileUrl);
        return AppSignatureFile;
    }

    public AppFile GetNidFileToUploadFolder()
    {
        if (AppNidFile != null) { return AppNidFile; }

        var fileUpload = new AppFileUploadHelper
        {
            FormFile = NidFile?.IsNullOrEmpty() == true ? null : NidFile,
            FolderPath = FolderPath,
            FileNamePrefix = "NID"
        };
        AppNidFile = Utility.Utility.ConvertFormFileToAppFile(fileUpload)?.FirstOrDefault();
        if (NidFile != null) NidUrl = Utility.Utility.GetFileSmallPath(AppNidFile?.FileUrl);
        return AppNidFile;
    }

    #endregion
}
