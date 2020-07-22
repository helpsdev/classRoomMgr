using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassRoomManager.Repositories.Migrations
{
    public partial class SetActivityFinalEvaluationValueAsDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalEvaluationValue",
                table: "Activity",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FinalEvaluationValue",
                table: "Activity",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
