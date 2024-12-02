using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project01.Model;

public partial class Project01Context : DbContext
{
    public Project01Context()
    {
    }

    public Project01Context(DbContextOptions<Project01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLiked> UserLikeds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Project01;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(256)
                .IsFixedLength();
            entity.Property(e => e.RefreshTokenEndDate).HasColumnType("datetime");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<UserLiked>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserLiked");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Comments)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.IsLiked).HasColumnName("isLiked");
            entity.Property(e => e.PostComments)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
