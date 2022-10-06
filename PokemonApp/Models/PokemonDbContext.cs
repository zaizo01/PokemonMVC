using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PokemonApp.Models;

namespace PokemonApp.Models
{
    public partial class PokemonDbContext : DbContext
    {
        public PokemonDbContext()
        {
        }

        public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PokemonRegion> PokemonRegions { get; set; } = null!;
        public virtual DbSet<PokemonType> PokemonTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=PokemonDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonRegion>(entity =>
            {
                entity.Property(e => e.Name)
                            .HasMaxLength(50)
                            .IsRequired();
            });

            modelBuilder.Entity<PokemonType>(entity =>
            {
                entity.Property(e => e.Name)
                                .HasMaxLength(50)
                                .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<PokemonApp.Models.Pokemon> Pokemon { get; set; }
    }
}
