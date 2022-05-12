using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStation.Migrations
{
    public partial class qw2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusStopRoute");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "BusStops",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_RouteId",
                table: "BusStops",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusStops_Routes_RouteId",
                table: "BusStops",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusStops_Routes_RouteId",
                table: "BusStops");

            migrationBuilder.DropIndex(
                name: "IX_BusStops_RouteId",
                table: "BusStops");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "BusStops");

            migrationBuilder.CreateTable(
                name: "BusStopRoute",
                columns: table => new
                {
                    BusStopsId = table.Column<int>(type: "int", nullable: false),
                    RouteDaysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStopRoute", x => new { x.BusStopsId, x.RouteDaysId });
                    table.ForeignKey(
                        name: "FK_BusStopRoute_BusStops_BusStopsId",
                        column: x => x.BusStopsId,
                        principalTable: "BusStops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusStopRoute_Routes_RouteDaysId",
                        column: x => x.RouteDaysId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusStopRoute_RouteDaysId",
                table: "BusStopRoute",
                column: "RouteDaysId");
        }
    }
}
