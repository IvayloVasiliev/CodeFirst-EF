namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;
    using System;

    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.OnModelCreatingProduct(modelBuilder);

            this.OnModelCreatingCustomer(modelBuilder);

            this.OnModelCreatingStore(modelBuilder);

            this.OnModelCreatingSale(modelBuilder);
        }

        private void OnModelCreatingSale(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Sale>()
              .HasKey(s => s.SaleId);


        }

        private void OnModelCreatingStore(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Store>()
              .HasKey(s => s.StoreId);

            modelBuilder
               .Entity<Store>()
               .Property(c => c.Name)
               .HasMaxLength(80)
               .IsUnicode();

            modelBuilder
               .Entity<Store>()
               .HasMany(s => s.Sales)
               .WithOne(x => x.Store);

        }

        private void OnModelCreatingCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<Customer>()
              .HasKey(c => c.CustomerId);

            modelBuilder
                .Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsUnicode();

            modelBuilder
               .Entity<Customer>()
               .Property(c => c.Email)
               .HasMaxLength(80);

            modelBuilder
                .Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Customer);

        }

        private void OnModelCreatingProduct(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Product>()
               .HasKey(p => p.ProductId);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode();

            modelBuilder
                .Entity<Product>()
                .HasMany(p => p.Sales)
                .WithOne(s => s.Product);

            modelBuilder
                .Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(250);
        }
    }
}
