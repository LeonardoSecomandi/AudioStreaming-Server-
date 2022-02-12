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

        public async Task<DeletePlaylistResponse> DeletePlaylist(int idPlaylist)
        {
            var Playlist = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == idPlaylist);
            if (Playlist != null)
            {
                _context.Remove(Playlist);
               await _context.SaveChangesAsync();
                return new DeletePlaylistResponse()
                {
                    Success = true,
                    DeletedPlaylist = Playlist,
                    Message = "Playlist Eliminata",
                    Errors = null
                };
            }
            return new DeletePlaylistResponse()
            {
                Success = false,
                DeletedPlaylist = null,
                Message = "Playlist Non eliminata",
                Errors = null
            };
        }

        public async Task<AddCanzoneToPlaylistResponse> AddCanzoneToPlaylist(AddCanzoneToPlaylistRequest req)
        {
            var isPlaylist = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == req.idCanzone);
            if (isPlaylist is null)
                return new AddCanzoneToPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "errore nel recupero della playlist"
                };
            var isCanzone = await _context.Canzoni.FirstOrDefaultAsync(X => X.SongID == req.idCanzone);
            if(isCanzone is null)
                return new AddCanzoneToPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "errore nel recupero della canzone"
                };

            var newRelation= new CanzonePlaylist()
            {
                SongID = isCanzone.SongID,
                PlaylistID = isPlaylist.PlaylistID,
                Canzone = isCanzone,
                Playlist = isPlaylist
            };

            await _context.Canzone_Playlist.AddAsync(newRelation);


            //if (isPlaylist.Canzones is null)
            //{
            //    isPlaylist.Canzones = new List<Canzone_Playlist>();
            //}
            //if (isCanzone.Playlists is null)
            //    isCanzone.Playlists = new List<Canzone_Playlist>();

            //isPlaylist.Canzones.Add(newRelation);
            //isCanzone.Playlists.Add(newRelation);
            await _context.SaveChangesAsync();
            //var b = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == req.IdPlaylist);
            return new AddCanzoneToPlaylistResponse()
            {
                Success = true,
                Errors = null,
                Message = "Canzone aggiunta alla playlist"
            };

        }
    }
}
