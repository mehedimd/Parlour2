using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.Entities.Parlour;
using Domain.ViewModel.Department;
using Domain.ViewModel.Designation;
using Domain.ViewModel.EmpAttendence;
using Domain.ViewModel.EmpDisciplinary;
using Domain.ViewModel.EmpEducation;
using Domain.ViewModel.EmpExperience;
using Domain.ViewModel.EmpJournal;
using Domain.ViewModel.EmpLeaveApplication;
using Domain.ViewModel.Employees;
using Domain.ViewModel.EmpPosting;
using Domain.ViewModel.EmpReference;
using Domain.ViewModel.EmpTraining;
using Domain.ViewModel.HrSetting;
using Domain.ViewModel.LeaveCf;
using Domain.ViewModel.LeaveSetup;
using Domain.ViewModel.LeaveType;
using Domain.ViewModel.PrCustomer;
using Domain.ViewModel.PrServiceCategory;
using Domain.ViewModel.PrServiceInfo;
using Domain.ViewModel.PrServicesBill;
using Domain.ViewModel.PrServicesBillDetail;
using Domain.ViewModel.Role;
using Domain.ViewModel.SetHoliday;
using Domain.ViewModel.TranPayment;
using Domain.ViewModel.User;
using Domain.ViewModel.UserAccount;

namespace Utility.Factory;

public class AppAutoMapperProfile : Profile
{
    public AppAutoMapperProfile()
    {
        #region Config

        CreateMap<RegisterViewModel, ApplicationUser>();

        CreateMap<ApplicationUser, UserSearchVm>();

        CreateMap<ApplicationRole, RoleSearchVm>();

        CreateMap<Department, DepartmentVm>();
        CreateMap<DepartmentVm, Department>();
        CreateMap<Department, DepartmentSearchVm>();
        CreateMap<DepartmentSearchVm, Department>();

        CreateMap<DesignationVm, Designation>();
        CreateMap<Designation, DesignationVm>();
        CreateMap<Designation, DesignationSearchVm>();


        #region HRMS

        CreateMap<HrSetting, HrSettingVm>();
        CreateMap<HrSettingVm, HrSetting>();

        CreateMap<EmployeeVm, Employee>();
        CreateMap<Employee, EmployeeVm>();
        CreateMap<Employee, EmployeeSearchVm>();

        CreateMap<EmpEducationVm, EmpEducation>();
        CreateMap<EmpEducation, EmpEducationVm>();

        CreateMap<EmpExperienceVm, EmpExperience>();
        CreateMap<EmpExperience, EmpExperienceVm>();

        CreateMap<EmpReferenceVm, EmpReference>();
        CreateMap<EmpReference, EmpReferenceVm>();

        CreateMap<EmpTrainingVm, EmpTraining>();
        CreateMap<EmpTraining, EmpTrainingVm>();

        CreateMap<EmpPostingVm, EmpPosting>();
        CreateMap<EmpPosting, EmpPostingVm>();

        CreateMap<EmpDisciplinaryVm, EmpDisciplinary>();
        CreateMap<EmpDisciplinary, EmpDisciplinaryVm>();

        CreateMap<EmpJournalVm, EmpJournal>();
        CreateMap<EmpJournal, EmpJournalVm>();

        CreateMap<EmpAttendanceVm, EmpAttendance>();
        CreateMap<EmpAttendance, EmpAttendanceVm>();
        CreateMap<EmpAttendance, EmpAttendanceSearchVm>();

        CreateMap<LeaveTypeVm, LeaveType>();
        CreateMap<LeaveType, LeaveTypeVm>();
        CreateMap<LeaveType, LeaveTypeSearchVm>();

        CreateMap<LeaveSetupVm, LeaveSetup>();
        CreateMap<LeaveSetup, LeaveSetupVm>();
        CreateMap<LeaveSetup, LeaveSetupSearchVm>();

        CreateMap<EmpLeaveApplicationVm, EmpLeaveApplication>();
        CreateMap<EmpLeaveApplication, EmpLeaveApplicationVm>();
        CreateMap<EmpLeaveApplication, EmpLeaveApplicationSearchVm>();

        CreateMap<SetHolidayVm, SetHoliday>();
        CreateMap<SetHoliday, SetHolidayVm>();
        CreateMap<SetHoliday, SetHolidaySearchVm>();

        CreateMap<LeaveCf, LeaveCfVm>();
        CreateMap<LeaveCfVm, LeaveCf>();
        CreateMap<LeaveCf, LeaveCfSearchVm>();

        #endregion

        #region Parlour

        CreateMap<PrCustomerVm, PrCustomer>();
        CreateMap<PrCustomer, PrCustomerVm>();
        CreateMap<PrCustomer, PrCustomerSearchVm>();

        CreateMap<PrServiceCategory, PrServiceCategoryVm>();
        CreateMap<PrServiceCategoryVm, PrServiceCategory>();
        CreateMap<PrServiceCategorySearchVm, PrServiceCategory>();
        CreateMap<PrServiceCategory, PrServiceCategorySearchVm>();

        CreateMap<PrServiceInfo, PrServiceInfoVm>();
        CreateMap<PrServiceInfoVm, PrServiceInfo>();
        CreateMap<PrServiceInfoSearchVm, PrServiceInfo>();
        CreateMap<PrServiceInfo, PrServiceInfoSearchVm>();

        CreateMap<PrServicesBillVm, PrServicesBill>();
        CreateMap<PrServicesBill, PrServicesBillVm>();
        CreateMap<PrServicesBill, PrServicesBillSearchVm>();

        CreateMap<PrServicesBillDetailVm, PrServicesBillDetail>();
        CreateMap<PrServicesBillDetail, PrServicesBillDetailVm>();

        CreateMap<SavePrServiceBillDetailDto, PrServicesBillDetail>();

        CreateMap<TranPaymentVm, TranPayment>();
        CreateMap<TranPayment, TranPaymentVm>();

        CreateMap<ServicePaymentDto, TranPayment>();
        #endregion

        #endregion
    }
}
