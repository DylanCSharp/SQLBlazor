using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Syncfusion.Blazor;

namespace SQLBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDM5NTUyQDMxMzkyZTMxMmUzMGxpQlJNQzM0WEwzS3BXM3RaMGY2SnRHajZFVFpvVVlDSjArUFo1a1A1MzA9");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();
           
            await builder.Build().RunAsync();
        }
    }
}
