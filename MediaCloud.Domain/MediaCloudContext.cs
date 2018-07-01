using System;
using System.IO;
using MediaCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain {

    public class MediaCloudContext : DbContext {

        public DbSet<Item> Items { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Genre> Genres { get; set; }

		public DbSet<ItemLibrary> ItemLibraries { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<ItemGenre> ItemGenres { get; set; }
        public DbSet<MovieLibrary> MovieLibraries { get; set; }
        public DbSet<SeriesLibrary> SeriesLibraries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=" + Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"MediaCloud\mediacloud.db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Item-Library many-to-many
            modelBuilder.Entity<ItemLibrary>().HasKey(il => new {il.ItemId, il.LibraryId});

            modelBuilder.Entity<ItemLibrary>()
                .HasOne(li => li.Item)
                .WithMany(li => li.ItemLibraries)
                .HasForeignKey(li => li.ItemId)
                .IsRequired();

            modelBuilder.Entity<ItemLibrary>()
                .HasOne(li => li.Library)
                .WithMany(li => li.ItemLibraries)
                .HasForeignKey(li => li.LibraryId)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .HasMany(g => g.ItemLibraries)
                .WithOne(ig => ig.Item)
                .OnDelete(DeleteBehavior.Cascade);

            //Item-Genre many-to-many
            modelBuilder.Entity<ItemGenre>().HasKey(il => new { il.ItemId, il.GenreId });

            modelBuilder.Entity<ItemGenre>()
                .HasOne(li => li.Item)
                .WithMany(li => li.ItemGenres)
                .HasForeignKey(li => li.ItemId)
                .IsRequired();

            modelBuilder.Entity<ItemGenre>()
                .HasOne(li => li.Genre)
                .WithMany(li => li.ItemGenres)
                .HasForeignKey(li => li.GenreId)
                .IsRequired();

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.ItemGenres)
                .WithOne(ig => ig.Genre)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Season>()
                .HasOne(s => s.Series)
                .WithMany(s => s.Seasons)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Episode>()
                .HasOne(e => e.Season)
                .WithMany(s => s.Episodes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Episode)
                .WithMany(e => e.Media)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Library)
                .WithMany(l => l.Media)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Movie)
                .WithMany(m => m.Media)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
