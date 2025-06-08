using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dropclone.Migrations
{
    /// <inheritdoc />
    public partial class fixFolderFileRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderEntityId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FolderEntityId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FolderEntityId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId",
                table: "Files",
                column: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FolderId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "FolderEntityId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderEntityId",
                table: "Files",
                column: "FolderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderEntityId",
                table: "Files",
                column: "FolderEntityId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
