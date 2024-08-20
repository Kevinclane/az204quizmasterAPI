using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace az204quizmasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class removesactiveQAtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveQAs_Quizzes_QuizId",
                table: "ActiveQAs");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ActiveQAs");

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "ActiveQAs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedAnswers",
                table: "ActiveQAs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveQAs_Quizzes_QuizId",
                table: "ActiveQAs",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveQAs_Quizzes_QuizId",
                table: "ActiveQAs");

            migrationBuilder.DropColumn(
                name: "SubmittedAnswers",
                table: "ActiveQAs");

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "ActiveQAs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ActiveQAs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveQAs_Quizzes_QuizId",
                table: "ActiveQAs",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
