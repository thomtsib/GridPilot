using GridPilot.Core.Models.Topography;

namespace GridPilot.Core.Services;

public interface ITopographyEngine
{
    List<SpanGeometry> ProcessSurveyPoints(List<SurveyPoint> points);
}

public class TopographyEngine : ITopographyEngine
{
    public List<SpanGeometry> ProcessSurveyPoints(List<SurveyPoint> points)
    {
        var spans = new List<SpanGeometry>();
        for (int i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double dz = p2.Z - p1.Z;

            double horizDist = Math.Sqrt(dx * dx + dy * dy);

            // Υπολογισμός γωνίας εκτροπής (θ) αν υπάρχει επόμενο σημείο (i+2)
            double deflection = 0;
            if (i < points.Count - 2)
            {
                var p3 = points[i + 2];
                deflection = CalculateAngle(p1, p2, p3);
            }

            spans.Add(new SpanGeometry
            {
                FromPointId = p1.PointId,
                ToPointId = p2.PointId,
                HorizontalDistance = horizDist,
                ElevationDifference = dz,
                DeflectionAngleDegrees = deflection
            });
        }
        return spans;
    }

    private double CalculateAngle(SurveyPoint p1, SurveyPoint p2, SurveyPoint p3)
    {
        // Υπολογισμός οριζόντιας γωνίας αλλαγής κατεύθυνσης σε μοίρες
        double v1x = p2.X - p1.X; double v1y = p2.Y - p1.Y;
        double v2x = p3.X - p2.X; double v2y = p3.Y - p2.Y;
        double dot = v1x * v2x + v1y * v2y;
        double det = v1x * v2y - v1y * v2x;
        return Math.Atan2(det, dot) * (180.0 / Math.PI);
    }
}