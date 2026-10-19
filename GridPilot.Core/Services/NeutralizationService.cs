using System;
using GridPilot.Data.Models;

namespace GridPilot.Core.Services
{
    public class NeutralizationService
    {
        // Σταθερές αγωγών (R και X σε Ω/km) με βάση τον Λαμπρόπουλο
        // R_Phase + R_Neutral (για αυτό οι τιμές διπλασιάζονται στον βρόχο σφάλματος)
        private static double GetCableResistance(string type) => type switch
        {
            "4x50 AL" => 0.641 * 2,       // 0.641 Ω/km ανά αγωγό
            "4x120 ΣΚ" => 0.253 * 2,      // 0.253 Ω/km ανά αγωγό (Συνεστραμμένο Καλώδιο)
            "3x70+54.6" => 0.443 * 2,     // 0.443 Ω/km ανά αγωγό
            "2x6 CU" => 3.08 * 2,         // Παροχή χαλκού
            "2x16 CU" => 1.15 * 2,        // Παροχή χαλκού
            _ => 1.00
        };

        private static double GetCableReactance(string type) => type switch
        {
            "4x50 AL" => 0.28 * 2,
            "4x120 ΣΚ" => 0.08 * 2,
            "3x70+54.6" => 0.09 * 2,
            "2x6 CU" => 0.10 * 2,
            "2x16 CU" => 0.09 * 2,
            _ => 0.10
        };

        public NeutralizationCalculation Calculate(NeutralizationCalculation calc)
        {
            // 1. Αντίσταση Μετασχηματιστή βάσει kVA (Πίνακας Λαμπρόπουλου)
            (calc.Rm, calc.Xm) = calc.TransformerKva switch
            {
                160 => (0.012, 0.045),
                250 => (0.007, 0.028),
                400 => (0.004, 0.018),
                630 => (0.002, 0.011),
                _ => (0.007, 0.028) // Default 250 kVA
            };

            // 2. Αντίσταση Κύριας Γραμής (Μετατροπή μέτρων σε km)
            double lineKm = calc.LineLengthMeters / 1000.0;
            double rLine = GetCableResistance(calc.LineType) * lineKm;
            double xLine = GetCableReactance(calc.LineType) * lineKm;

            // 3. Αντίσταση Παροχής
            double serviceKm = calc.ServiceLengthMeters / 1000.0;
            double rService = GetCableResistance(calc.ServiceCableType) * serviceKm;
            double xService = GetCableReactance(calc.ServiceCableType) * serviceKm;

            // 4. Συνολική Σύνθετη Αντίσταση Βρόχου (Z)
            calc.TotalR = calc.Rm + rLine + rService;
            calc.TotalX = calc.Xm + xLine + xService;
            calc.TotalZ = Math.Sqrt(Math.Pow(calc.TotalR, 2) + Math.Pow(calc.TotalX, 2));

            // 5. Υπολογισμός Ρεύματος Βραχυκύκλωσης (U_phase = 230V)
            calc.I_ShortCircuit = 230.0 / calc.TotalZ;

            // 6. Έλεγχος Συνθήκης (Κριτήριο Λαμπρόπουλου / ΔΕΗ)
            // Το ρεύμα σφάλματος πρέπει να είναι τουλάχιστον k φορές το ονομαστικό της ασφάλειας.
            // Για ασφάλειες τήξης gG/gL (ΔΕΔΔΗΕ), συνήθως απαιτείται k = 2.5 έως 4 για ακαριαία τήξη.
            double requiredCurrent = calc.FuseRatingAmps * 3.5; 
            calc.IsConditionMet = calc.I_ShortCircuit >= requiredCurrent;

            return calc;
        }
    }
}
