using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using Microsoft.EntityFrameworkCore;


namespace CoffeeOrderingApiWithCQRSandMediatR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        public DbSet<CoffeeOrder> CoffeeOrders { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

    }
}
