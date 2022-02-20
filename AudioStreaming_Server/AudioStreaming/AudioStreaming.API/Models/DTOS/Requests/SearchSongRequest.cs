using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class SearchSongRequest
    {
        public string Title { get; set; }
        public string Album { get; set; }
    }
}
