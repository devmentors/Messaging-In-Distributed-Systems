using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperStore.Carts.DAL.Migrations
{
    public partial class Deduplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deduplications",
                schema: "SuperStore.Carts",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "text", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deduplications", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deduplications",
                schema: "SuperStore.Carts");
        }
    }
}
