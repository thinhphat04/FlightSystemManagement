using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSystemManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalDocumentsToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Flights",
                newName: "DepartureDate");

            migrationBuilder.AddColumn<int>(
                name: "TotalDocuments",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDocuments",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Flights",
                newName: "Date");
        }
    }
}
