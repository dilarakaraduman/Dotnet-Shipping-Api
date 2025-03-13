using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carriers_CarrierId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carriers_CarrierId",
                table: "Orders",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "CarrierId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carriers_CarrierId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carriers_CarrierId",
                table: "Orders",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "CarrierId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
