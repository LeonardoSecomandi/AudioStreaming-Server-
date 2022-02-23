using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcpIp.Models
{
    class CanzoniService : ICanzoniService
    {
        private readonly HttpClient client = new HttpClient(new HttpClientHandler() { 
            ServerCertificateCustomValidationCallback= (message, cert, chain, errors) => { return true; }
        });
        public async Task<IEnumerable<Canzone>> GetCanzoni()
        {
           
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImY2MDIyZjM1LWJlNjgtNGNlZC04NjkzLTczMjRjMzA3Zjc2NyIsImVtYWlsIjoiZWFzeXNvdW5kQGdtYWlsLmNvbSIsInN1YiI6ImVhc3lzb3VuZEBnbWFpbC5jb20iLCJqdGkiOiI2Y2UzZTEzMC04OGNmLTRhZTEtYmUxNy02NjJhZTgyNTExYjgiLCJuYmYiOjE2NDM3MDk5MTcsImV4cCI6MTY0MzcxMzUxNywiaWF0IjoxNjQzNzA5OTE3fQ.u783DbgPJhT5VikX7iPfKuEpC61SI4ndrdblX6FnE4Y");
            var result = await client.GetAsync("https://localhost:44347/Canzoni/api");
            var resultstring =await result.Content.ReadAsStringAsync();
            var canzoni = JsonConvert.DeserializeObject<IEnumerable<Canzone>>(resultstring);
            return canzoni;
        }
    }
}
