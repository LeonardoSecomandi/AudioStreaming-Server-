using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public class UserTokens
    {
        [Key]
        public int idUser { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserToken { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
    }

}
