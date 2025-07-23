namespace Tkd.Simsa.Blazor.WebApp.Extensions;

using Tkd.Simsa.Blazor.WebApp.Endpoints;
using Tkd.Simsa.DataGenerator;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence;

public static class WebApplicationExtensions
{
    public static void MapSimsaApiEndpoints(this WebApplication app)
    {
        var routeGroupBuilder = app.MapGroup("/api");
        routeGroupBuilder.MapDefaultEndpoints<Event>();
        routeGroupBuilder.MapDefaultEndpoints<Person>();
        
        // Map examination-specific endpoints
        app.MapExaminationEndpoints();
    }

    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await using var scope = app.Services.CreateAsyncScope();
            await DbContextInitializer.MigrateDatabaseAsync(scope.ServiceProvider);

            await new DataGenerator(scope.ServiceProvider).PopulateDatabaseAsync();
        }
    }
}