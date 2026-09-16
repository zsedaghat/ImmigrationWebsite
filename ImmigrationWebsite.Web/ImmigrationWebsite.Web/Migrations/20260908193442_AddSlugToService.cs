using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImmigrationWebsite.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Services");
        }
    }
}
