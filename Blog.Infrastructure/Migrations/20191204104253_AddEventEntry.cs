using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Infrastructure.Migrations
{
    public partial class AddEventEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClusterId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: false),
                    Operation = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventEntries");
        }
    }
}
