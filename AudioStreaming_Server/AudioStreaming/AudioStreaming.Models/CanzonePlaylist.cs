using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AudioStreaming.Models
{
    public class CanzonePlaylist
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int SongID { get; set; }

        public Canzone Canzone { get; set; }

        [Required]
        public int PlaylistID { get; set; }

        public Playlist Playlist { get; set; }

    }
}
