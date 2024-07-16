namespace Simsa.Wasm.Extensions;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Core.Services;
using Simsa.Model;
using Simsa.Wasm.Features;

public static class ServiceCollectionExtensions
{
    public const string SyncfusionLicenseKey = "SyncfusionLicense";

    public static IServiceCollection AddSimsaWasmServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        ////SyncfusionLicenseProvider.RegisterLicense(configuration[SyncfusionLicenseKey]);
        ////services.AddSyncfusionBlazor();

        services.AddHttpClient<ApiHttpClient>(client => client.BaseAddress = new Uri(hostEnvironment.BaseAddress));

        services.RegisterApiClientService<Event>();
        services.RegisterApiClientService<Person>();

        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

        return services;
    }

    private static void RegisterApiClientService<TItem>(this IServiceCollection services)
        where TItem : IHasId<Guid>
    {
        services.AddScoped<IGenericItemService<TItem>, GenericItemService<TItem>>();
        services.Configure<EndpointConfig<TItem>>(config => config.ApiEndpoint = $"api/{typeof(TItem).Name}");
    }
}