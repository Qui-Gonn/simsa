using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace Simsa.Blazor.Library.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSimsaFrontEndServices(this IServiceCollection services, IConfiguration configuration)
    {
        SyncfusionLicenseProvider.RegisterLicense(configuration["SyncfusionLicense"]);
        services.AddSyncfusionBlazor();

        return services;
    }
}
