using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace giftexchange.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiftExchanges",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPurchasePrice = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftExchanges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftAssignmentID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Participants_Participants_GiftAssignmentID",
                        column: x => x.GiftAssignmentID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_GiftAssignmentID",
                table: "Participants",
                column: "GiftAssignmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftExchanges");

            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}
