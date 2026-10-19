using GridPilot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GridPilot.Data
{
    public class GridPilotContext : DbContext
    {
        public GridPilotContext(DbContextOptions<GridPilotContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerSupply> CustomerSupplies { get; set; }
        public DbSet<LvSupplyParameter> LvSupplyParameters { get; set; } // Ο πίνακας της φωτογραφίας
        public DbSet<NeutralizationCalculation> NeutralizationCalculations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Μοναδικότητα στον Αριθμό Παροχής Πελάτη
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.SupplyNumber)
                .IsUnique();

            modelBuilder.Entity<NeutralizationCalculation>()
                .Property(p => p.I_ShortCircuit)
                .HasConversion<double>();
        }
    }
}
