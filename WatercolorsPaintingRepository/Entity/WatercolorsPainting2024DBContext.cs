using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WatercolorsPaintingRepository.Entity
{
    public partial class WatercolorsPainting2024DBContext : DbContext
    {
        public WatercolorsPainting2024DBContext()
        {
        }

        public WatercolorsPainting2024DBContext(DbContextOptions<WatercolorsPainting2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Style> Styles { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<WatercolorsPainting> WatercolorsPaintings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.StyleId);

                entity.ToTable("Style");

                entity.Property(e => e.StyleId).HasMaxLength(50);

                entity.Property(e => e.OriginalCountry).HasMaxLength(255);

                entity.Property(e => e.StyleName).HasMaxLength(255);

                entity.Property(e => e.StyleDescription);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
				entity.HasKey(e => e.UserAccountId);

				entity.ToTable("UserAccount");

				entity.Property(e => e.UserAccountId).HasColumnName("UserAccountId");
				entity.Property(e => e.UserEmail).HasColumnName("UserEmail");
				entity.Property(e => e.UserFullName).HasColumnName("UserFullName");
				entity.Property(e => e.UserPassword).HasColumnName("UserPassword");
				entity.Property(e => e.Role).HasColumnName("Role");
			});

            modelBuilder.Entity<WatercolorsPainting>(entity =>
            {
                entity.HasKey(e => e.PaintingId);

                entity.ToTable("WatercolorsPainting");

                entity.Property(e => e.PaintingId).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaintingAuthor).HasMaxLength(255);

                entity.Property(e => e.PaintingName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StyleId).HasMaxLength(50);

                entity.HasOne(d => d.Style)
                    .WithMany()
                    .HasForeignKey(d => d.StyleId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
