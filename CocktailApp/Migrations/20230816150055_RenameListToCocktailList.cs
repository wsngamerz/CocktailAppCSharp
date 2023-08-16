using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameListToCocktailList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListFavourites_Lists_ListId",
                table: "ListFavourites");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "ListFavourites",
                newName: "CocktailListId");

            migrationBuilder.RenameIndex(
                name: "IX_ListFavourites_ListId",
                table: "ListFavourites",
                newName: "IX_ListFavourites_CocktailListId");

            migrationBuilder.CreateTable(
                name: "CocktailListItems",
                columns: table => new
                {
                    CocktailListId = table.Column<Guid>(type: "uuid", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailListItems", x => new { x.CocktailListId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_CocktailListItems_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailListItems_Lists_CocktailListId",
                        column: x => x.CocktailListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CocktailListItems_CocktailId",
                table: "CocktailListItems",
                column: "CocktailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListFavourites_Lists_CocktailListId",
                table: "ListFavourites",
                column: "CocktailListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListFavourites_Lists_CocktailListId",
                table: "ListFavourites");

            migrationBuilder.DropTable(
                name: "CocktailListItems");

            migrationBuilder.RenameColumn(
                name: "CocktailListId",
                table: "ListFavourites",
                newName: "ListId");

            migrationBuilder.RenameIndex(
                name: "IX_ListFavourites_CocktailListId",
                table: "ListFavourites",
                newName: "IX_ListFavourites_ListId");

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    ListId = table.Column<Guid>(type: "uuid", nullable: false),
                    CocktailId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => new { x.ListId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_ListItems_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListItems_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_CocktailId",
                table: "ListItems",
                column: "CocktailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListFavourites_Lists_ListId",
                table: "ListFavourites",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
