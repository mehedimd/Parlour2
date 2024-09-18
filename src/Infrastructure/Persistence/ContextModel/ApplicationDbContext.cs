using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Identity;
using Domain.Entities.Parlour;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Persistence.ContextModel;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    #region Config
    public long CurrentUserId { get; set; }
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    #endregion

    #region Db_Entity

    #region Identity

    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<UserLogAttempt> UserLogAttempts { get; set; }
    public DbSet<UserPassChangeHst> PassChangeHsts { get; set; }

    #endregion

    #region Admin

    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<SetCountry> SetCountries { get; set; }
    public DbSet<SetDivision> SetDivisions { get; set; }
    public DbSet<SetDistrict> SetDistricts { get; set; }
    public DbSet<SetPoliceStation> SetPoliceStations { get; set; }
    public DbSet<Branch> Branches { get; set; }

    #endregion

    #region HRMS

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmpEducation> EmpEducations { get; set; }
    public DbSet<EmpExperience> EmpExperiences { get; set; }
    public DbSet<EmpReference> EmpReferences { get; set; }
    public DbSet<EmpTraining> EmpTrainings { get; set; }
    public DbSet<EmpPosting> EmpPostings { get; set; }
    public DbSet<EmpJournal> EmpJournals { get; set; }
    public DbSet<EmpDisciplinary> EmpDisciplinaries { get; set; }
    public DbSet<EmpAttendance> EmpAttendances { get; set; }
    public DbSet<HrSetting> HrSettings { get; set; }

    #endregion

    #region Leave

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveSetup> LeaveSetups { get; set; }
    public DbSet<LeaveCf> LeaveCfs { get; set; }
    public DbSet<SetHoliday> SetHolidays { get; set; }
    public DbSet<EmpLeaveApplication> EmpLeaveApplications { get; set; }

    #endregion

    #region Parlour

    public DbSet<PrCustomer> PrCustomers { get; set; }
    public DbSet<PrServiceCategory> PrServiceCategories { get; set; }
    public DbSet<PrServiceInfo> PrServiceInfos { get; set; }
    public DbSet<PrPackageService> PrPackageServices { get; set; }
    public DbSet<PrServicesHst> PrServicesHsts { get; set; }
    public DbSet<PrShift> PrShifts { get; set; }
    public DbSet<PrServicesBill> PrServicesBills { get; set; }
    public DbSet<PrServicesBillDetail> PrServicesBillDetails { get; set; }
    public DbSet<PrServiceSpMap> PrServiceSpMaps { get; set; }
    public DbSet<TranPayment> TranPayments { get; set; }
    public DbSet<ExpHeadInfo> ExpHeadInfos { get; set; }
    
    #endregion

    #endregion

    public IDbConnection Connection => Database.GetDbConnection();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUserRole>(userRole =>
        {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        //AllDefaultValueSeedData.SetConfig(modelBuilder);
        //modelBuilder.HasDefaultSchema("dbo");
    }


}
