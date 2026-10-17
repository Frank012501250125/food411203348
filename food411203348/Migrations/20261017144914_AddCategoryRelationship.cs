using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace food411203348.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "FoodItems");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FoodItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_CategoryId",
                table: "FoodItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Categories_CategoryId",
                table: "FoodItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Categories_CategoryId",
                table: "FoodItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_CategoryId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FoodItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "FoodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
