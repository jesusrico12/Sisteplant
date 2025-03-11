using Microsoft.EntityFrameworkCore;
using Sisteplant.Domain.Exercises.Exercise3;

namespace Sisteplant.Infrastructure.Exercises
{
    public class SisteplantContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        public SisteplantContext(DbContextOptions<SisteplantContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasKey(i => i.ItemCode);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);
            // Define the foreign key relationship between `Order` and `Item`
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Item) // Navigation property in Order
                .WithMany(i => i.Orders) // Navigation property in Item
                .HasForeignKey(o => o.ItemOrdered) // ForeignKey property in Order
                .HasPrincipalKey(i => i.ItemCode) // Principal key in Item
                .OnDelete(DeleteBehavior.Cascade); // Optional: Define cascade delete behavior
        }
    }
}
