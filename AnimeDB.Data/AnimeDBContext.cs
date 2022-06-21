using AnimeDB.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimeDB.Data
{
    public partial class AnimeDBContext : DbContext
    {
        private string DatabasePath;

        public AnimeDBContext(string databasePath)
            : base()
        {
            this.DatabasePath = databasePath;
        }

        public virtual DbSet<AniList> AniList { get; set; } = null!;
        public virtual DbSet<AniListMyAnimeList> AniListMyAnimeList { get; set; } = null!;
        public virtual DbSet<Download> Download { get; set; } = null!;
        public virtual DbSet<Media> Media { get; set; } = null!;
        public virtual DbSet<MyAnimeList> MyAnimeList { get; set; } = null!;
        public virtual DbSet<Personal> Personal { get; set; } = null!;
        public virtual DbSet<Source> Source { get; set; } = null!;
        public virtual DbSet<SourceType> SourceType { get; set; } = null!;
        public virtual DbSet<Theme> Theme { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source={this.DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AniList>(entity =>
            {
                entity.ToTable("AniList");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AniListMyAnimeList>(entity =>
            {
                entity.HasKey(e => new { e.MyAnimeListId, e.AniListId });

                entity.ToTable("AniList_MyAnimeList");

                entity.HasOne(d => d.AniList)
                    .WithMany(p => p.AniListMyAnimeLists)
                    .HasForeignKey(d => d.AniListId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MyAnimeList)
                    .WithMany(p => p.AniListMyAnimeLists)
                    .HasForeignKey(d => d.MyAnimeListId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.ToTable("Download");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Key)
                    .WithMany(p => p.Downloads)
                    .HasForeignKey(d => d.KeyId);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.ToTable("Media");
                
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.ThemeId);
            });

            modelBuilder.Entity<MyAnimeList>(entity =>
            {
                entity.ToTable("MyAnimeList");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => new { e.AniListId, e.UserId });

                entity.ToTable("Personal");

                entity.HasOne(d => d.AniList)
                    .WithMany(p => p.Personals)
                    .HasForeignKey(d => d.AniListId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Personals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("Source");

                entity.HasIndex(e => e.KeyId, "IX_Source_KeyId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.SourceType)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.SourceTypeId);
            });

            modelBuilder.Entity<SourceType>(entity =>
            {
                entity.ToTable("SourceType");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("Theme");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Theme1).HasColumnName("Theme");

                entity.HasOne(d => d.Key)
                    .WithMany(p => p.Themes)
                    .HasPrincipalKey(p => p.KeyId)
                    .HasForeignKey(d => d.KeyId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
