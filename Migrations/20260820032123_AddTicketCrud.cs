using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_sem2.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketCrud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketList_townList_FromTownId",
                table: "ticketList");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketList_townList_ToTownId",
                table: "ticketList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_townList",
                table: "townList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticketList",
                table: "ticketList");

            migrationBuilder.RenameTable(
                name: "townList",
                newName: "Towns");

            migrationBuilder.RenameTable(
                name: "ticketList",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_ticketList_ToTownId",
                table: "Tickets",
                newName: "IX_Tickets_ToTownId");

            migrationBuilder.RenameIndex(
                name: "IX_ticketList_FromTownId",
                table: "Tickets",
                newName: "IX_Tickets_FromTownId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Towns",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Airline",
                table: "Tickets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Towns",
                table: "Towns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Towns_FromTownId",
                table: "Tickets",
                column: "FromTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Towns_ToTownId",
                table: "Tickets",
                column: "ToTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Towns_FromTownId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Towns_ToTownId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Towns",
                table: "Towns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Towns",
                newName: "townList");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "ticketList");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ToTownId",
                table: "ticketList",
                newName: "IX_ticketList_ToTownId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_FromTownId",
                table: "ticketList",
                newName: "IX_ticketList_FromTownId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "townList",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Airline",
                table: "ticketList",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_townList",
                table: "townList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticketList",
                table: "ticketList",
                column: "Id");

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
    }
}
