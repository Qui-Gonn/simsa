using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Simsa.Client.Extensions;
using Simsa.Ui.Library.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

////await builder.ConfigureLicense();
////builder.Services.AddSimsaFrontEndServices(builder.Configuration);
builder.Services.AddSimsaUiServices(builder.Configuration);
builder.Services.AddSimsaClientServices(builder.Configuration, builder.HostEnvironment);

await builder.Build().RunAsync();