using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class CreatePlaylistRequest
    {
        [Required]
        public string PlayListTitle { get; set; }

        [Required]
        public int UserId { get; set; }

        public string SongsIDs { get; set; }

        [Required]
        public bool Private { get; set; }
    }
}
