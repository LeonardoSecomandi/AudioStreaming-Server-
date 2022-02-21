using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.Web.Pages
{
    public class PlayerBase : ComponentBase
    {
        private string _CurrentPlayingSong;
        [Inject]
        public NavigationManager nv { get; set; }

        [Parameter]
        public  string CurrentPlayingSong
        {
            get {
                return this._CurrentPlayingSong;
            }
            
            set
            {
                this._CurrentPlayingSong = value;
            }
        }
    }
}
