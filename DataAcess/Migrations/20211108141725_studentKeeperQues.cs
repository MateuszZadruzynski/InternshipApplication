using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcess.Migrations
{
    public partial class studentKeeperQues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeeperId",
                table: "QuestionnaireModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireModels_KeeperId",
                table: "QuestionnaireModels",
                column: "KeeperId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionnaireModels_keepers_KeeperId",
                table: "QuestionnaireModels",
                column: "KeeperId",
                principalTable: "keepers",
                principalColumn: "KeeperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionnaireModels_keepers_KeeperId",
                table: "QuestionnaireModels");

            migrationBuilder.DropIndex(
                name: "IX_QuestionnaireModels_KeeperId",
                table: "QuestionnaireModels");

            migrationBuilder.DropColumn(
                name: "KeeperId",
                table: "QuestionnaireModels");
        }
    }
}
