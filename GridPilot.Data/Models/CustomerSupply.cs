using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridPilot.Data.Models
{
    public class CustomerSupply
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public VoltageType VoltageType { get; set; } // XT (Low) ή MT (Medium)

        // --- Πεδία για ΜΕΣΗ ΤΑΣΗ (ΜΤ) ---
        public int? MvContractedPowerKva { get; set; } // Συμφωνημένη ισχύς (πολλαπλάσιο του 50)

        // --- Πεδία για ΧΑΜΗΛΗ ΤΑΣΗ (ΧΤ) ---
        public Guid? LvSupplyParameterId { get; set; }
        
        [ForeignKey(nameof(LvSupplyParameterId))]
        public LvSupplyParameter LvSupplyParameter { get; set; } // Σύνδεση με τον πίνακα ΔΕΔΔΗΕ της φωτό
    }

    public enum VoltageType
    {
        LowVoltage,    // Χαμηλή Τάση (ΧΤ)
        MediumVoltage  // Μέση Τάση (ΜΤ)
    }
}
