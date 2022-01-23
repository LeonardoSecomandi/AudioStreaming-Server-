using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Requests
{
    public class ChangeDownloadNumberReq
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
