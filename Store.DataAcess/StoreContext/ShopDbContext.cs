using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using System;
using Shared.Enums;

namespace Store.DataAcess.StoreContext
{
    public class ShopDbContext : IdentityDbContext<StoreUser, IdentityRole<long>, long>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<StoreUser> StoreUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);
            modelBuilder.Entity<PrintingEdition>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);
            modelBuilder.Entity<StoreUser>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);
            modelBuilder.Entity<Order>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);
            modelBuilder.Entity<OrderItem>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);
            modelBuilder.Entity<Payment>().Property(x => x.CreationDate).HasDefaultValue(DateTime.MaxValue);

            modelBuilder.Entity<Author>()
                .HasData(
                new Author
                {
                    Id = 1,
                    Name = Shared.Constants.DefaultValues.DefaultAuthorName

                });

            modelBuilder.Entity<PrintingEdition>()
                .HasData(
                new PrintingEdition
                {
                    Id = 1,
                    Title = Shared.Constants.DefaultValues.DefaultPrintingEditionTitle,
                    Description = Shared.Constants.DefaultValues.DefaultPrintingEditionDescription,
                    Price = 300,
                    Status = Status.Available,
                    Type = Shared.Enums.Type.Journal,
                    Curency = Curency.USD,
                }
                );
        }
    }
}
