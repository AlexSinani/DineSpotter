using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineSpotterRestaurantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Restaurant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Restaurant");
        }
    }
}
