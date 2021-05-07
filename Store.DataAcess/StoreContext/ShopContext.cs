using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;

namespace Store.DataAcess.StoreContext
{
    public class ShopContext : IdentityDbContext<StoreUser, IdentityRole<long>, long>
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            
        }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
