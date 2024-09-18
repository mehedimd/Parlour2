using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HrmModuleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RetirementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProbationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SignatureUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    NIDUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    NID = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    EmployeeStatus = table.Column<short>(type: "smallint", nullable: false),
                    PreAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    PerAddress = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    SalaryType = table.Column<short>(type: "smallint", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    DisableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisableReason = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    BankAccNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPfMember = table.Column<bool>(type: "bit", nullable: false),
                    EmpMachineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    AcademicDptId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    DisableById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_DisableById",
                        column: x => x.DisableById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_AcademicDptId",
                        column: x => x.AcademicDptId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HrSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeStartTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OfficeEndTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HolidayOne = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HolidayTwo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HolidayOneType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    HolidayTwoType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsSpecialTime = table.Column<bool>(type: "bit", nullable: false),
                    SpOfficeStartTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    SpOfficeEndTime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrSettings_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HrSettings_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetHolidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayYear = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    HolidayName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<short>(type: "smallint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetHolidays_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHolidays_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpAttendances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LunchOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LunchIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    StatusDtl = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    IsManual = table.Column<bool>(type: "bit", nullable: false),
                    ManualEntrydate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsNight = table.Column<short>(type: "smallint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ManualEntryById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpAttendances_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpAttendances_AspNetUsers_ManualEntryById",
                        column: x => x.ManualEntryById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpAttendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpDisciplinaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<short>(type: "smallint", nullable: false),
                    DisciplineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionReason = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: true),
                    ActionDesc = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Committee = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActionFileUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpDisciplinaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpDisciplinaries_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpDisciplinaries_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpDisciplinaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpEducations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    BoardUniversity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearFrom = table.Column<short>(type: "smallint", nullable: true),
                    YearTo = table.Column<short>(type: "smallint", nullable: true),
                    PassingYear = table.Column<short>(type: "smallint", nullable: true),
                    SubjectGroup = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Result = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CertificateUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpEducations_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpEducations_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpEducations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpExperiences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    LastDrawnSalary = table.Column<double>(type: "float", nullable: false),
                    LeftReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ExperienceUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpExperiences_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpExperiences_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpExperiences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpJournals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    About = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DocUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpJournals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpJournals_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpJournals_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpJournals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpPostings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostingType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    NetSalary = table.Column<double>(type: "float", nullable: false),
                    PreNetSalary = table.Column<double>(type: "float", nullable: false),
                    PostingDoc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    DesignationId = table.Column<long>(type: "bigint", nullable: true),
                    PreDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    PreDesignationId = table.Column<long>(type: "bigint", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpPostings_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_Departments_PreDepartmentId",
                        column: x => x.PreDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_Designations_PreDesignationId",
                        column: x => x.PreDesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpPostings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpReferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    RefType = table.Column<short>(type: "smallint", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    OwnerShip = table.Column<double>(type: "float", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpReferences_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpReferences_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpReferences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpTrainings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TrainingFileUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    SlNo = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpTrainings_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpTrainings_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpTrainings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpLeaveApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Length = table.Column<short>(type: "smallint", nullable: false),
                    ApproveLength = table.Column<short>(type: "smallint", nullable: false),
                    FileDoc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewRemarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalRemarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    LeaveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SubmitById = table.Column<long>(type: "bigint", nullable: false),
                    ReviewById = table.Column<long>(type: "bigint", nullable: true),
                    ApprovedById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpLeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_AspNetUsers_ReviewById",
                        column: x => x.ReviewById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_AspNetUsers_SubmitById",
                        column: x => x.SubmitById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpLeaveApplications_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveCfs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveYear = table.Column<int>(type: "int", nullable: false),
                    LeaveBalance = table.Column<int>(type: "int", nullable: false),
                    CfBalance = table.Column<int>(type: "int", nullable: false),
                    LeaveEnjoyed = table.Column<int>(type: "int", nullable: false),
                    LeaveSale = table.Column<int>(type: "int", nullable: false),
                    LastActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LeaveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveCfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveCfs_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveCfs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveCfs_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveSetups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveLimitType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    LeaveBalance = table.Column<int>(type: "int", nullable: false),
                    MaxLeave = table.Column<int>(type: "int", nullable: false),
                    MinLeave = table.Column<int>(type: "int", nullable: false),
                    IsCarryForward = table.Column<bool>(type: "bit", nullable: false),
                    MaxCarryForward = table.Column<int>(type: "int", nullable: false),
                    LeaveAfter = table.Column<int>(type: "int", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LeaveTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveSetups_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveSetups_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveSetups_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpAttendances_ActionById",
                table: "EmpAttendances",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpAttendances_EmployeeId",
                table: "EmpAttendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpAttendances_ManualEntryById",
                table: "EmpAttendances",
                column: "ManualEntryById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpDisciplinaries_ActionById",
                table: "EmpDisciplinaries",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpDisciplinaries_EmployeeId",
                table: "EmpDisciplinaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpDisciplinaries_UpdatedById",
                table: "EmpDisciplinaries",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpEducations_ActionById",
                table: "EmpEducations",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpEducations_EmployeeId",
                table: "EmpEducations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpEducations_UpdatedById",
                table: "EmpEducations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpExperiences_ActionById",
                table: "EmpExperiences",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpExperiences_EmployeeId",
                table: "EmpExperiences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpExperiences_UpdatedById",
                table: "EmpExperiences",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpJournals_ActionById",
                table: "EmpJournals",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpJournals_EmployeeId",
                table: "EmpJournals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpJournals_UpdatedById",
                table: "EmpJournals",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_ActionById",
                table: "EmpLeaveApplications",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_ApprovedById",
                table: "EmpLeaveApplications",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_EmployeeId",
                table: "EmpLeaveApplications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_LeaveTypeId",
                table: "EmpLeaveApplications",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_ReviewById",
                table: "EmpLeaveApplications",
                column: "ReviewById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_SubmitById",
                table: "EmpLeaveApplications",
                column: "SubmitById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AcademicDptId",
                table: "Employees",
                column: "AcademicDptId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ActionById",
                table: "Employees",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DisableById",
                table: "Employees",
                column: "DisableById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UpdatedById",
                table: "Employees",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_ActionById",
                table: "EmpPostings",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_DepartmentId",
                table: "EmpPostings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_DesignationId",
                table: "EmpPostings",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_EmployeeId",
                table: "EmpPostings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_PreDepartmentId",
                table: "EmpPostings",
                column: "PreDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_PreDesignationId",
                table: "EmpPostings",
                column: "PreDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpPostings_UpdatedById",
                table: "EmpPostings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpReferences_ActionById",
                table: "EmpReferences",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpReferences_EmployeeId",
                table: "EmpReferences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpReferences_UpdatedById",
                table: "EmpReferences",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTrainings_ActionById",
                table: "EmpTrainings",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTrainings_EmployeeId",
                table: "EmpTrainings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTrainings_UpdatedById",
                table: "EmpTrainings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HrSettings_ActionById",
                table: "HrSettings",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_HrSettings_UpdatedById",
                table: "HrSettings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveCfs_ActionById",
                table: "LeaveCfs",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveCfs_EmployeeId",
                table: "LeaveCfs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveCfs_LeaveTypeId",
                table: "LeaveCfs",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSetups_ActionById",
                table: "LeaveSetups",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSetups_LeaveTypeId",
                table: "LeaveSetups",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSetups_UpdatedById",
                table: "LeaveSetups",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_ActionById",
                table: "LeaveTypes",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_UpdatedById",
                table: "LeaveTypes",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SetHolidays_ActionById",
                table: "SetHolidays",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_SetHolidays_UpdatedById",
                table: "SetHolidays",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpAttendances");

            migrationBuilder.DropTable(
                name: "EmpDisciplinaries");

            migrationBuilder.DropTable(
                name: "EmpEducations");

            migrationBuilder.DropTable(
                name: "EmpExperiences");

            migrationBuilder.DropTable(
                name: "EmpJournals");

            migrationBuilder.DropTable(
                name: "EmpLeaveApplications");

            migrationBuilder.DropTable(
                name: "EmpPostings");

            migrationBuilder.DropTable(
                name: "EmpReferences");

            migrationBuilder.DropTable(
                name: "EmpTrainings");

            migrationBuilder.DropTable(
                name: "HrSettings");

            migrationBuilder.DropTable(
                name: "LeaveCfs");

            migrationBuilder.DropTable(
                name: "LeaveSetups");

            migrationBuilder.DropTable(
                name: "SetHolidays");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveTypes");
        }
    }
}
