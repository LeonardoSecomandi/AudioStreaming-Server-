using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client_EasySound.Models
{
    public class client
    {
        [Key]
        public string id { get; set; }

        [Required]
        public string utente { get; set; }

    }
}
