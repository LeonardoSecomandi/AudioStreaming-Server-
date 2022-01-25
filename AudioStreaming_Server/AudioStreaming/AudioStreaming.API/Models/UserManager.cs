using AudioStreaming.API.Data;
using AudioStreaming.API.Models.DTOS.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Models
{
    public class UserManager : IUserManager
    {

        private readonly UsersDbContext _context;

        public UserManager(UsersDbContext context)
        {
            this._context = context;
        }
        public async Task<UserTokenResponse> AddUserToken(UserTokens req)
        {
            try
            {
                var result = await _context.UserTokens.AddAsync(req);
                await _context.SaveChangesAsync();
                return new UserTokenResponse()
                {
                    Token = req.UserToken,
                    CreationTime = req.CreationTime,
                    Username = req.UserName
                };
            }
            catch(Exception ex)
            {
                return new UserTokenResponse()
                {
                    Token = null,
                    CreationTime = default(DateTime),
                    Username = null
                };
            }
            
            
        }
    }
}
