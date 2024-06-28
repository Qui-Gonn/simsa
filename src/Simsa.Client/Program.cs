using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

////await builder.ConfigureLicense();

builder.AddSimsaFrontendServices();

await builder.Build().RunAsync();