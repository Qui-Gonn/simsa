namespace Simsa.Blazor.Library.Components;

using Microsoft.AspNetCore.Components;

public partial class TestForm : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public Person Person { get; set; } = default!;
}