using System.Data.Entity;

namespace ClassLibrary2
{
    public class PropertyContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<Property_img> PropertyImages { get; set; }
        public PropertyContext(string connectionString) : base(connectionString)
        {
        }
    }
}
