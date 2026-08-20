using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_sem2.Migrations
{
    /// <inheritdoc />
    public partial class add_airlineand_Durations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Airline",
                table: "ticketList",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationHours",
                table: "ticketList",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ticketList_FromTownId",
                table: "ticketList",
                column: "FromTownId");

            migrationBuilder.CreateIndex(
                name: "IX_ticketList_ToTownId",
                table: "ticketList",
                column: "ToTownId");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketList_townList_FromTownId",
                table: "ticketList",
                column: "FromTownId",
                principalTable: "townList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketList_townList_ToTownId",
                table: "ticketList",
                column: "ToTownId",
                principalTable: "townList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketList_townList_FromTownId",
                table: "ticketList");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketList_townList_ToTownId",
                table: "ticketList");

            migrationBuilder.DropIndex(
                name: "IX_ticketList_FromTownId",
                table: "ticketList");

            migrationBuilder.DropIndex(
                name: "IX_ticketList_ToTownId",
                table: "ticketList");

            migrationBuilder.DropColumn(
                name: "Airline",
                table: "ticketList");

            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "ticketList");
        }
    }
}
