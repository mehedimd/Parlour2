using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdentityEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "PrServicesBillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "PrServicesBillDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "LeaveSetups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "HrSettings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BranchId",
                table: "EmpLeaveApplications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassChangeHsts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassChangeHsts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassChangeHsts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassChangeHsts_UserLogs_SessionId",
                        column: x => x.SessionId,
                        principalTable: "UserLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogAttempts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserFullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DateAttempt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PcName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogAttempts_UserLogs_SessionId",
                        column: x => x.SessionId,
                        principalTable: "UserLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSetups_BranchId",
                table: "LeaveSetups",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HrSettings_BranchId",
                table: "HrSettings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpLeaveApplications_BranchId",
                table: "EmpLeaveApplications",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PassChangeHsts_SessionId",
                table: "PassChangeHsts",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PassChangeHsts_UserId",
                table: "PassChangeHsts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogAttempts_SessionId",
                table: "UserLogAttempts",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpLeaveApplications_Branches_BranchId",
                table: "EmpLeaveApplications",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HrSettings_Branches_BranchId",
                table: "HrSettings",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveSetups_Branches_BranchId",
                table: "LeaveSetups",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpLeaveApplications_Branches_BranchId",
                table: "EmpLeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_HrSettings_Branches_BranchId",
                table: "HrSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveSetups_Branches_BranchId",
                table: "LeaveSetups");

            migrationBuilder.DropTable(
                name: "PassChangeHsts");

            migrationBuilder.DropTable(
                name: "UserLogAttempts");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_LeaveSetups_BranchId",
                table: "LeaveSetups");

            migrationBuilder.DropIndex(
                name: "IX_HrSettings_BranchId",
                table: "HrSettings");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmpLeaveApplications_BranchId",
                table: "EmpLeaveApplications");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "PrServicesBillDetails");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "PrServicesBillDetails");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "LeaveSetups");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HrSettings");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "EmpLeaveApplications");
        }
    }
}
