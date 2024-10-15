﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSystemManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddNoteToDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Documents");
        }
    }
}
