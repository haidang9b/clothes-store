using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Entities
{
    public class ClothingContext : DbContext
    {
        public ClothingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Role> roles { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Bill> bills { get; set; }
        public DbSet<BillDetail> billDetails { get; set; }
    }
}
