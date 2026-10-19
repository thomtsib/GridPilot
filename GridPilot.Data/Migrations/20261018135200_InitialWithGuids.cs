using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridPilot.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LvSupplyParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Kva = table.Column<int>(type: "INTEGER", nullable: false),
                    IsThreePhase = table.Column<bool>(type: "INTEGER", nullable: false),
                    InternalInstallationFuse = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MeterFuse = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MeterCircuitBreaker = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MinNetworkDeparture = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MeterRating = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConcentricCableCu = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    XlpeCable = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MinLineCrossSection = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MinTransformerKva = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LvSupplyParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NeutralizationCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransformerKva = table.Column<int>(type: "INTEGER", nullable: false),
                    Rm = table.Column<double>(type: "REAL", nullable: false),
                    Xm = table.Column<double>(type: "REAL", nullable: false),
                    LineType = table.Column<string>(type: "TEXT", nullable: false),
                    LineLengthMeters = table.Column<double>(type: "REAL", nullable: false),
                    ServiceLengthMeters = table.Column<double>(type: "REAL", nullable: false),
                    ServiceCableType = table.Column<string>(type: "TEXT", nullable: false),
                    FuseRatingAmps = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalR = table.Column<double>(type: "REAL", nullable: false),
                    TotalX = table.Column<double>(type: "REAL", nullable: false),
                    TotalZ = table.Column<double>(type: "REAL", nullable: false),
                    I_ShortCircuit = table.Column<double>(type: "REAL", nullable: false),
                    IsConditionMet = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeutralizationCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSupplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VoltageType = table.Column<int>(type: "INTEGER", nullable: false),
                    MvContractedPowerKva = table.Column<int>(type: "INTEGER", nullable: true),
                    LvSupplyParameterId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSupplies_LvSupplyParameters_LvSupplyParameterId",
                        column: x => x.LvSupplyParameterId,
                        principalTable: "LvSupplyParameters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SupplyNumber = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    StreetAddress = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Municipality = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerSupplyId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerSupplies_CustomerSupplyId",
                        column: x => x.CustomerSupplyId,
                        principalTable: "CustomerSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerSupplyId",
                table: "Customers",
                column: "CustomerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_SupplyNumber",
                table: "Customers",
                column: "SupplyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSupplies_LvSupplyParameterId",
                table: "CustomerSupplies",
                column: "LvSupplyParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "NeutralizationCalculations");

            migrationBuilder.DropTable(
                name: "CustomerSupplies");

            migrationBuilder.DropTable(
                name: "LvSupplyParameters");
        }
    }
}
