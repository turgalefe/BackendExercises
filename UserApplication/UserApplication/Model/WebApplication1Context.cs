using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserApplication.Model;

public partial class WebApplication1Context : DbContext
{
    public WebApplication1Context()
    {
    }

    public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLikedMovie> UserLikedMovies { get; set; }

    public virtual DbSet<UserLikedTv> UserLikedTvs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=WebApplication1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Nickname);

            entity.ToTable("User");

            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<UserLikedMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("UserLikedMovie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("Movie_id");
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("nickname");
            entity.Property(e => e.Overview)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("overview");
            entity.Property(e => e.PosterPath)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("posterPath");
            entity.Property(e => e.ReleaseDate)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("releaseDate");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("title");
            entity.Property(e => e.VoteAverage).HasColumnName("vote_average");
            entity.Property(e => e.VoteCount).HasColumnName("vote_count");
        });

        modelBuilder.Entity<UserLikedTv>(entity =>
        {
            entity.HasKey(e => e.TvId);

            entity.ToTable("UserLikedTv");

            entity.Property(e => e.TvId)
                .ValueGeneratedNever()
                .HasColumnName("Tv_id");
            entity.Property(e => e.FirstAirDate)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("firstAirDate");
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("nickname");
            entity.Property(e => e.OriginalLanguage)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("originalLanguage");
            entity.Property(e => e.OriginalName)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("originalName");
            entity.Property(e => e.Overview)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("overview");
            entity.Property(e => e.VoteAverage).HasColumnName("vote_average");
            entity.Property(e => e.VoteCount).HasColumnName("vote_count");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
