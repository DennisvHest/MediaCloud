using System;
using System.IO;
using MediaCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain {

    public class MediaCloudContext : DbContext {

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Season> Season { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Media> Media { get; set; }

		public virtual DbSet<ItemLibrary> ItemLibraries { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<MovieLibrary> MovieLibraries { get; set; }
        public virtual DbSet<SeriesLibrary> SeriesLibraries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=" + Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"MediaCloud\mediacloud.db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ItemLibrary>().HasKey(il => new {il.ItemId, il.LibraryId});

            modelBuilder.Entity<ItemLibrary>()
                .HasOne(li => li.Item)
                .WithMany(li => li.ItemLibraries)
                .HasForeignKey(li => li.ItemId);

            modelBuilder.Entity<ItemLibrary>()
                .HasOne(li => li.Library)
                .WithMany(li => li.ItemLibraries)
                .HasForeignKey(li => li.LibraryId);
        }
    }
}
