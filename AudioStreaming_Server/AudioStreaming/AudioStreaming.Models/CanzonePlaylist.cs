using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AudioStreaming.Models
{
    public class CanzonePlaylist
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int SongID { get; set; }

        [JsonIgnore]
        public Canzone Canzone { get; set; }

        [Required]
        public int PlaylistID { get; set; }

        [JsonIgnore]
        public Playlist Playlist { get; set; }

    }
}
