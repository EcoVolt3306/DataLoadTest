#pragma checksum "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "799867297afef82544bc07593f229f517191865b"
// <auto-generated/>
#pragma warning disable 1591
namespace DataLoadTest.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using DataLoadTest;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\_Imports.razor"
using DataLoadTest.Shared;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "sidebar");
            __builder.AddMarkupContent(2, "\n    ");
            __builder.OpenComponent<DataLoadTest.Shared.NavMenu>(3);
            __builder.CloseComponent();
            __builder.AddMarkupContent(4, "\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\n\n");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "main");
            __builder.AddMarkupContent(8, "\n    ");
            __builder.AddMarkupContent(9, "<div class=\"top-row px-4\">\n        <a href=\"https://docs.microsoft.com/aspnet/\" target=\"_blank\">About</a>\n    </div>\n\n    ");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "content px-4");
            __builder.AddMarkupContent(12, "\n        ");
            __builder.AddContent(13, 
#nullable restore
#line 13 "C:\Users\Seungwoo\source\repos\EcoVolt3306\DataLoadTest\DataLoadTest\Shared\MainLayout.razor"
         Body

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(14, "\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
