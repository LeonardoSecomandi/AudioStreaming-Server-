using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class UpdateSongRequest
    {
        [Required]
        public int id { get; set; }
        public string SongTitle { get; set; }
        public string AlbumName { get; set; }
        public int Duration { get; set; }
    }
}
