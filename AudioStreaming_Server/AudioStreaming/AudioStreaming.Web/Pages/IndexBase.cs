using AudioStreaming.Models;
using AudioStreaming.Web.Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public ICanzoniService canzoniService { get; set; }

        public IEnumerable<Canzone> EleCanzoni { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EleCanzoni = await canzoniService.GetCanzoni();
            return;
        }
    }
}
