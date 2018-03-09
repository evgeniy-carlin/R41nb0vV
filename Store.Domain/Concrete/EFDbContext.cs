using System.Data.Entity;
using Store.Domain.Entities;

namespace Store.Domain.Concrete
{
    public class  EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
