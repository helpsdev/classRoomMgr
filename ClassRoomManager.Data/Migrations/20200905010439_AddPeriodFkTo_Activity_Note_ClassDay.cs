using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class AddPeriodFkTo_Activity_Note_ClassDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Note_PeriodId",
                table: "Note",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDay_PeriodId",
                table: "ClassDay",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_PeriodId",
                table: "Activity",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Period_PeriodId",
                table: "Activity",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassDay_Period_PeriodId",
                table: "ClassDay",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Period_PeriodId",
                table: "Note",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
                "UPDATE dbo.Note " +
                "SET PeriodId = (SELECT TOP 1 PeriodId FROM dbo.Period) " +
                "WHERE PeriodId IS NULL");

            migrationBuilder.Sql(
                "UPDATE dbo.ClassDay " +
                "SET PeriodId = (SELECT TOP 1 PeriodId FROM dbo.Period) " +
                "WHERE PeriodId IS NULL");

            migrationBuilder.Sql(
                "UPDATE dbo.Activity " +
                "SET PeriodId = (SELECT TOP 1 PeriodId FROM dbo.Period) " +
                "WHERE PeriodId IS NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Period_PeriodId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassDay_Period_PeriodId",
                table: "ClassDay");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Period_PeriodId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Note_PeriodId",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_ClassDay_PeriodId",
                table: "ClassDay");

            migrationBuilder.DropIndex(
                name: "IX_Activity_PeriodId",
                table: "Activity");
        }
    }
}
