using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class ComplexModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseAssignmentCourseID",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseAssignmentInstructorID",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseAssignment",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssignment", x => new { x.CourseID, x.InstructorID });
                    table.ForeignKey(
                        name: "FK_CourseAssignment_Instructor_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseAssignmentCourseID_CourseAssignmentInstructorID",
                table: "Course",
                columns: new[] { "CourseAssignmentCourseID", "CourseAssignmentInstructorID" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignment_InstructorID",
                table: "CourseAssignment",
                column: "InstructorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseAssignment_CourseAssignmentCourseID_CourseAssignmentInstructorID",
                table: "Course",
                columns: new[] { "CourseAssignmentCourseID", "CourseAssignmentInstructorID" },
                principalTable: "CourseAssignment",
                principalColumns: new[] { "CourseID", "InstructorID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseAssignment_CourseAssignmentCourseID_CourseAssignmentInstructorID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "CourseAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseAssignmentCourseID_CourseAssignmentInstructorID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseAssignmentCourseID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseAssignmentInstructorID",
                table: "Course");
        }
    }
}
