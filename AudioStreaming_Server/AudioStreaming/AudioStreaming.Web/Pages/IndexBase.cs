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

        [Inject]
        public NavigationManager nv { get; set; }

        public IEnumerable<Canzone> EleCanzoni { get; set; }

        public static string CurrentPlayingSong { get; set; }
        public string a = "/";

        //public string Status { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EleCanzoni = await canzoniService.GetCanzoni();
            return;
        }

        public void ClickHandle(string Title)
        {
            CurrentPlayingSong = Title+".mp3";
        }
    }
}
