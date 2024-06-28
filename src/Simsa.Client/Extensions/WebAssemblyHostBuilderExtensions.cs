namespace Simsa.Client.Extensions;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddSimsaFrontendServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(
            _ =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });
    }

    public static async Task ConfigureLicense(this WebAssemblyHostBuilder builder)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        using var response = await httpClient.GetAsync("license");
        if (response.IsSuccessStatusCode)
        {
            builder.Configuration[ServiceCollectionExtensions.SyncfusionLicenseKey] = await response.Content.ReadAsStringAsync();
        }
    }
}