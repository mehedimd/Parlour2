using Microsoft.EntityFrameworkCore.Migrations;
using Persistence.SeedData;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BaseSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            BaseSeedModel.Up(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            BaseSeedModel.Down(migrationBuilder);
        }
    }
}
