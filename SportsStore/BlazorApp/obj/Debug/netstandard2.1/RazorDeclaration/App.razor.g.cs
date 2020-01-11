#pragma checksum "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\App.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9ddac0f162cd27d99908bcb55d4d3aee0e19da1"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorApp
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using BlazorApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\_Imports.razor"
using BlazorApp.Shared;

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "C:\Users\alanm_9qb660b\git\repos\netCoreAndAngular3\netCoreAndAngular3\netCoreAndAngular3\SportsStore\BlazorApp\App.razor"
      
    static readonly string productsUrl = "/api/products";
    SearchSettings search = new SearchSettings();
    string results = "No results to display";
    async void HandleSearch(){
        if(search.searchTerm != String.Empty){
            Product[] prods = await Http.GetJsonAsync<Product[]>($"{productsUrl}/?search={search.searchTerm}");
            decimal totalPrice = prods.Select(p => p.Price).Sum();
            results = $"{ prods.Length } products, total price is ${ totalPrice }";
            StateHasChanged();
        }
    }
    class Product{
        public decimal Price{get;set;}
    }
    class SearchSettings{
        public string searchTerm;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
