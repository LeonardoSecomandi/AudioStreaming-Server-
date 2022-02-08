﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AudioStreaming.Models
{
    public class Canzone
    {
        [Key]
        [Required]
        public int SongID { get; set; }

        [Required]
        [MinLength(2)]
        public string SongTitle { get; set; }

        [MinLength(2)]
        public string AlbumName { get; set; }

        [Required]
        public int IDUserUploader { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int DownnloadNumber { get; set; }

        public ICollection<Playlist> ElePlaylist { get; set; }
    }
}
