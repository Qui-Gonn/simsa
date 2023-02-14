namespace Simsa.Blazor.Client;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class WebAssemblyHostBuilderExtension
{
    public static async Task ReadLicenseJson(this WebAssemblyHostBuilder builder)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        using var response = await httpClient.GetAsync("license.json");
        if (response.IsSuccessStatusCode)
        {
            await using var stream = await response.Content.ReadAsStreamAsync();
            builder.Configuration.AddJsonStream(stream);
        }
    }
}