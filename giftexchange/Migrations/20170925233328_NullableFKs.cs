using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace giftexchange.Migrations
{
    public partial class NullableFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Participants_GiftAssignmentID",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "GiftAssignmentID",
                table: "Participants",
                newName: "GiftAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_GiftAssignmentID",
                table: "Participants",
                newName: "IX_Participants_GiftAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Participants_GiftAssignmentId",
                table: "Participants",
                column: "GiftAssignmentId",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Participants_GiftAssignmentId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "GiftAssignmentId",
                table: "Participants",
                newName: "GiftAssignmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_GiftAssignmentId",
                table: "Participants",
                newName: "IX_Participants_GiftAssignmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Participants_GiftAssignmentID",
                table: "Participants",
                column: "GiftAssignmentID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
