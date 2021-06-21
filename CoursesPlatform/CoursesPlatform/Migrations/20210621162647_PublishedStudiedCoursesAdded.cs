using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesPlatform.Migrations
{
    public partial class PublishedStudiedCoursesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Courses",
                newName: "UserStudiedCoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                newName: "IX_Courses_UserStudiedCoursesId");

            migrationBuilder.AddColumn<string>(
                name: "UserPublishedCoursesId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserPublishedCoursesId",
                table: "Courses",
                column: "UserPublishedCoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserPublishedCoursesId",
                table: "Courses",
                column: "UserPublishedCoursesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserStudiedCoursesId",
                table: "Courses",
                column: "UserStudiedCoursesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserPublishedCoursesId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserStudiedCoursesId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserPublishedCoursesId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserPublishedCoursesId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "UserStudiedCoursesId",
                table: "Courses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_UserStudiedCoursesId",
                table: "Courses",
                newName: "IX_Courses_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
