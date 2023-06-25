﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketHub.Migrations
{
    /// <inheritdoc />
    public partial class isListedPropertyToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isListed",
                table: "Ticket",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isListed",
                table: "Ticket");
        }
    }
}
