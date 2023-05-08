using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stix.Migrations
{
    /// <inheritdoc />
    public partial class WorkingOnRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantFood");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Food",
                newName: "FoodTypeId");

            migrationBuilder.CreateTable(
                name: "FoodRestaurant",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodRestaurant", x => new { x.FoodsId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_FoodRestaurant_Food_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_FoodTypeId",
                table: "Food",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodRestaurant_RestaurantsId",
                table: "FoodRestaurant",
                column: "RestaurantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_FoodType_FoodTypeId",
                table: "Food",
                column: "FoodTypeId",
                principalTable: "FoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_FoodType_FoodTypeId",
                table: "Food");

            migrationBuilder.DropTable(
                name: "FoodRestaurant");

            migrationBuilder.DropTable(
                name: "FoodType");

            migrationBuilder.DropIndex(
                name: "IX_Food_FoodTypeId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "FoodTypeId",
                table: "Food",
                newName: "Type");

            migrationBuilder.CreateTable(
                name: "RestaurantFood",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "INTEGER", nullable: false),
                    RestaurantsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantFood", x => new { x.FoodsId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_RestaurantFood_Food_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantFood_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantFood_RestaurantsId",
                table: "RestaurantFood",
                column: "RestaurantsId");
        }
    }
}
