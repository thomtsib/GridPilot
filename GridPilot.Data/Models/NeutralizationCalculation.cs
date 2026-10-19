namespace GridPilot.Data.Models
{
    public class NeutralizationCalculation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProjectName { get; set; } = string.Empty;
        public DateTime CalculationDate { get; set; } = DateTime.UtcNow;

        // Δεδομένα Μετασχηματιστή
        public int TransformerKva { get; set; } // π.χ. 250, 400, 630
        public double Rm { get; set; } // Ωμική αντίσταση Μ/Σ
        public double Xm { get; set; } // Επαγωγική αντίσταση Μ/Σ

        // Δεδομένα Δικτύου/Γραμμής (Αλουμίνιο ή Συνεστραμμένο)
        public string LineType { get; set; } = "4x50 AL";
        public double LineLengthMeters { get; set; }

        // Δεδομένα Παροχής Καταναλωτή
        public double ServiceLengthMeters { get; set; }
        public string ServiceCableType { get; set; } = "2x6 CU";

        // Ασφάλεια Αναχώρησης
        public int FuseRatingAmps { get; set; } // π.χ. 160 A

        // Αποτελέσματα Υπολογισμού
        public double TotalR { get; set; }
        public double TotalX { get; set; }
        public double TotalZ { get; set; }
        public double I_ShortCircuit { get; set; } // Ρεύμα βραχυκύκλωσης (Iβρ)
        public bool IsConditionMet { get; set; } // Αν περνάει την ουδετέρωση
    }
}
