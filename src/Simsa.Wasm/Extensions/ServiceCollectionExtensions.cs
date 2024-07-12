namespace Simsa.Wasm.Extensions;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Core.Features.EventManagement;
using Simsa.Core.Features.PersonManagement;
using Simsa.Wasm.Features.EventManagement;
using Simsa.Wasm.Features.PersonManagement;

////using Syncfusion.Blazor;
////using Syncfusion.Licensing;

public static class ServiceCollectionExtensions
{
    public const string SyncfusionLicenseKey = "SyncfusionLicense";

    public static IServiceCollection AddSimsaWasmServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddHttpClient<ApiHttpClient>(client => client.BaseAddress = new Uri(hostEnvironment.BaseAddress));

        ////SyncfusionLicenseProvider.RegisterLicense(configuration[SyncfusionLicenseKey]);
        ////services.AddSyncfusionBlazor();

        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}