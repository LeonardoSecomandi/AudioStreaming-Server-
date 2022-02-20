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

            modelBuilder.Entity<CanzonePlaylist>()
                 .HasKey(bc => new { bc.SongID, bc.PlaylistID });
            
            modelBuilder.Entity<CanzonePlaylist>()
                .HasOne(bc => bc.Canzone)
                .WithMany(bc => bc.CanzonePlaylist)
                .HasForeignKey(bc => bc.SongID);

            modelBuilder.Entity<CanzonePlaylist>()
                .HasOne(bc => bc.Playlist)
                .WithMany(bc => bc.CanzonePlaylist)
                .HasForeignKey(bc => bc.PlaylistID);

        }
        public DbSet<Canzone> Canzoni { get; set; }
        public DbSet<Playlist> Playlist { get; set; }

        public DbSet<CanzonePlaylist> CanzonePlaylists { get; set; }
    }
}
