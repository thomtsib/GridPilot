using GridPilot.Data;
using GridPilot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GridPilot.Core
{
    public class CaseService
    {
        private readonly IDbContextFactory<GridPilotContext> _dbFactory;

        public CaseService(IDbContextFactory<GridPilotContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// Αναζητά τα παραμετρικά στοιχεία μιας παροχής ΧΤ με βάση τον κωδικό της (π.χ. "03", "1")
        /// </summary>
        public async Task<LvSupplyParameter> GetLvParameterByCodeAsync(string code)
        {
            using var context = await _dbFactory.CreateDbContextAsync();
            return await context.LvSupplyParameters.FirstOrDefaultAsync(p => p.Code == code);
        }

        /// <summary>
        /// Αποθηκεύει μια νέα υπόθεση καταναλωτή και την παροχή του στη βάση δεδομένων
        /// </summary>
        public async Task SaveCaseAsync(Customer customer, CustomerSupply supply)
        {
            using var context = await _dbFactory.CreateDbContextAsync();
            
            customer.Id = Guid.NewGuid();
            supply.Id = Guid.NewGuid();
            
            customer.CustomerSupplyId = supply.Id;
            
            context.CustomerSupplies.Add(supply);
            context.Customers.Add(customer);
            
            await context.SaveChangesAsync();
        }
    }
}
