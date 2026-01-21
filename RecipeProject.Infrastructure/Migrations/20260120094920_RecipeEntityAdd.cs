using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecipeEntityAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_recipes");

            migrationBuilder.CreateTable(
                name: "recipes_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_recipes_products_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recipes_products_recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipes_products_ProductId",
                table: "recipes_products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_recipes_products_RecipeId",
                table: "recipes_products",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipes_products");

            migrationBuilder.CreateTable(
                name: "products_recipes",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_recipes", x => new { x.ProductsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_products_recipes_products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_recipes_recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_recipes_RecipesId",
                table: "products_recipes",
                column: "RecipesId");
        }
    }
}
