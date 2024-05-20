using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringStationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "Sensor",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rainfall",
                table: "Sensor",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Rainfall",
                table: "Sensor");
        }
    }
}
