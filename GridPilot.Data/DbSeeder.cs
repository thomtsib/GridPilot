using System;
using System.Linq;
using GridPilot.Data.Models;

namespace GridPilot.Data
{
    public static class DbSeeder
    {
        public static void SeedParameters(GridPilotContext context)
        {
            if (context.LvSupplyParameters.Take(1).ToList().Count > 0) 
            {
                return;
            }

            var parameters = new[]
            {
                // === ΜΟΝΟΦΑΣΙΚΕΣ ΠΑΡΟΧΕΣ ===
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "03", Kva = 8, IsThreePhase = false,
                    InternalInstallationFuse = "35", MeterFuse = "35", MeterCircuitBreaker = "40",
                    MinNetworkDeparture = "63 (40)", MeterRating = "10/40 ή 15/60",
                    ConcentricCableCu = "2 x 6", XlpeCable = "-", MinLineCrossSection = "3 x 10", MinTransformerKva = "50 (25)"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "05", Kva = 12, IsThreePhase = false,
                    InternalInstallationFuse = "50", MeterFuse = "63", MeterCircuitBreaker = "63",
                    MinNetworkDeparture = "80", MeterRating = "15/60",
                    ConcentricCableCu = "2 x 16", XlpeCable = "-", MinLineCrossSection = "3 x 16", MinTransformerKva = "50"
                },

                // === ΤΡΙΦΑΣΙΚΕΣ ΠΑΡΟΧΕΣ ===
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "1", Kva = 15, IsThreePhase = true,
                    InternalInstallationFuse = "25", MeterFuse = "-", MeterCircuitBreaker = "25",
                    MinNetworkDeparture = "63 (40)", MeterRating = "3 x 10/40 ή 3 x 10/60",
                    ConcentricCableCu = "4 x 6", XlpeCable = "-", MinLineCrossSection = "5 x 6", MinTransformerKva = "50 (25)"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "2", Kva = 25, IsThreePhase = true,
                    InternalInstallationFuse = "35", MeterFuse = "-", MeterCircuitBreaker = "40",
                    MinNetworkDeparture = "63 (40)", MeterRating = "3 x 10/40 ή 3 x 10/60",
                    ConcentricCableCu = "4 x 6", XlpeCable = "-", MinLineCrossSection = "5 x 10", MinTransformerKva = "50 (25)"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "3", Kva = 35, IsThreePhase = true,
                    InternalInstallationFuse = "50", MeterFuse = "-", MeterCircuitBreaker = "-",
                    MinNetworkDeparture = "63", MeterRating = "3 x 20/60 ή 3 x 10/60",
                    ConcentricCableCu = "4 x 16", XlpeCable = "-", MinLineCrossSection = "5 x 16", MinTransformerKva = "50"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "4", Kva = 55, IsThreePhase = true,
                    InternalInstallationFuse = "80", MeterFuse = "-", MeterCircuitBreaker = "-",
                    MinNetworkDeparture = "100", MeterRating = "3 x 50/100 ή 3 x 20/100",
                    ConcentricCableCu = "4 x 25", XlpeCable = "-", MinLineCrossSection = "3x25 + 16 + 16", MinTransformerKva = "100 (75)"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "5", Kva = 85, IsThreePhase = true,
                    InternalInstallationFuse = "125", MeterFuse = "-", MeterCircuitBreaker = "-",
                    MinNetworkDeparture = "160", MeterRating = "3 x 1.5/6 ή 3 x 1/6",
                    ConcentricCableCu = "4 x 50", XlpeCable = "3x95 Al + 35 Cu X-LPE", MinLineCrossSection = "3x50 + 25 + 25", MinTransformerKva = "100"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "6", Kva = 135, IsThreePhase = true,
                    InternalInstallationFuse = "200", MeterFuse = "-", MeterCircuitBreaker = "-",
                    MinNetworkDeparture = "250", MeterRating = "3 x 1.5/6 ή 3 x 1/6",
                    ConcentricCableCu = "Μονοπολ. 95 Cu", XlpeCable = "3x150 Al + 50 Cu X-LPE", MinLineCrossSection = "3x120 + 70 + 70", MinTransformerKva = "160"
                },
                new LvSupplyParameter
                {
                    Id = Guid.NewGuid(), Code = "7", Kva = 250, IsThreePhase = true,
                    InternalInstallationFuse = "315 ή 355", MeterFuse = "-", MeterCircuitBreaker = "-",
                    MinNetworkDeparture = "400", MeterRating = "3 x 1.5/6 ή 3 x 1/6",
                    ConcentricCableCu = "Μονοπολ. 150 Cu", XlpeCable = "2 x (3x150 Al + 50 Cu) X-LPE", MinLineCrossSection = "3x240 + 120 + 120 ή Μονοπ. 150 Cu", MinTransformerKva = "250"
                }
            };

            context.LvSupplyParameters.AddRange(parameters);
            context.SaveChanges();
        }
    }
}
