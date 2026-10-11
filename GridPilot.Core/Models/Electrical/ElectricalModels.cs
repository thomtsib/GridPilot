namespace GridPilot.Core.Models.Electrical;

public record ConductorSpecs(
    string CodeName,          // π.χ. "ΣΚ 4x50 AL", "ACSR 95"
    double ResistancePerKm,   // R (Ω/km)
    double ReactancePerKm,    // X (Ω/km)
    double MaxCurrentAmperes, // Θερμικό όριο ρεύματος (A)
    double WeightKgPerMeter   // Βάρος (daN/m ή kg/m)
);

public class VoltageDropRequest
{
    public double LoadKva { get; set; }          // π.χ. 85 kVA για Ν5
    public double PowerFactor { get; set; } = 0.85; // cosφ
    public double LineVoltageVolts { get; set; } = 400; // U = 400V (ΧΤ)
    public double LengthMeters { get; set; }      // π.χ. 125m
    public ConductorSpecs Conductor { get; set; } = null!;
}

public class VoltageDropResult
{
    public double CurrentAmperes { get; set; }
    public double VoltageDropVolts { get; set; }
    public double VoltageDropPercentage { get; set; } // πτ%
    public bool IsCompliant { get; set; }             // πτ% <= 4%
}