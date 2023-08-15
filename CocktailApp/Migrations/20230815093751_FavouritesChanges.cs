using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailApp.Migrations
{
    /// <inheritdoc />
    public partial class FavouritesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarItems",
                table: "BarItems");

            migrationBuilder.DropIndex(
                name: "IX_BarItems_UserId",
                table: "BarItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BarItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarItems",
                table: "BarItems",
                columns: new[] { "UserId", "IngredientId" });

            migrationBuilder.CreateTable(
                name: "CocktailFavourites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailFavourites", x => new { x.UserId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_CocktailFavourites_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailFavourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListFavourites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ListId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListFavourites", x => new { x.UserId, x.ListId });
                    table.ForeignKey(
                        name: "FK_ListFavourites_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListFavourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CocktailFavourites_CocktailId",
                table: "CocktailFavourites",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_ListFavourites_ListId",
                table: "ListFavourites",
                column: "ListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CocktailFavourites");

            migrationBuilder.DropTable(
                name: "ListFavourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarItems",
                table: "BarItems");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BarItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarItems",
                table: "BarItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserFavourites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => new { x.UserId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_UserFavourites_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarItems_UserId",
                table: "BarItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_CocktailId",
                table: "UserFavourites",
                column: "CocktailId");
        }
    }
}
