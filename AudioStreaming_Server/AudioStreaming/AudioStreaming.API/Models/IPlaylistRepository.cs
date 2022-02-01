using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public interface IPlaylistRepository
    {
        public Task<CreatePlaylistResponse> CreatePlaylist(CreatePlaylistRequest req);
        public Task<IEnumerable<Playlist>> GetPlaylist();
        public Task<GetUserPlaylistResponse> GetUserPlaylist(int IdUser);

        public Task<DeletePlaylistResponse> DeletePlaylist(int idPlaylist);
    }
}
