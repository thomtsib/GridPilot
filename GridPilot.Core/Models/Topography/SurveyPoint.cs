namespace GridPilot.Core.Models.Topography;

// Αρχικό σημείο από το τοπογραφικό όργανο
public record SurveyPoint(
    int PointId,
    double X,
    double Y,
    double Z,
    string Description // π.χ. "Υφιστάμενος Στύλος ΜΤ", "Νέος Στύλος 1"
);