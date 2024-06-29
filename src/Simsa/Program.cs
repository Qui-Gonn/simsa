using Simsa;
using Simsa.Extensions;
using Simsa.Interfaces.Features.EventManagement;
using Simsa.Ui.Library.Extensions;

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
    .AddAdditionalAssemblies(typeof(Simsa.Client._Imports).Assembly, typeof(Simsa.Ui.Library._Imports).Assembly);

////app.MapGet("license", () => builder.Configuration[ServiceCollectionExtension.SyncfusionLicenseKey]);

var eventsEndpoint = app.MapGroup("/api")
    .MapGroup("/events");
eventsEndpoint.MapGet(string.Empty, async (IEventService eventService) => TypedResults.Ok(await eventService.GetAllAsync()))
    .WithName("GetEvents");
eventsEndpoint.MapDelete(
        "{id}",
        async (Guid id, IEventService eventService) =>
        {
            await eventService.DeleteAsync(id);
            return TypedResults.NoContent();
        })
    .WithName("DeleteEventById");

app.Run();