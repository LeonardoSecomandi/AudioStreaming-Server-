using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class AddCanzoneToPlaylistRequest
    {
        [Required]
        public int IdPlaylist { get; set; }

        [Required]
        public int idCanzone { get; set; }

    }
}
