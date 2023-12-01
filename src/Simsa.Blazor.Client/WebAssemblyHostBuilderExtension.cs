namespace Simsa.Blazor.Client;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Blazor.Library.Extensions;

public static class WebAssemblyHostBuilderExtension
{
    public static async Task ConfigureLicense(this WebAssemblyHostBuilder builder)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        using var response = await httpClient.GetAsync("license");
        if (response.IsSuccessStatusCode)
        {
            builder.Configuration[ServiceCollectionExtension.SyncfusionLicenseKey] = await response.Content.ReadAsStringAsync();
        }
    }
}