using Microsoft.EntityFrameworkCore;
using ProductOrdering.Entity;

namespace ProductOrdering.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> dbContext) : base(dbContext)
        {

        }
        public DbSet<Order> Orders { get; set; }

    }
}
