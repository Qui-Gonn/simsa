using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Ui.Extensions;
using Simsa.Wasm.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

////await builder.ConfigureLicense();
////builder.Services.AddSimsaFrontEndServices(builder.Configuration);
builder.Services.AddSimsaUiServices(builder.Configuration);
builder.Services.AddSimsaWasmServices(builder.Configuration, builder.HostEnvironment);

await builder.Build().RunAsync();