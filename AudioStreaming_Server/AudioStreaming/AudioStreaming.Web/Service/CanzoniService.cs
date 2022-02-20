using AudioStreaming.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AudioStreaming.Web.Service
{
    public class CanzoniService : ICanzoniService
    {
        private readonly HttpClient _httpClient;

        public CanzoniService(HttpClient client)
        {
            this._httpClient = client;
        }

        public async Task<IEnumerable<Canzone>> GetCanzoni()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImY2MDIyZjM1LWJlNjgtNGNlZC04NjkzLTczMjRjMzA3Zjc2NyIsImVtYWlsIjoiZWFzeXNvdW5kQGdtYWlsLmNvbSIsInN1YiI6ImVhc3lzb3VuZEBnbWFpbC5jb20iLCJqdGkiOiI2Y2UzZTEzMC04OGNmLTRhZTEtYmUxNy02NjJhZTgyNTExYjgiLCJuYmYiOjE2NDM3MDk5MTcsImV4cCI6MTY0MzcxMzUxNywiaWF0IjoxNjQzNzA5OTE3fQ.u783DbgPJhT5VikX7iPfKuEpC61SI4ndrdblX6FnE4Y");
            var result = await _httpClient.GetJsonAsync<IEnumerable<Canzone>>("Canzoni/api");
            return result;
        }
    }
}
