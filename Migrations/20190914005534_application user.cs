using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeelAtHomeRestaurant.Migrations
{
    public partial class applicationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ShoppingCarts",
                nullable: true,
                oldClrType: typeof(Guid));

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "ShoppingCarts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
