using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEF.Models
{
    public class music
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public int anno { get; set; }
        [Required]
        public string giocatore { get; set; }
        [Required]
        public string squadra { get; set; }
    }
}
