using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class CreateCanzoneRequest
    {
        [Required]
        public string SongTitle { get; set; }

        public string AlbumName { get; set; }

        [Required]
        public int IDUserUploader { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
