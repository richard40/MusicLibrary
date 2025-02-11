using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Models;

public partial class MusicLibraryDbContext : DbContext
{
    public MusicLibraryDbContext()
    {
    }

    public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }


    public virtual DbSet<Genre> Genres { get; set; }


    public virtual DbSet<Song> Songs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:musiclibrarydbserver.database.windows.net,1433;Initial Catalog=MusicLibrary_db;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication='Active Directory Default';");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.artist_id).HasName("PK__artist__6CD04001AE75753E");

            entity.ToTable("artist");

            entity.Property(e => e.artist_id).HasColumnName("artist_id");
            entity.Property(e => e.label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.origin)
                .HasMaxLength(50)
                .HasColumnName("origin");
            entity.Property(e => e.stagename)
                .HasMaxLength(100)
                .HasColumnName("stagename");
        });

   

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.genre_id).HasName("PK__genre__18428D428E986D21");

            entity.ToTable("genre");

            entity.Property(e => e.genre_id).HasColumnName("genre_id");
            entity.Property(e => e.title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

     

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.song_id).HasName("PK__song__A535AE1C9B24CB56");

            entity.ToTable("song");

            entity.Property(e => e.song_id).HasColumnName("song_id");
            entity.Property(e => e.artist_id).HasColumnName("artist_id");
            entity.Property(e => e.genre_id).HasColumnName("genre_id");
            entity.Property(e => e.title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Artist).WithMany(p => p.Songs)
                .HasForeignKey(d => d.artist_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__song__artist_id__60A75C0F");

            entity.HasOne(d => d.Genre).WithMany(p => p.Songs)
                .HasForeignKey(d => d.genre_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__song__genre_id__619B8048");
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
