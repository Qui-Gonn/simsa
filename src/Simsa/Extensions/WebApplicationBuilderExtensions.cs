namespace Simsa.Extensions;

using Simsa.Persistence;
using Simsa.Persistence.Extensions;
using Simsa.Services;

public static class WebApplicationBuilderExtensions
{
    public static void AddSimsaBackendServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

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