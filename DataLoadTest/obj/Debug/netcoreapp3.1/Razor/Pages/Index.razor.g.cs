#pragma checksum "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "622bb3d921ec994a550a3ae0f4e5d0774b959750"
// <auto-generated/>
#pragma warning disable 1591
namespace DataLoadTest.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using DataLoadTest;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\_Imports.razor"
using DataLoadTest.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Data Load TEST</h1>\r\n<br>\r\n");
            __builder.AddMarkupContent(1, "<table id=\"loadTest\" class=\"display\" style=\"width:100%;\"><thead><tr><th>DATA1</th>\r\n            <th>DATA2</th>\r\n            <th>DATA3</th>\r\n            <th>DATA4</th>\r\n            <th>DATA5</th></tr></thead></table>");
        }
        #pragma warning restore 1998
#nullable restore
#line 41 "C:\Users\Seungwoo\source\repos\DataLoadTest\DataLoadTest\Pages\Index.razor"
       

    private bool IsRender { get; set; } = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("Tables");
        }
        else
        {
            Console.Out.WriteLine("Load blazor.server.js");
        }
        base.OnAfterRender(firstRender);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
    }
}
#pragma warning restore 1591
