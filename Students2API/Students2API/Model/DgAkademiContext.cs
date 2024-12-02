using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Students2API.Model;

public partial class DgAkademiContext : DbContext
{
    public DgAkademiContext()
    {
    }

    public DgAkademiContext(DbContextOptions<DgAkademiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=DgAkademi;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Class");

            entity.ToTable("classes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.student");

            entity.ToTable("student");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Classid)
                .ValueGeneratedOnAdd()
                .HasColumnName("classid");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("password");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_student_classes1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
