using AudioStreaming.API.Models.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public interface IUserManager
    {
        public Task<UserTokenResponse> AddUserToken(UserTokens req);
        public Task<UserTokenResponse> GetUserToken(string Username);
    }
}
