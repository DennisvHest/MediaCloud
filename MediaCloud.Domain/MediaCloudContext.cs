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
                .HasForeignKey(li => li.ItemId);

            modelBuilder.Entity<ItemLibrary>()
                .HasOne(li => li.Library)
                .WithMany(li => li.ItemLibraries)
                .HasForeignKey(li => li.LibraryId);

            //Item-Genre many-to-many
            modelBuilder.Entity<ItemGenre>().HasKey(il => new { il.ItemId, il.GenreId });

            modelBuilder.Entity<ItemGenre>()
                .HasOne(li => li.Item)
                .WithMany(li => li.ItemGenres)
                .HasForeignKey(li => li.ItemId);

            modelBuilder.Entity<ItemGenre>()
                .HasOne(li => li.Genre)
                .WithMany(li => li.ItemGenres)
                .HasForeignKey(li => li.GenreId);
        }
    }
}
