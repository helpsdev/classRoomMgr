using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class AddNullablePeriodIdTo_Activity_Note_ClassDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "ClassDay",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Activity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "ClassDay");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Activity");
        }
    }
}
