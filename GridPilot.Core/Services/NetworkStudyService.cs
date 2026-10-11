using GridPilot.Core.Services;
using GridPilot.Core.Models;
using GridPilot.Core.Models.Topography;
using GridPilot.Core.Models.Electrical;
using GridPilot.Core.Models.Mechanical;

public class NetworkStudyService
{
    private readonly ITopographyEngine _topoEngine;
    private readonly IElectricalEngine _elecEngine;
    private readonly IMechanicalEngine _mechEngine;

    public NetworkStudyService(
        ITopographyEngine topoEngine, 
        IElectricalEngine elecEngine, 
        IMechanicalEngine mechEngine)
    {
        _topoEngine = topoEngine;
        _elecEngine = elecEngine;
        _mechEngine = mechEngine;
    }

    public StudyReport RunFullStudy(List<SurveyPoint> rawPoints, VoltageDropRequest elecRequest)
{
    var spans = _topoEngine.ProcessSurveyPoints(rawPoints);
    var elecResult = _elecEngine.CalculateVoltageDrop(elecRequest);

    var poleSelections = new List<PoleSelection>();
    
    // Δημιουργία επιλογής στύλου για ΚΑΘΕ τοπογραφικό σημείο (N σημεία -> N στύλοι)
    for (int i = 0; i < rawPoints.Count; i++)
    {
        double spanDist = i < spans.Count ? spans[i].HorizontalDistance : (spans.Count > 0 ? spans.Last().HorizontalDistance : 40.0);
        double deflection = i < spans.Count ? spans[i].DeflectionAngleDegrees : 0.0;

        var pole = _mechEngine.SelectPoleAndGuyWire(spanDist, deflection, LoadingZone.Light);
        poleSelections.Add(pole);
    }

    return new StudyReport(spans, elecResult, poleSelections);
}
}