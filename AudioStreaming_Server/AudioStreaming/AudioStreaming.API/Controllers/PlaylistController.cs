using AudioStreaming.API.Models;
using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Controllers
{
    [ApiController]
    [Route("[Controller]/api")]
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
            var result = await _playlistRepository.CreatePlaylist(req);
            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<Playlist>> GetPlaylist()
        {
            var result = await _playlistRepository.GetPlaylist();
            return result;
        }


        [HttpGet("userd/{id:int}")]
        public async Task<GetUserPlaylistResponse> GetUserPlaylist(int id)
        {
            var result = await _playlistRepository.GetUserPlaylist(id);
            return result;
        }
    }
}
