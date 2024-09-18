using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DapperModel;
using Repository;
using Repository.UnitOfWork;
using Services;
using Utility.CachingUtility;

namespace Utility.Factory;

public class DependencyResolver
{
    public void SetDependencyConfiguration(IServiceCollection services)
    {
        //var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AppAutoMapperProfile()); });
        //services.AddSingleton(mappingConfig.CreateMapper());

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
        services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddTransient<IApplicationUserService, ApplicationUserService>();
        services.AddTransient<IApplicationRoleService, ApplicationRoleService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddTransient<DropdownService, DropdownService>();
        services.AddTransient<CacheStoreService, CacheStoreService>();

        services.AddTransient<IAutoCodeRepository, AutoCodeRepository>();

        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<IDepartmentRepository, DepartmentRepository>();

        services.AddTransient<IDesignationService, DesignationService>();
        services.AddTransient<IDesignationRepository, DesignationRepository>();

        services.AddTransient<IBranchRepository, BranchRepository>();

        #region HRMS

        services.AddTransient<IHrSettingService, HrSettingService>();
        services.AddTransient<IHrSettingRepository, HrSettingRepository>();

        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        services.AddTransient<IEmpEducationService, EmpEducationService>();
        services.AddTransient<IEmpEducationRepository, EmpEducationRepository>();

        services.AddTransient<IEmpExperienceService, EmpExperienceService>();
        services.AddTransient<IEmpExperienceRepository, EmpExperienceRepository>();

        services.AddTransient<IEmpReferenceService, EmpReferenceService>();
        services.AddTransient<IEmpReferenceRepository, EmpReferenceRepository>();

        services.AddTransient<IEmpTrainingService, EmpTrainingService>();
        services.AddTransient<IEmpTrainingRepository, EmpTrainingRepository>();

        services.AddTransient<IEmpPostingService, EmpPostingService>();
        services.AddTransient<IEmpPostingRepository, EmpPostingRepository>();

        services.AddTransient<IEmpDisciplinaryRepository, EmpDisciplinaryRepository>();
        services.AddTransient<IEmpDisciplinaryService, EmpDisciplinaryService>();

        services.AddTransient<IEmpJournalService, EmpJournalService>();
        services.AddTransient<IEmpJournalRepository, EmpJournalRepository>();

        services.AddTransient<IEmpAttendanceService, EmpAttendanceService>();
        services.AddTransient<IEmpAttendanceRepository, EmpAttendanceRepository>();

        services.AddTransient<ISetHolidayService, SetHolidayService>();
        services.AddTransient<ISetHolidayRepository, SetHolidayRepository>();

        #endregion

        #region Leave

        services.AddTransient<ILeaveTypeService, LeaveTypeService>();
        services.AddTransient<ILeaveTypeRepository, LeaveTypeRepository>();

        services.AddTransient<ILeaveSetupService, LeaveSetupService>();
        services.AddTransient<ILeaveSetupRepository, LeaveSetupRepository>();

        services.AddTransient<ILeaveCfRepository, LeaveCfRepository>();
        services.AddTransient<ILeaveCfService, LeaveCfService>();

        services.AddTransient<IEmpLeaveApplicationService, EmpLeaveApplicationService>();
        services.AddTransient<IEmpLeaveApplicationRepository, EmpLeaveApplicationRepository>();

        #endregion

        #region Parlour
        services.AddTransient<IPrCustomerRepository, PrCustomerRepository>();
        services.AddTransient<IPrCustomerService, PrCustomerService>();

        services.AddTransient<IPrServicesBillService, PrServicesBillService>();
        services.AddTransient<IPrServicesBillRepository, PrServicesBillRepository>();

        services.AddTransient<IPrServicesBillDetailRepository, PrServicesBillDetailRepository>();

        services.AddTransient<IPrServiceCategoryService, PrServiceCategoryService>();
        services.AddTransient<IPrServiceCategoryRepository, PrServiceCategoryRepository>();

        services.AddTransient<IPrServiceInfoService, PrServiceInfoService>();
        services.AddTransient<IPrServiceInfoRepository, PrServiceInfoRepository>();

        services.AddTransient<ITranPaymentRepository, TranPaymentRepository>();
        #endregion
    }
}