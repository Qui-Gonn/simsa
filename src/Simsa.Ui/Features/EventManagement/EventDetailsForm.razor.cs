namespace Simsa.Ui.Features.EventManagement;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor;

public partial class EventDetailsForm
{
    [Parameter]
    public required EventEditItem Event { get; set; }

    public MudForm FormRef { get; set; } = default!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public EventCallback<EventEditItem> OnSave { get; set; }

    private Variant Variant => Variant.Filled;

    private async Task CancelAsync()
    {
        await this.JsRuntime.InvokeVoidAsync("history.back");
    }

    private async Task SaveAsync()
    {
        if (this.OnSave.HasDelegate)
        {
            await this.OnSave.InvokeAsync(this.Event);
        }
    }
}