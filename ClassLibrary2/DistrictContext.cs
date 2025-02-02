using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class DistrictContext : DbContext
    {
        public DbSet<District> Districts { get; set; }
    }
}
