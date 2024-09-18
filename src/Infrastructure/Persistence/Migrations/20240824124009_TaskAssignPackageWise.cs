using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaskAssignPackageWise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPackage",
                table: "PrServiceInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PrPackageServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    PackageServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ActionById = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrPackageServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrPackageServices_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrPackageServices_PrServiceInfos_PackageServiceId",
                        column: x => x.PackageServiceId,
                        principalTable: "PrServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrPackageServices_PrServiceInfos_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "PrServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrServiceSpMaps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceStatus = table.Column<short>(type: "smallint", nullable: false),
                    AssignRemarks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SpRemaks = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    CustomerRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerRating = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceBillDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProviderId = table.Column<long>(type: "bigint", nullable: true),
                    AssignById = table.Column<long>(type: "bigint", nullable: true),
                    ActionById = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrServiceSpMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_AspNetUsers_ActionById",
                        column: x => x.ActionById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_AspNetUsers_AssignById",
                        column: x => x.AssignById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_Employees_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_PrServiceInfos_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "PrServiceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrServiceSpMaps_PrServicesBillDetails_ServiceBillDetailId",
                        column: x => x.ServiceBillDetailId,
                        principalTable: "PrServicesBillDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrPackageServices_ActionById",
                table: "PrPackageServices",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrPackageServices_PackageServiceId",
                table: "PrPackageServices",
                column: "PackageServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrPackageServices_ServiceId",
                table: "PrPackageServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_ActionById",
                table: "PrServiceSpMaps",
                column: "ActionById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_AssignById",
                table: "PrServiceSpMaps",
                column: "AssignById");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_ProviderId",
                table: "PrServiceSpMaps",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_ServiceBillDetailId",
                table: "PrServiceSpMaps",
                column: "ServiceBillDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_ServiceId",
                table: "PrServiceSpMaps",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PrServiceSpMaps_UpdatedById",
                table: "PrServiceSpMaps",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrPackageServices");

            migrationBuilder.DropTable(
                name: "PrServiceSpMaps");

            migrationBuilder.DropColumn(
                name: "IsPackage",
                table: "PrServiceInfos");
        }
    }
}
