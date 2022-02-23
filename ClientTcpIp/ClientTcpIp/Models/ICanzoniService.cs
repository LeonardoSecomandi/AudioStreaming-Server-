using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcpIp.Models
{
    public interface ICanzoniService
    {
        public Task<IEnumerable<Canzone>> GetCanzoni();
    }
}
