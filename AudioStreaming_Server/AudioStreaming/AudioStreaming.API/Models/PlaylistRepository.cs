using AudioStreaming.API.Data;
using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaylistRepository(ApplicationDbContext contetx)
        {
            this._context = contetx;
        }

        public async Task<CreatePlaylistResponse> CreatePlaylist(CreatePlaylistRequest req)
        {
            var ExistPlaylit = await _context.Playlist.FirstOrDefaultAsync(x => x.Name == req.PlayListTitle);
            if (ExistPlaylit != null)
                return new CreatePlaylistResponse()
                {
                    Success = false,
                    CreatedPlaylist = null,
                    Errors = null,
                    Message = "Playlist con nome " + req.PlayListTitle + " già esistene"
                };

            var newPlaylist = new Playlist()
            {
                Name = req.PlayListTitle,
                SongsIDs = req.SongsIDs,
                UserID = req.UserId,
                Private = req.Private
            };

            var result = await _context.AddAsync(newPlaylist);
            if (result.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return new CreatePlaylistResponse()
                {
                    Success = true,
                    CreatedPlaylist = await _context.Playlist.FirstOrDefaultAsync(x => x.Name == req.PlayListTitle),
                    Errors = null,
                    Message = "Playlist creata con successo"
                };
            }
            return new CreatePlaylistResponse()
            {
                Success = true,
                CreatedPlaylist = await _context.Playlist.FirstOrDefaultAsync(x => x.Name == req.PlayListTitle),
                Errors = null,
                Message = "Errore durante la creazione"
            };
        }

        public async Task<IEnumerable<Playlist>> GetPlaylist()
        {
            return await _context.Playlist.ToListAsync();
        }

        public async Task<GetUserPlaylistResponse> GetUserPlaylist(int IdUser)
        {
            var Playlist =await _context.Playlist.ToListAsync();
            var UserPlaylist= Playlist.Where(x => x.UserID == IdUser);
            if(!UserPlaylist.Any())
                return new GetUserPlaylistResponse()
                {
                    Success = true,
                    Playlist = UserPlaylist,
                    Errors = null,
                    Message = "Sono state trovate " + UserPlaylist.Count() + " associate all'utente"
                };

            return new GetUserPlaylistResponse()
            {
                Success = true,
                Playlist = UserPlaylist,
                Errors = null,
                Message = "Sono state trovate " + UserPlaylist.Count() + " associate all'utente"
            };
        }
    }
}
