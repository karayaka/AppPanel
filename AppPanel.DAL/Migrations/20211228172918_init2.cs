using Microsoft.EntityFrameworkCore.Migrations;

namespace AppPanel.DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppSubDesc",
                table: "PanelApps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppSubDesc",
                table: "PanelApps");
        }
    }
}
