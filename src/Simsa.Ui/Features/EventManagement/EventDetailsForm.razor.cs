namespace Simsa.Ui.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using MudBlazor;

public partial class EventDetailsForm
{
    [Parameter]
    public required EventEditItem Event { get; set; }

    public MudForm FormRef { get; set; } = default!;
}