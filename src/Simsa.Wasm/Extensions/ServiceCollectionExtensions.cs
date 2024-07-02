namespace Simsa.Wasm.Extensions;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Wasm.Features.EventManagement;
using Simsa.Interfaces.Features.EventManagement;

////using Syncfusion.Blazor;
////using Syncfusion.Licensing;

public static class ServiceCollectionExtensions
{
    public const string SyncfusionLicenseKey = "SyncfusionLicense";

    public static IServiceCollection AddSimsaClientServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddScoped(
            _ =>
                new HttpClient
                {
                    BaseAddress = new Uri(hostEnvironment.BaseAddress)
                });

        ////SyncfusionLicenseProvider.RegisterLicense(configuration[SyncfusionLicenseKey]);
        ////services.AddSyncfusionBlazor();
        services.AddScoped<IEventService, EventService>();
        return services;
    }
}