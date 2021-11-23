using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcess.Migrations
{
    public partial class questionar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_studentAvatars_StudentId",
                table: "studentAvatars");

            migrationBuilder.DropIndex(
                name: "IX_keeperAvatars_KeeperId",
                table: "keeperAvatars");

            migrationBuilder.CreateTable(
                name: "QuestionnaireModels",
                columns: table => new
                {
                    QuestionnaireiD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionOne = table.Column<int>(type: "int", nullable: false),
                    QuestionTwo = table.Column<int>(type: "int", nullable: false),
                    QuestionThree = table.Column<int>(type: "int", nullable: false),
                    QuestionFour = table.Column<int>(type: "int", nullable: false),
                    QuestionFive = table.Column<int>(type: "int", nullable: false),
                    QuestionSix = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Opinion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireModels", x => x.QuestionnaireiD);
                    table.ForeignKey(
                        name: "FK_QuestionnaireModels_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_studentAvatars_StudentId",
                table: "studentAvatars",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_keeperAvatars_KeeperId",
                table: "keeperAvatars",
                column: "KeeperId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireModels_StudentId",
                table: "QuestionnaireModels",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionnaireModels");

            migrationBuilder.DropIndex(
                name: "IX_studentAvatars_StudentId",
                table: "studentAvatars");

            migrationBuilder.DropIndex(
                name: "IX_keeperAvatars_KeeperId",
                table: "keeperAvatars");

            migrationBuilder.CreateIndex(
                name: "IX_studentAvatars_StudentId",
                table: "studentAvatars",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_keeperAvatars_KeeperId",
                table: "keeperAvatars",
                column: "KeeperId");
        }
    }
}
