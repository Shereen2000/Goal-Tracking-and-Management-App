using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goal_Management_Web_App.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionStep_Goal_GoalId",
                table: "ActionStep");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "ActionStep",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionStep_Goal_GoalId",
                table: "ActionStep",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionStep_Goal_GoalId",
                table: "ActionStep");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "ActionStep",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionStep_Goal_GoalId",
                table: "ActionStep",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "GoalId");
        }
    }
}
