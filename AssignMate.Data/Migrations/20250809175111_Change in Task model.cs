using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeinTaskmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_TaskItemId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskItemId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskItemId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskItemId",
                table: "Tasks",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_TaskItemId",
                table: "Tasks",
                column: "TaskItemId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
