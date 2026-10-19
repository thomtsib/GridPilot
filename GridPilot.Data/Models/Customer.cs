using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GridPilot.Data.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty; // Όνομα / Επωνυμία Εταιρείας

        [Required]
        [MaxLength(11)]
        public string SupplyNumber { get; set; } = string.Empty; // 11-ψήφιος Αριθμός Παροχής ΔΕΔΔΗΕ

        // Στοιχεία Τοποθεσίας
        [Required]
        [MaxLength(150)]
        public string StreetAddress { get; set; } = string.Empty; // Οδός και Αριθμός

        [MaxLength(150)]
        public string Location { get; set; } = string.Empty; // Συντεταγμένες

        [Required]
        [MaxLength(100)]
        public string Municipality { get; set; } = string.Empty; // Δήμος (π.χ. Κορδελιού-Ευόσμου, Δέλτα)

        // Σύνδεση με την Παροχή του
        [Required]
        public Guid CustomerSupplyId { get; set; }
        
        [ForeignKey(nameof(CustomerSupplyId))]
        public CustomerSupply Supply { get; set; }
        
    }
}
