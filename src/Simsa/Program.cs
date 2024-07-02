using Microsoft.AspNetCore.Http.HttpResults;

using Simsa;
using Simsa.Extensions;
using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;
using Simsa.Ui.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

////builder.Configuration.AddJsonFile("license.json");

builder.Services.AddSimsaUiServices(builder.Configuration);
builder.AddSimsaBackendServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Simsa.Wasm._Imports).Assembly, typeof(Simsa.Ui._Imports).Assembly);

////app.MapGet("license", () => builder.Configuration[ServiceCollectionExtension.SyncfusionLicenseKey]);

var eventsEndpoint = app.MapGroup("/api")
    .MapGroup("/events");
eventsEndpoint.MapGet(
        string.Empty,
        async (IEventService eventService)
            => TypedResults.Ok(await eventService.GetAllAsync()))
    .WithName("GetEvents");
eventsEndpoint.MapGet(
        "{id}",
        async Task<Results<Ok<Event>, NotFound>> (Guid id, IEventService eventService)
            => await eventService.GetById(id) is { } itemById ? TypedResults.Ok(itemById) : TypedResults.NotFound())
    .WithName("GetEventById");
eventsEndpoint.MapPost(
        string.Empty,
        async (Event newItem, IEventService eventService) =>
        {
            await eventService.AddAsync(newItem);
            return TypedResults.Created($"/api/events/{newItem.Id}", newItem);
        })
    .WithName("AddEvent");
eventsEndpoint.MapPut(
        "{id}",
        async (Guid id, Event updatedItem, IEventService eventService) =>
        {
            await eventService.UpdateAsync(updatedItem);
            return TypedResults.Ok();
        })
    .WithName("UpdateEvent");
eventsEndpoint.MapDelete(
        "{id}",
        async (Guid id, IEventService eventService) =>
        {
            await eventService.DeleteAsync(id);
            return TypedResults.NoContent();
        })
    .WithName("DeleteEvent");

app.Run();