namespace Simsa;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public partial class App
{
    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    private IComponentRenderMode? RenderModeForPage
        => this.HttpContext.Request.Path.StartsWithSegments("/Error")
            ? null
            : RenderMode.InteractiveAuto;
}