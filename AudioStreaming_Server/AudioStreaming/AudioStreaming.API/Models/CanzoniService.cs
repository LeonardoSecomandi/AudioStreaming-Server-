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
    public class CanzoniService : ICanzoniService
    {
        private ApplicationDbContext _context { get; set; }
        public CanzoniService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<CreateCanzoneResponse> AddCanzone(CreateCanzoneRequest canzone)
        {
            if (canzone is null)
                return null;

            var Exist = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongTitle == canzone.SongTitle && x.IDUserUploader == canzone.IDUserUploader);
            if (Exist != null)
                return new CreateCanzoneResponse() {
                    Success = false,
                    CreatedCaznone = null,
                    Message = "Canzone Già presente",
                    Errors=null
                };
            var result = await _context.AddAsync(new Canzone()
            {

                SongTitle = canzone.SongTitle,
                AlbumName = canzone.AlbumName,
                IDUserUploader = canzone.IDUserUploader,
                Duration = canzone.Duration,
                DownnloadNumber = 0
            }); 
            await _context.SaveChangesAsync();


            return new CreateCanzoneResponse()
            {
                Success = true,
                CreatedCaznone = await _context.Canzoni.FirstOrDefaultAsync(x=>x.SongTitle==canzone.SongTitle && x.AlbumName==canzone.AlbumName && x.IDUserUploader==canzone.IDUserUploader),
                Message="Canzone Aggiunta",
                Errors = null
            };
        }
        public async Task<IEnumerable<Canzone>> GetCanzoni()
        {
            return await _context.Canzoni.ToListAsync();
        }

        public async Task<SearchSongResponse> Search(SearchSongRequest req)
        {
            List<Canzone> songs = new List<Canzone>();

            if (!string.IsNullOrEmpty(req.Title))
            {
                 songs =await _context.Canzoni.Where(x => x.SongTitle.ToLower().Trim().Contains(req.Title.ToLower().Trim())).ToListAsync();
                if (!string.IsNullOrEmpty(req.Album))
                {
                    songs = songs.Where(x => x.AlbumName.ToLower().Trim().Contains(req.Album.ToLower().Trim())).ToList();
                }
                return new SearchSongResponse()
                {
                    Success = true,
                    Message = "Risultati Ottenuti: "+songs.Count,
                    Results = songs,
                    Errors = null
                };
            }
            if (!string.IsNullOrEmpty(req.Album))
            {
                 songs = await _context.Canzoni.Where(x => x.AlbumName.ToLower().Trim().Contains(req.Album.ToLower().Trim())).ToListAsync();
                if (!string.IsNullOrEmpty(req.Title))
                {
                    songs = songs.Where(x => x.SongTitle.Contains(req.Title.ToLower().Trim())).ToList();
                }
                return new SearchSongResponse()
                {
                    Success = true,
                    Message = "Risultati Ottenuti: " + songs.Count,
                    Results = songs,
                    Errors = null
                };
            }
            return new SearchSongResponse()
            {
                Success = false,
                Message = "Specifica parametri ricerca",
                Results = null,
                Errors = null
            };
        }

        public async Task<Canzone> GetCanzone(int id)
        {
            var issong = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == id);
            if (issong != null)
                return issong;
            return null;
        }

        public async Task<DeleteCanzoneResponse> DeleteCanzone(int id)
        {
            var Canzone = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == id);
            if (Canzone != null)
            {
                _context.Canzoni.Remove(Canzone);
                await _context.SaveChangesAsync();
                if(await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == id) == null)
                {
                    return new DeleteCanzoneResponse()
                    {
                        Success = true,
                        DeletedCanzone = Canzone,
                        Message="Canzone con id "+Canzone.SongID+" Eliminata",
                        Errors=null
                    };
                }
                return new DeleteCanzoneResponse()
                {
                    Success = false,
                    DeletedCanzone = null,
                    Message = "Eliminazione della canzone con id "+Canzone.SongID+" fallita",
                    Errors = null
                };
            }
               
            return null;
        }

        public async Task<UpdateSongResponse> UpdateCanzone(UpdateSongRequest req)
        {
            var canzone = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == req.id);
            if (canzone != null)
            {
                try
                {
                    canzone.SongTitle = req.SongTitle;
                    canzone.AlbumName = req.AlbumName;
                    canzone.Duration = req.Duration;

                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return new UpdateSongResponse()
                    {
                        Success = false,
                        UpdatedCanzone = null,
                        Message = "Modifica della canzone con id " + req.id + " non riuscita",
                        Errors = new List<string>() { ex.Message}
                    };
                }
               
                return new UpdateSongResponse()
                {
                    Success = true,
                    UpdatedCanzone = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == req.id),
                    Message = "Canzone con id " + req.id + " Modificata",
                    Errors = null
                };
            }
            return new UpdateSongResponse()
            {
                Success = false,
                UpdatedCanzone = null,
                Message = "Modifica della canzone con id "+req.id+" non riuscita",
                Errors = null
            };
        }

        public async Task<ChangeDownloadNumberResp> ChangeDownloadNumber(ChangeDownloadNumberReq req)
        {
            var Song = await _context.Canzoni.FirstOrDefaultAsync(x => x.SongID == req.id);
            if (Song != null)
            {
                try
                {
                    Song.DownnloadNumber += req.Value;
                    await _context.SaveChangesAsync();
                    return new ChangeDownloadNumberResp()
                    {
                        Success = true,
                        Canzone = Song,
                        Message = "Modifica Effettuata",
                        Errors = null

                    };
                }
                catch(Exception ex)
                {
                    return new ChangeDownloadNumberResp()
                    {
                        Success = false,
                        Canzone = Song,
                        Message = ex.Message,
                        Errors = null
                    };
                }
            }
            return new ChangeDownloadNumberResp()
            {
                Success = false,
                Canzone = null,
                Message = "Canzone con id "+req.id+" non trovata",
                Errors = null
            };




        }
    }
}
