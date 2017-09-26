using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace giftexchange.Migrations
{
    public partial class InverseRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GiftExchangeId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_GiftExchangeId",
                table: "Participants",
                column: "GiftExchangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_GiftExchanges_GiftExchangeId",
                table: "Participants",
                column: "GiftExchangeId",
                principalTable: "GiftExchanges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_GiftExchanges_GiftExchangeId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_GiftExchangeId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "GiftExchangeId",
                table: "Participants");
        }
    }
}
