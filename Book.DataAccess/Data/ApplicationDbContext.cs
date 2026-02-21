using Book.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category Seed
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            // Product Seed
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Atomic Habits",
                    Description = "Self improvement book",
                    ISBN = "ABC123",
                    Author = "James Clear",
                    ListPrice = 500,
                    Price = 450,
                    Price50 = 400,
                    Price100 = 350,
                    CategoryId = 3
                },
                new Product
                {
                    Id = 2,
                    Title = "Dune",
                    Description = "Sci-Fi novel",
                    ISBN = "XYZ789",
                    Author = "Frank Herbert",
                    ListPrice = 600,
                    Price = 550,
                    Price50 = 500,
                    Price100 = 450,
                    CategoryId = 2
                }
            );
        }
    }
}