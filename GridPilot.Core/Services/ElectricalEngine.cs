using GridPilot.Core.Models.Electrical;

namespace GridPilot.Core.Services;

public interface IElectricalEngine
{
    VoltageDropResult CalculateVoltageDrop(VoltageDropRequest req);
}

public class ElectricalEngine : IElectricalEngine
{
    public VoltageDropResult CalculateVoltageDrop(VoltageDropRequest req)
    {
        // I = S / (sqrt(3) * U)
        double current = (req.LoadKva * 1000.0) / (Math.Sqrt(3) * req.LineVoltageVolts);
        
        // ΔU = sqrt(3) * I * (L/1000) * (R*cosφ + X*sinφ)
        double sinPhi = Math.Sqrt(1 - Math.Pow(req.PowerFactor, 2));
        double lengthKm = req.LengthMeters / 1000.0;
        
        double deltaU = Math.Sqrt(3) * current * lengthKm * 
                       (req.Conductor.ResistancePerKm * req.PowerFactor + 
                        req.Conductor.ReactancePerKm * sinPhi);

        double dropPercentage = (deltaU / req.LineVoltageVolts) * 100.0;

        return new VoltageDropResult
        {
            CurrentAmperes = Math.Round(current, 2),
            VoltageDropVolts = Math.Round(deltaU, 2),
            VoltageDropPercentage = Math.Round(dropPercentage, 2),
            IsCompliant = dropPercentage <= 4.0 // Όριο ΔΕΔΔΗΕ 4%
        };
    }
}