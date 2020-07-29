using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class AddClassDayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "StudentClassDay");

            migrationBuilder.AddColumn<int>(
                name: "ClassDayId",
                table: "StudentClassDay",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClassDay",
                columns: table => new
                {
                    ClassDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDay", x => x.ClassDayId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClassDay_ClassDayId",
                table: "StudentClassDay",
                column: "ClassDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassDay_ClassDay_ClassDayId",
                table: "StudentClassDay",
                column: "ClassDayId",
                principalTable: "ClassDay",
                principalColumn: "ClassDayId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassDay_ClassDay_ClassDayId",
                table: "StudentClassDay");

            migrationBuilder.DropTable(
                name: "ClassDay");

            migrationBuilder.DropIndex(
                name: "IX_StudentClassDay_ClassDayId",
                table: "StudentClassDay");

            migrationBuilder.DropColumn(
                name: "ClassDayId",
                table: "StudentClassDay");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "StudentClassDay",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
