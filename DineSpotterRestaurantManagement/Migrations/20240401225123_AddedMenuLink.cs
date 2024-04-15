using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineSpotterRestaurantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedMenuLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuLink",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuLink",
                table: "Restaurant");
        }
    }
}
