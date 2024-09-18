using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ParlourEntiryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpHeadInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeadName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpHeadInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpHeadInfos_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpHeadInfos_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpHeadInfos_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrCustomers_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrCustomers_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrCustomers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServiceCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServiceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServiceCategories_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceCategories_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrShifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrShifts_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrShifts_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrShifts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServiceInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServiceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServiceInfos_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceInfos_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceInfos_PrServiceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PrServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServicesBills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingRemarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    TotalBill = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    VAT = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    NetAmount = table.Column<double>(type: "float", nullable: false),
                    ServiceStatus = table.Column<short>(type: "smallint", nullable: false),
                    BillStatus = table.Column<short>(type: "smallint", nullable: false),
                    CompleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedRemarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceShiftId = table.Column<long>(type: "bigint", nullable: false),
                    EntryById = table.Column<long>(type: "bigint", nullable: false),
                    CompletedById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServicesBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_AspNetUsers_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_AspNetUsers_EntryById",
                        column: x => x.EntryById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_PrCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "PrCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBills_PrShifts_ServiceShiftId",
                        column: x => x.ServiceShiftId,
                        principalTable: "PrShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServicesHsts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceInfoId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServicesHsts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServicesHsts_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesHsts_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesHsts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesHsts_PrServiceCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PrServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesHsts_PrServiceInfos_ServiceInfoId",
                        column: x => x.ServiceInfoId,
                        principalTable: "PrServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServicesBillDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    VAT = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    NetAmount = table.Column<double>(type: "float", nullable: false),
                    ServiceStatus = table.Column<short>(type: "smallint", nullable: false),
                    AssignRemarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SpRemaks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    CustomerRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerRating = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceBillId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProviderId = table.Column<long>(type: "bigint", nullable: true),
                    AssignById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServicesBillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_AspNetUsers_AssignById",
                        column: x => x.AssignById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_Employees_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_PrServiceInfos_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "PrServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServicesBillDetails_PrServicesBills_ServiceBillId",
                        column: x => x.ServiceBillId,
                        principalTable: "PrServicesBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TranPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayMode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ChequeNo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceBillId = table.Column<long>(type: "bigint", nullable: true),
                    ExpHeadId = table.Column<long>(type: "bigint", nullable: true),
                    SalesId = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranPayments_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranPayments_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranPayments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranPayments_ExpHeadInfos_ExpHeadId",
                        column: x => x.ExpHeadId,
                        principalTable: "ExpHeadInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TranPayments_PrServicesBills_ServiceBillId",
                        column: x => x.ServiceBillId,
                        principalTable: "PrServicesBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpHeadInfos_ActionById",
                table: "ExpHeadInfos",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_ExpHeadInfos_BranchId",
                table: "ExpHeadInfos",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpHeadInfos_UpdatedById",
                table: "ExpHeadInfos",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrCustomers_ActionById",
                table: "PrCustomers",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrCustomers_BranchId",
                table: "PrCustomers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PrCustomers_UpdatedById",
                table: "PrCustomers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceCategories_ActionById",
                table: "PrServiceCategories",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceCategories_UpdatedById",
                table: "PrServiceCategories",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceInfos_ActionById",
                table: "PrServiceInfos",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceInfos_CategoryId",
                table: "PrServiceInfos",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceInfos_UpdatedById",
                table: "PrServiceInfos",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_ActionById",
                table: "PrServicesBillDetails",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_AssignById",
                table: "PrServicesBillDetails",
                column: "AssignById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_ProviderId",
                table: "PrServicesBillDetails",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_ServiceBillId",
                table: "PrServicesBillDetails",
                column: "ServiceBillId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_ServiceId",
                table: "PrServicesBillDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBillDetails_UpdatedById",
                table: "PrServicesBillDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_ActionById",
                table: "PrServicesBills",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_BranchId",
                table: "PrServicesBills",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_CompletedById",
                table: "PrServicesBills",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_CustomerId",
                table: "PrServicesBills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_EntryById",
                table: "PrServicesBills",
                column: "EntryById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_ServiceShiftId",
                table: "PrServicesBills",
                column: "ServiceShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesBills_UpdatedById",
                table: "PrServicesBills",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesHsts_ActionById",
                table: "PrServicesHsts",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesHsts_BranchId",
                table: "PrServicesHsts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesHsts_CategoryId",
                table: "PrServicesHsts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesHsts_ServiceInfoId",
                table: "PrServicesHsts",
                column: "ServiceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServicesHsts_UpdatedById",
                table: "PrServicesHsts",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrShifts_ActionById",
                table: "PrShifts",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrShifts_BranchId",
                table: "PrShifts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PrShifts_UpdatedById",
                table: "PrShifts",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TranPayments_ActionById",
                table: "TranPayments",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_TranPayments_BranchId",
                table: "TranPayments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_TranPayments_ExpHeadId",
                table: "TranPayments",
                column: "ExpHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TranPayments_ServiceBillId",
                table: "TranPayments",
                column: "ServiceBillId");

            migrationBuilder.CreateIndex(
                name: "IX_TranPayments_UpdatedById",
                table: "TranPayments",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrServicesBillDetails");

            migrationBuilder.DropTable(
                name: "PrServicesHsts");

            migrationBuilder.DropTable(
                name: "TranPayments");

            migrationBuilder.DropTable(
                name: "PrServiceInfos");

            migrationBuilder.DropTable(
                name: "ExpHeadInfos");

            migrationBuilder.DropTable(
                name: "PrServicesBills");

            migrationBuilder.DropTable(
                name: "PrServiceCategories");

            migrationBuilder.DropTable(
                name: "PrCustomers");

            migrationBuilder.DropTable(
                name: "PrShifts");
        }
    }
}
