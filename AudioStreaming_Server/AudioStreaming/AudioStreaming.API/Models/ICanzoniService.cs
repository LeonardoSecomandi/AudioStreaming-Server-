using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public interface ICanzoniService
    {
        public Task<CreateCanzoneResponse> AddCanzone(CreateCanzoneRequest canzone);
        public Task<IEnumerable<Canzone>> GetCanzoni();

        public Task<SearchSongResponse> Search(SearchSongRequest req);

        public Task<Canzone> GetCanzone(int id);

        public Task<DeleteCanzoneResponse> DeleteCanzone(int id);

        public Task<UpdateSongResponse> UpdateCanzone(UpdateSongRequest req);

        public Task<ChangeDownloadNumberResp> ChangeDownloadNumber(ChangeDownloadNumberReq req);
    }
}
