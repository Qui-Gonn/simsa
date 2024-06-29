﻿namespace Simsa.Extensions;

using Simsa.Features.EventManagement;
using Simsa.Interfaces.Features.EventManagement;

public static class WebApplicationBuilderExtensions
{
    public static void AddSimsaBackendServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventService, EventService>();
    }
}