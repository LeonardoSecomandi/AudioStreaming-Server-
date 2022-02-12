using AudioStreaming.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Canzone>()
                .HasMany(p => p.Playlists)
                .WithMany(pa => pa.Canzones)
                .UsingEntity(j => j.ToTable("CanzonePlaylist"));

            modelBuilder.Entity<Canzone>()
                .HasData(new Canzone()
                {
                    SongID=1,
                    SongTitle = "Canzone1",
                    AlbumName = "Album1",
                    DownnloadNumber = 0,
                    Duration = 120,
                    IDUserUploader = 1
                });

            modelBuilder.Entity<Playlist>()
                .HasData(new Playlist()
                {
                    PlaylistID=1,
                    Name = "Playlist1",
                    UserID = 1,
                    Private = true
                });

            modelBuilder.Entity<Canzone>()
                .HasMany(p => p.Playlists)
                .WithMany(p => p.Canzones)
                .UsingEntity(j => j.HasData(new
                {
                    CanzonesCanzoneID = 1,
                    PlaylistsPlaylistID = 1

                }));

        }
        public DbSet<Canzone> Canzoni { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        
        //public DbSet<Canzone_Playlist> Canzone_Playlist { get; set; }
    }
}
