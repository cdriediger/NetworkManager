using Microsoft.EntityFrameworkCore.Migrations;

namespace NetworkManager.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "driver",
                table: "SwitchModels",
                newName: "adapter");

            migrationBuilder.AddColumn<string>(
                name: "adaptername",
                table: "Switches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adaptername",
                table: "Switches");

            migrationBuilder.RenameColumn(
                name: "adapter",
                table: "SwitchModels",
                newName: "driver");
        }
    }
}
