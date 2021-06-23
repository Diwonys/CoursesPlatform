using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesPlatform.Migrations
{
    public partial class LessonContentElementOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentElements_Lessons_LessonId",
                table: "ContentElements");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ContentElements_Lessons_LessonId",
                table: "ContentElements",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
