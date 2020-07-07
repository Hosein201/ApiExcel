using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiExcel.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    HashRow = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<string>(nullable: true),
                    HashRow = table.Column<byte[]>(nullable: true),
                    Genre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Genres_Genre",
                        column: x => x.Genre,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Genre",
                table: "Videos",
                column: "Genre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
