using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class AddStudentFinalGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentFinalGrade",
                columns: table => new
                {
                    StudentFinalGradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    ModificationDate = table.Column<DateTimeOffset>(nullable: false),
                    FinalGrade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFinalGrade", x => x.StudentFinalGradeId);
                    table.ForeignKey(
                        name: "FK_StudentFinalGrade_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "PeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentFinalGrade_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentFinalGrade_PeriodId",
                table: "StudentFinalGrade",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFinalGrade_StudentId",
                table: "StudentFinalGrade",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentFinalGrade");
        }
    }
}
