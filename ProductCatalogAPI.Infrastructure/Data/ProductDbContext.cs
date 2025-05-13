using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Core.Entities;

namespace ProductCatalogAPI.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                // Set precision and scale for decimal type 'Price' column
                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)"); // Example: 18 digits, 2 decimal places
            });
        }
    }
}
