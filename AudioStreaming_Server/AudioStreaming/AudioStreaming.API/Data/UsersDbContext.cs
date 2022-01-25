using AudioStreaming.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Data
{
    public class UsersDbContext : IdentityDbContext<UserModel>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserTokens> UserTokens { get; set; }

    }
}
