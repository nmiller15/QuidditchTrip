using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuidditchTrip.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsWithRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTeamKey",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameEndDate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinningTeamKey",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameStartDate",
                table: "Games",
                newName: "GameStartDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Teams",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Teams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "GameEndDateTime",
                table: "Games",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameKey",
                table: "Teams",
                column: "GameKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameKey",
                table: "Teams",
                column: "GameKey",
                principalTable: "Games",
                principalColumn: "GameKey",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameKey",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_GameKey",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GameEndDateTime",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameStartDateTime",
                table: "Games",
                newName: "GameStartDate");

            migrationBuilder.AddColumn<int>(
                name: "ActiveTeamKey",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "GameEndDate",
                table: "Games",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WinningTeamKey",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
