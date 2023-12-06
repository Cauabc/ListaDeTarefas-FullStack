using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListaDeTarefas.API.Migrations
{
    /// <inheritdoc />
    public partial class RelationUserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ApplicationUsers_ApplicationUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ApplicationUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ApplicationUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ApplicationUsers_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ApplicationUserId",
                table: "Tasks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ApplicationUsers_ApplicationUserId",
                table: "Tasks",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }
    }
}
