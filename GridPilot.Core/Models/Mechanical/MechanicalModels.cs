namespace GridPilot.Core.Models.Mechanical;

public enum LoadingZone { Light, Medium, Heavy, ExtraHeavy } // Ελαφρά, Μέση, Βαριά

public class SagTensionResult
{
    public double TemperatureCelsius { get; set; }
    public double TensionDaN { get; set; }            // Τάνυση T (daN)
    public double SagMeters { get; set; }             // Βέλος κάμψης f (m)
    public double GroundClearanceMeters { get; set; } // Απόσταση από έδαφος
    public bool HasUpwardPull { get; set; }           // Έλεγχος τραβήγματος (αρνητικό ΚΑ)
}

public class PoleSelection
{
    public string PoleType { get; set; } = string.Empty; // π.χ. "10E", "11M", "FCP-21X"
    public double HeightMeters { get; set; }            // π.χ. 10.0m
    public double EmbedmentDepthMeters { get; set; }     // Βάθος θεμελίωσης = 0.1*H + 0.5m
    public double TotalBendingMomentDaNm { get; set; }   // Συνολική Ροπή Κάμψης
    public string GuyWireConfig { get; set; } = "None"; // π.χ. "F-21X/E (1:1)" για επίτονο
}