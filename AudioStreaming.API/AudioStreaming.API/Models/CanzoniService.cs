using AudioStreaming.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public class CanzoniService : ICanzoniService
    {

        private ApplicationDbContext _context { get; set; }
        public CanzoniService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Canzone> AddCanzone(Canzone canzone)
        {
            if (canzone is null)
                return null;

            var Exist = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongTitle == canzone.SongTitle && x.IDUserUploader == canzone.IDUserUploader);
            if (Exist != null)
                return null;
            var result=await _context.AddAsync(canzone);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<IEnumerable<Canzone>> GetCanzoni()
        {
            return await _context.Canzoni.ToListAsync();
        }

        public async Task<IEnumerable<Canzone>> Search()
        {

        }
    }
}
