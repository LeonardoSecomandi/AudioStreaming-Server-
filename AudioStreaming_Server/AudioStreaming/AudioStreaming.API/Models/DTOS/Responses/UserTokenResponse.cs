using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models.DTOS.Responses
{
    public class UserTokenResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
