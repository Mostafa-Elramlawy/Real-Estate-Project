using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ClassLibrary2.Models
{
    public class TransactionsContext : DbContext
    {
        public DbSet<Transactions> Transactions { get; set; }
    }
}