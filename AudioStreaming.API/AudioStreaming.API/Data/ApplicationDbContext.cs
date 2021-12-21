using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AudioStreaming.Models;

namespace AudioStreaming.API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        
        }
        public DbSet<Canzone> Canzoni { get; set; }
        public DbSet<Playlist> Playlist { get; set; }

        internal Task firtOrDefaultAsync()
        {
            throw new NotImplementedException();
        }
    }
}
