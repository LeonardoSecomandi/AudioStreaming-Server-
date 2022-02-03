using AudioStreaming.API.Models;
using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Controllers
{
    [ApiController]
    [Route("[Controller]/api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;
        public PlaylistController(IPlaylistRepository playlistRepository)
        {
            this._playlistRepository = playlistRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CreatePlaylistResponse>> CreatePlaylist(CreatePlaylistRequest req)
        {
            try
            {
                var result = await _playlistRepository.CreatePlaylist(req);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
        }

        [HttpGet]
        public async Task <IEnumerable<Playlist>> GetPlaylist()
        {
                var result = await _playlistRepository.GetPlaylist();
                return result;
        }


        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<GetUserPlaylistResponse>> GetUserPlaylist(int id)
        {
            try
            {
                var result = await _playlistRepository.GetUserPlaylist(id);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }

        }

        [HttpDelete("delete/{idPlaylist:int}")]
        public async Task<ActionResult<DeletePlaylistResponse>> DeletePlaylist(int idPlaylist)
        {
            try
            {
                var result = await _playlistRepository.DeletePlaylist(idPlaylist);
                return result;
            }
            catch (Exception)
            {
                var result = await _playlistRepository.DeletePlaylist(idPlaylist);
                return result;
            }
        }
    }
}
