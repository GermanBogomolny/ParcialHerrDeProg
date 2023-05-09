using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stix.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRestaurant_Food_FoodId",
                table: "FoodRestaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodRestaurant_Restaurant_RestaurantId",
                table: "FoodRestaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodRestaurant",
                table: "FoodRestaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "Restaurant",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "FoodRestaurant",
                newName: "FoodRestaurants");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "Foods");

            migrationBuilder.RenameIndex(
                name: "IX_FoodRestaurant_RestaurantId",
                table: "FoodRestaurants",
                newName: "IX_FoodRestaurants_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodRestaurants",
                table: "FoodRestaurants",
                columns: new[] { "FoodId", "RestaurantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRestaurants_Foods_FoodId",
                table: "FoodRestaurants",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRestaurants_Restaurants_RestaurantId",
                table: "FoodRestaurants",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRestaurants_Foods_FoodId",
                table: "FoodRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodRestaurants_Restaurants_RestaurantId",
                table: "FoodRestaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodRestaurants",
                table: "FoodRestaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurant");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "Food");

            migrationBuilder.RenameTable(
                name: "FoodRestaurants",
                newName: "FoodRestaurant");

            migrationBuilder.RenameIndex(
                name: "IX_FoodRestaurants_RestaurantId",
                table: "FoodRestaurant",
                newName: "IX_FoodRestaurant_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodRestaurant",
                table: "FoodRestaurant",
                columns: new[] { "FoodId", "RestaurantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRestaurant_Food_FoodId",
                table: "FoodRestaurant",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRestaurant_Restaurant_RestaurantId",
                table: "FoodRestaurant",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
