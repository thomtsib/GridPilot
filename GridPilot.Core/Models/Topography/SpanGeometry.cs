namespace GridPilot.Core.Models.Topography;

// Υπολογισμένο άνοιγμα μεταξύ δύο διαδοχικών σημείων
public class SpanGeometry
{
    public int FromPointId { get; set; }
    public int ToPointId { get; set; }
    
    public double HorizontalDistance { get; set; } // L = sqrt(ΔX² + ΔY²)
    public double ElevationDifference { get; set; } // Δz = Z2 - Z1
    public double InclinedDistance => Math.Sqrt(Math.Pow(HorizontalDistance, 2) + Math.Pow(ElevationDifference, 2));
    public double SlopePercentage => HorizontalDistance > 0 ? (ElevationDifference / HorizontalDistance) * 100 : 0;
    public double DeflectionAngleDegrees { get; set; } // Γωνία εκτροπής γραμμής (θ)
}