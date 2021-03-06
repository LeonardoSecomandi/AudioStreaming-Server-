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
            List<Playlist> ElePlaylist = new List<Playlist>();
            var Playlist = await _context.Playlist.ToListAsync();
            var CanzonePlaylist = await _context.CanzonePlaylists.ToListAsync();
            foreach(var item in Playlist)
            {
                item.CanzonePlaylist = CanzonePlaylist.Where(x=>x.PlaylistID==item.PlaylistID).ToList();
                ElePlaylist.Add(item);
            }
            return ElePlaylist;
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
            var isPlaylist = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == req.IdPlaylist);
            if (isPlaylist is null)
                return new AddCanzoneToPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "errore nel recupero della playlist"
                };
            var isCanzone = await _context.Canzoni.FirstOrDefaultAsync(X => X.SongID == req.idCanzone);


           var ispresent=await _context.CanzonePlaylists.FirstOrDefaultAsync(x => x.PlaylistID == isPlaylist.PlaylistID && x.Canzone.SongID == isCanzone.SongID);
            if(ispresent!=null)
                return new AddCanzoneToPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "canzone già presente nella playlist"
                };

            if (isCanzone is null)
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

            // await _context.Canzone_Playlist.AddAsync(newRelation);


            if (isPlaylist.CanzonePlaylist is null)
            {
                isPlaylist.CanzonePlaylist = new List<CanzonePlaylist>();
            }
            if (isCanzone.CanzonePlaylist is null)
                isCanzone.CanzonePlaylist = new List<CanzonePlaylist>();

            isPlaylist.CanzonePlaylist.Add(newRelation);
            isCanzone.CanzonePlaylist.Add(newRelation);
            await _context.SaveChangesAsync();
            //var b = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == req.IdPlaylist);
            return new AddCanzoneToPlaylistResponse()
            {
                Success = true,
                Errors = null,
                Message = "Canzone aggiunta alla playlist"
            };

        }

        public async Task<RemoveCanzoneFromPlaylistResponse> RemoveCanzoneFromPlaylist(AddCanzoneToPlaylistRequest req) 
        {
            var isPlaylist = await _context.Playlist.FirstOrDefaultAsync(x => x.PlaylistID == req.IdPlaylist);
            if (isPlaylist is null)
                return new RemoveCanzoneFromPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "errore nel recupero della playlist",
                    Playlist=null
                };

            var isCanzone = await _context.Canzoni.FirstOrDefaultAsync(X => X.SongID == req.idCanzone);

            var ispresent = await _context.CanzonePlaylists.FirstOrDefaultAsync(x => x.PlaylistID == isPlaylist.PlaylistID && x.Canzone.SongID == isCanzone.SongID);
            if (ispresent == null)
                return new RemoveCanzoneFromPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "canzone non presente nella playlist"
                };

            if (isCanzone is null)
                return new RemoveCanzoneFromPlaylistResponse()
                {
                    Success = false,
                    Errors = null,
                    Message = "errore nel recupero della canzone",
                    Playlist=null
                };
            var Relation = await _context.CanzonePlaylists.FirstOrDefaultAsync(x => x.PlaylistID == req.IdPlaylist && x.SongID==req.idCanzone);
            if (Relation is null)
                return new RemoveCanzoneFromPlaylistResponse()
                {
                    Success = false,
                    Message = "Errore nella rimozione della canzone",
                    Errors = null,
                    Playlist = null
                };

            _context.Remove(Relation);
            await _context.SaveChangesAsync();
            return new RemoveCanzoneFromPlaylistResponse()
            {
                Success = true,
                Message = "Canzone Rimossa dalla playlist",
                Playlist = isPlaylist,
                Errors = null
            };

            
        }
    }
}
