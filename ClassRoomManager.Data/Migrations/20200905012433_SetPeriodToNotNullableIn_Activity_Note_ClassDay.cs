using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class SetPeriodToNotNullableIn_Activity_Note_ClassDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "Note",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "ClassDay",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Period_PeriodId",
                table: "Activity",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassDay_Period_PeriodId",
                table: "ClassDay",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Period_PeriodId",
                table: "Note",
                column: "PeriodId",
                principalTable: "Period",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "Note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "ClassDay",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PeriodId",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
        }
    }
}
