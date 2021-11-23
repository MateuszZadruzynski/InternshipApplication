using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcess.Migrations
{
    public partial class nullabledesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_images_CompanyId",
                table: "images");

            migrationBuilder.CreateIndex(
                name: "IX_images_CompanyId",
                table: "images",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_images_CompanyId",
                table: "images");

            migrationBuilder.CreateIndex(
                name: "IX_images_CompanyId",
                table: "images",
                column: "CompanyId");
        }
    }
}
