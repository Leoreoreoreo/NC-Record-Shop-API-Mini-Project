using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NC_Record_Shop_API_Mini_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Albums",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Albums");
        }
    }
}
