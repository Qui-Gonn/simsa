namespace Simsa.Ui.Library.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimsaUiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMudServices();
        return services;
    }
}