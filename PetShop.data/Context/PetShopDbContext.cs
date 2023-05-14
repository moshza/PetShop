using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PetShop.data.Models;

namespace PetShop.data.Context
{
    public partial class PetShopDbContext : DbContext
    {
        public PetShopDbContext()
        {
        }

        public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-C8FCNRF;Initial Catalog=PetShop1027;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.Description).IsFixedLength();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Animals__Categor__29572725");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__Animal__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
