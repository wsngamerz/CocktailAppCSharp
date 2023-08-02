using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailApp.Migrations
{
    /// <inheritdoc />
    public partial class UserRemoveClerkID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ClerkId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClerkId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClerkId",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClerkId",
                table: "Users",
                column: "ClerkId",
                unique: true);
        }
    }
}
