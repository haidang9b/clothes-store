using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStore.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numberPhone",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "numberPhone",
                table: "Bill");
        }
    }
}
