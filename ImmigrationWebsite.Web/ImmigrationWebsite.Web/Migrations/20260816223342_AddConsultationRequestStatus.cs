using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImmigrationWebsite.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultationRequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ConsultationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ConsultationRequests");
        }
    }
}
