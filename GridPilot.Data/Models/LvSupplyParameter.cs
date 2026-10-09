using System;
using System.ComponentModel.DataAnnotations;

namespace GridPilot.Data.Models
{
    public class LvSupplyParameter
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty; // "03", "05", "1", "2", "3" κτλ.

        [Required]
        public int Kva { get; set; } // 8, 12, 15, 25, 35, 55, 85, 135, 250

        [Required]
        public bool IsThreePhase { get; set; } // True = Τριφασική, False = Μονοφασική

        [MaxLength(50)]
        public string InternalInstallationFuse { get; set; } = string.Empty;// Γενική εσωτ. εγκατάστασης (Α)

        [MaxLength(50)]
        public string MeterFuse { get; set; } = string.Empty; // Μετρητή Ασφ. (Α)

        [MaxLength(50)]
        public string MeterCircuitBreaker { get; set; } = string.Empty; // Μετρητή Μικρ. (Α)

        [MaxLength(50)]
        public string MinNetworkDeparture { get; set; } = string.Empty; // Ελάχ. Αναχ. δικτύου ΧΤ (Α)

        [MaxLength(100)]
        public string MeterRating { get; set; } = string.Empty; // Μετρητής (Α)

        [MaxLength(100)]
        public string ConcentricCableCu { get; set; } = string.Empty; // Διατομή Καλωδίου: Συγκεντρικά Θ.Ν. (Cu) mm²

        [MaxLength(100)]
        public string XlpeCable { get; set; } = string.Empty; // Διατομή Καλωδίου: X - LPE mm²

        [MaxLength(100)]
        public string MinLineCrossSection { get; set; } = string.Empty; // Ελάχ. διατομή γραμμής πίνακα-μετρητή mm²

        [MaxLength(50)]
        public string MinTransformerKva { get; set; } = string.Empty; // Ελάχιστη ισχύς Μ/Σ kVA
    }
}
