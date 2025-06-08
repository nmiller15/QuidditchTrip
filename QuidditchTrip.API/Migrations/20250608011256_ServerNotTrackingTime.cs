using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuidditchTrip.API.Migrations
{
    /// <inheritdoc />
    public partial class ServerNotTrackingTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeRemaining",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeRemaining",
                table: "Games",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
