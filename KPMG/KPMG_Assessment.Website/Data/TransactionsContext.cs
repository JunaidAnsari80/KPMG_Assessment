using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPMG_Assessment.Website.Data
{
    /// <summary>
    /// Code first 
    /// </summary>
    public class TransactionsContext : DbContext
    {
        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public TransactionsContext(DbContextOptions<TransactionsContext> options)
            : base(options)
        { }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

   

}
