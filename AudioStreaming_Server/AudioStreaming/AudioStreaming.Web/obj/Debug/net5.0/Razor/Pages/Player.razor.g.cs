#pragma checksum "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\Pages\Player.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1525d38ce3657948bc5fcadfe09449dc77778cd7"
// <auto-generated/>
#pragma warning disable 1591
namespace AudioStreaming.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using AudioStreaming.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using AudioStreaming.Web.Shared;

#line default
#line hidden
#nullable disable
    public partial class Player : PlayerBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "audio");
            __builder.AddAttribute(1, "controls");
            __builder.AddAttribute(2, "autoplay");
            __builder.OpenElement(3, "source");
            __builder.AddAttribute(4, "src", "/SongRepository/" + (
#nullable restore
#line 4 "C:\Users\wallo\OneDrive\Documenti\GitHub\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\Pages\Player.razor"
                                  CurrentPlayingSong

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(5, "type", "audio/mpeg");
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\r\n    ");
            __builder.AddMarkupContent(7, "<p>\r\n        Your browser doesn\'t support HTML5 audio. Here is\r\n        a <a href=\"myAudio.mp3\">link to the audio</a> instead.\r\n    </p>");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
