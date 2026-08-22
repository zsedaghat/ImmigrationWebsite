using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImmigrationWebsite.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryFlagImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlagImageUrl",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagImageUrl",
                table: "Countries");
        }
    }
}
