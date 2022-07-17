using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductsAPI.Data
{
    public partial class ProductsDbContext : DbContext
    {
        public ProductsDbContext()
        {
        }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("invoice_date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(50)
                    .HasColumnName("invoice_number");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Invoice__product__29572725");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("cost");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Product__categor__267ABA7A");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
