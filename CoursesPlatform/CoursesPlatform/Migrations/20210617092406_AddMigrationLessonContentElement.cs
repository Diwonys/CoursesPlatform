using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesPlatform.Migrations
{
    public partial class AddMigrationLessonContentElement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "ContentElements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentElements_LessonId",
                table: "ContentElements",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentElements_Lessons_LessonId",
                table: "ContentElements",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentElements_Lessons_LessonId",
                table: "ContentElements");

            migrationBuilder.DropIndex(
                name: "IX_ContentElements_LessonId",
                table: "ContentElements");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "ContentElements");
        }
    }
}
