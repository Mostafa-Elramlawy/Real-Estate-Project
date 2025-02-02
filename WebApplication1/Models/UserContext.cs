using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PP1.Models
{
    public class UserContext : DbContext
    {
        public DbSet<user> Users { get; set; }
    }
}