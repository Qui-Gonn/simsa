namespace Simsa.Blazor.Library.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Blazor;
using Syncfusion.Licensing;

public static class ServiceCollectionExtension
{
    public const string SyncfusionLicenseKey = "SyncfusionLicense";

    public static IServiceCollection AddSimsaFrontEndServices(this IServiceCollection services, IConfiguration configuration)
    {
        SyncfusionLicenseProvider.RegisterLicense(configuration[SyncfusionLicenseKey]);
        services.AddSyncfusionBlazor();

        return services;
    }
}