using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AudioStreaming.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistID { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public bool Private { get; set; }

        [Required]
        public int UserID { get; set; }

        public ICollection<Canzone> Canzones { get; set; }
    }
}
