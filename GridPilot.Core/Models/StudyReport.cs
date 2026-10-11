namespace GridPilot.Core.Models;

using GridPilot.Core.Models.Topography;
using GridPilot.Core.Models.Electrical;
using GridPilot.Core.Models.Mechanical;

/// <summary>
/// Συγκεντρωτικό μοντέλο αποτελεσμάτων πλήρους μελέτης δικτύου.
/// </summary>
public record StudyReport(
    List<SpanGeometry> Spans,
    VoltageDropResult VoltageDrop,
    List<PoleSelection> PoleSelections
)
{
    // Ημερομηνία & Ώρα εκτέλεσης της μελέτης
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    // Συνολικό μήκος όδευσης σε μέτρα
    public double TotalNetworkLengthMeters => Spans.Sum(s => s.HorizontalDistance);

    // Γενικός έλεγχος αν η μελέτη είναι αποδεκτή βάσει ορίων ΔΕΔΔΗΕ
    public bool IsCompliant => VoltageDrop.IsCompliant && PoleSelections.All(p => !string.IsNullOrEmpty(p.PoleType));
}