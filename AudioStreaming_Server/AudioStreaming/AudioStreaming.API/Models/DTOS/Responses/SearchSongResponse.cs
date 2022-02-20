using AudioStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Responses
{
    public class SearchSongResponse
    {
        public bool Success { get; set; }
        public IEnumerable<Canzone> Results { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
