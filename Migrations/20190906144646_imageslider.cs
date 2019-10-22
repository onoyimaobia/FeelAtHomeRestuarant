using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeelAtHomeRestaurant.Migrations
{
    public partial class imageslider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageSliders",
                columns: table => new
                {
                    ImageSliderId = table.Column<Guid>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSliders", x => x.ImageSliderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageSliders");
        }
    }
}
