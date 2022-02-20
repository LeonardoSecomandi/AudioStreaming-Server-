using AudioStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Web.Service
{
    public interface ICanzoniService
    {
        public Task<IEnumerable<Canzone>> GetCanzoni();
    }
}
