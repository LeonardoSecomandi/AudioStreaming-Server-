#pragma checksum "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\Shared\LoginDisplay.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51daf6efca5c6253ec6950be2a7a39ebf560639a"
// <auto-generated/>
#pragma warning disable 1591
namespace AudioStreaming.Web.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using AudioStreaming.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\_Imports.razor"
using AudioStreaming.Web.Shared;

#line default
#line hidden
#nullable disable
    public partial class LoginDisplay : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(0);
            __builder.AddAttribute(1, "Authorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(2, "a");
                __builder2.AddAttribute(3, "href", "Identity/Account/Manage");
                __builder2.AddContent(4, "Hello, ");
                __builder2.AddContent(5, 
#nullable restore
#line 3 "D:\GitHub\repository1\AudioStreaming-Server-\AudioStreaming_Server\AudioStreaming\AudioStreaming.Web\Shared\LoginDisplay.razor"
                                                  context.User.Identity.Name

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(6, "!");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(7, "\r\n        ");
                __builder2.AddMarkupContent(8, "<form method=\"post\" action=\"Identity/Account/LogOut\"><button type=\"submit\" class=\"nav-link btn btn-link\">Log out</button></form>");
            }
            ));
            __builder.AddAttribute(9, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(10, "<a href=\"Identity/Account/Register\">Register</a>\r\n        ");
                __builder2.AddMarkupContent(11, "<a href=\"Identity/Account/Login\">Log in</a>");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
