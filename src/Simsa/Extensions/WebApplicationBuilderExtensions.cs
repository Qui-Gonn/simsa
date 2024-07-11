namespace Simsa.Extensions;

using Simsa.Features.EventManagement;
using Simsa.Features.PersonManagement;
using Simsa.Interfaces.Features.EventManagement;
using Simsa.Interfaces.Features.PersonManagement;
using Simsa.Persistence;
using Simsa.Persistence.Extensions;
using Simsa.Services;

public static class WebApplicationBuilderExtensions
{
    public static void AddSimsaBackendServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IPersonService, PersonService>();

        var connectionString = builder.Configuration.GetConnectionString("SimsaDb") ?? string.Empty;
        builder.Services.AddSimsaPersistenceServices(connectionString);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddHostedService<DataGeneratorBackgroundService>();
        }
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, IWebHostEnvironment hostEnvironment)
    {
        if (hostEnvironment.IsDevelopment())
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            DbContextInitializer.MigrateDatabase(serviceScope.ServiceProvider);
        }

        return app;
    }
}