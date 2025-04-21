using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_DataLayer.Migrations
{
    public partial class AddSubCatId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategory_Id",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubCategory_Id",
                table: "Posts",
                column: "SubCategory_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_SubCategory_Id",
                table: "Posts",
                column: "SubCategory_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_SubCategory_Id",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubCategory_Id",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubCategory_Id",
                table: "Posts");
        }
    }
}
