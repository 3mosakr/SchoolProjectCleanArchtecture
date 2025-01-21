using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorTableWithHisRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "StudentSubjects",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "NVARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Position = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructors_Instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubjects",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubjects", x => new { x.InstructorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubjects_SubjectId",
                table: "InstructorSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_ManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "InstructorSubjects");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "NVARCHAR(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(255)",
                oldMaxLength: 255);
        }
    }
}
