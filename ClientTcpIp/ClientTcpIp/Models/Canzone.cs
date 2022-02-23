using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClientTcpIp.Models
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
        
    }
}

