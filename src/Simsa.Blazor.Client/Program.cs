using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Blazor.Client;
using Simsa.Blazor.Library.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.ConfigureLicense();
builder.Services.AddSimsaFrontEndServices(builder.Configuration);

await builder.Build().RunAsync();