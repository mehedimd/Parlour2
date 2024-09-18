namespace WebMVC.Models.IdentityModels;

public static class Permissions
{
    public const string SuperAdmin = "Permissions.SuperAdmin";

    public static class Module
    {
        public const string AdminModule = "Permissions.Module.AdminModule";
    }

    public static class Users
    {
        public const string ListView = "Permissions.Users.ListView";
        public const string Create = "Permissions.Users.Create";
    }

    public static class UserRoles
    {
        public const string ListView = "Permissions.UserRoles.ListView";
        public const string CreateOrEdit = "Permissions.UserRoles.CreateOrEdit";
    }

    public static class Departments
    {
        public const string ListView = "Permissions.Departments.ListView";
        public const string ListViewAcademicDepartment = "Permissions.Departments.ListViewAcademicDepartment";
        public const string Create = "Permissions.Departments.Create";
        public const string CreateAcademicDepartment = "Permissions.Departments.CreateAcademicDepartment";
        public const string Edit = "Permissions.Departments.Edit";
        public const string EditAcademicDepartment = "Permissions.Departments.EditAcademicDepartment";
        public const string Delete = "Permissions.Departments.Delete";
        public const string DeleteAcademicDepartment = "Permissions.Departments.DeleteAcademicDepartment";
    }

    public static class Designations
    {
        public const string ListView = "Permissions.Designations.ListView";
        public const string Create = "Permissions.Designations.Create";
        public const string Edit = "Permissions.Designations.Edit";
        public const string Delete = "Permissions.Designations.Delete";
    }

    public static class HrSettings
    {
        public const string Edit = "Permissions.HrSettings.Edit";
        public const string DetailsView = "Permissions.HrSettings.DetailsView";
    }

    public static class Employees
    {
        public const string ListView = "Permissions.Employees.ListView";
        public const string DetailsView = "Permissions.Employees.DetailsView";
        public const string Create = "Permissions.Employees.Create";
        public const string Edit = "Permissions.Employees.Edit";
        public const string ReportView = "Permissions.Employees.ReportView";
        public const string Disable = "Permissions.Employees.Disable";
    }

    public static class EmpAttendances
    {
        public const string Create = "Permissions.EmpAttendances.Create";
        public const string ReportView = "Permissions.EmpAttendances.ReportView";
    }

    public static class EmpLeaveApplications
    {
        public const string ListView = "Permissions.EmpLeaveApplications.ListView";
        public const string DetailsView = "Permissions.EmpLeaveApplications.DetailsView";
        public const string Create = "Permissions.EmpLeaveApplications.Create";
        public const string Edit = "Permissions.EmpLeaveApplications.Edit";
        public const string Delete = "Permissions.EmpLeaveApplications.Delete";
        public const string ReportView = "Permissions.EmpLeaveApplications.ReportView";
        public const string StatementView = "Permissions.EmpLeaveApplications.StatementView";
    }

    public static class LeaveSetups
    {
        public const string ListView = "Permissions.LeaveSetups.ListView";
        public const string Create = "Permissions.LeaveSetups.Create";
        public const string Edit = "Permissions.LeaveSetups.Edit";
        public const string Delete = "Permissions.LeaveSetups.Delete";
    }

    public static class LeaveTypes
    {
        public const string ListView = "Permissions.LeaveTypes.ListView";
        public const string Create = "Permissions.LeaveTypes.Create";
        public const string Edit = "Permissions.LeaveTypes.Edit";
        public const string Delete = "Permissions.LeaveTypes.Delete";
    }
    public static class CfLeave
    {
        public const string ListView = "Permissions.CfLeave.ListView";
        public const string Create = "Permissions.CfLeave.Create";
        public const string Edit = "Permissions.CfLeave.Edit";
        public const string Delete = "Permissions.CfLeave.Delete";
    }

    public static class SetHolidays
    {
        public const string ListView = "Permissions.SetHolidays.ListView";
        public const string Create = "Permissions.SetHolidays.Create";
        public const string Edit = "Permissions.SetHolidays.Edit";
        public const string Delete = "Permissions.SetHolidays.Delete";
    }

    public static class PrServicesBills
    {
        public const string ListView = "Permissions.PrServicesBills.ListView";
        public const string Create = "Permissions.PrServicesBills.Create";
        public const string Details = "Permissions.PrServicesBills.Details";
    }
}