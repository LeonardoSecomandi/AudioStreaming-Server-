using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioStreaming.Models;

namespace AudioStreaming.API.Models
{
    public interface ICanzoniService
    {
        public Task<Canzone> AddCanzone(Canzone canzone);
        public Task<IEnumerable<Canzone>> GetCanzoni();

        public Task<IEnumerable<Canzone>> Search(string? Title,string? Album);
    }
}
