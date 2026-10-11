using GridPilot.Core.Models.Mechanical;

namespace GridPilot.Core.Services;

public interface IMechanicalEngine
{
    SagTensionResult CalculateSag(double spanLength, double tensionDaN, double weightDaNPerM, double poleHeightM);
    PoleSelection SelectPoleAndGuyWire(double totalSpanLength, double deflectionAngle, LoadingZone zone);
}

public class MechanicalEngine : IMechanicalEngine
{
    // Τύπος Λαμπρόπουλου: f = (W * L²) / (8 * T)
    public SagTensionResult CalculateSag(double spanLength, double tensionDaN, double weightDaNPerM, double poleHeightM)
    {
        double sag = (weightDaNPerM * Math.Pow(spanLength, 2)) / (8.0 * tensionDaN);
        
        // Υπολογισμός ελάχιστης απόστασης από το έδαφος (παραδοχή ύψους ανάρτησης = H - 1.5m)
        double attachmentHeight = poleHeightM - 1.5;
        double groundClearance = attachmentHeight - sag;

        return new SagTensionResult
        {
            TensionDaN = tensionDaN,
            SagMeters = Math.Round(sag, 3),
            GroundClearanceMeters = Math.Round(groundClearance, 2),
            HasUpwardPull = groundClearance < 0
        };
    }

    public PoleSelection SelectPoleAndGuyWire(double totalSpanLength, double deflectionAngle, LoadingZone zone)
    {
        // Λογική επιλογής στύλου βάσει πινάκων Λαμπρόπουλου / ΤΚΔ
        string poleType = "10E"; // Προεπιλογή για ΧΤ
        string guyConfig = "None";

        if (deflectionAngle > 15 || totalSpanLength > 80)
        {
            poleType = "10M"; // Ενισχυμένος στύλος
            guyConfig = "F-21X/E (1:1)"; // Προσθήκη επιτόνου
        }

        double height = 10.0;
        double embedment = 0.1 * height + 0.5; // B.Θ. = 0.1*H + 0.5m

        return new PoleSelection
        {
            PoleType = poleType,
            HeightMeters = height,
            EmbedmentDepthMeters = Math.Round(embedment, 2),
            GuyWireConfig = guyConfig
        };
    }
}