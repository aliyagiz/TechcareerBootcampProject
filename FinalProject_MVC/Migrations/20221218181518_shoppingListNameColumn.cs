using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject_MVC.Migrations
{
    public partial class shoppingListNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShoppingListName",
                table: "ShoppingList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingListName",
                table: "ShoppingList");
        }
    }
}
