namespace Simsa.Extensions;

using Simsa.Features.EventManagement;
using Simsa.Features.PersonManagement;
using Simsa.Interfaces.Features.EventManagement;
using Simsa.Interfaces.Features.PersonManagement;

public static class WebApplicationBuilderExtensions
{
    public static void AddSimsaBackendServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
    }
}