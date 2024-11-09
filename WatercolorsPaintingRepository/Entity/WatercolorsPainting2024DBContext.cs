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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);User Id=sa;Password=12345;Database=WatercolorsPainting2024DB;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("Style");

                entity.Property(e => e.StyleId).HasMaxLength(50);

                entity.Property(e => e.OriginalCountry).HasMaxLength(255);

                entity.Property(e => e.StyleName).HasMaxLength(255);
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
                entity.HasKey(e => e.PaintingId)
                    .HasName("PK__Watercol__CF2D90F25C045092");

                entity.ToTable("WatercolorsPainting");

                entity.Property(e => e.PaintingId).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaintingAuthor).HasMaxLength(255);

                entity.Property(e => e.PaintingName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StyleId).HasMaxLength(50);

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.WatercolorsPaintings)
                    .HasForeignKey(d => d.StyleId)
                    .HasConstraintName("FK_WatercolorsPainting_Style");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
