namespace Simsa.Ui.Library.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using Simsa.Interfaces.Features.EventManagement;

public partial class EventsManagementComponent
{
    [Inject]
    public IEventService EventService { get; set; } = null!;

    private EventEditItem[] AllEvents { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        var allEvents = await this.EventService.GetAllAsync();
        this.AllEvents = allEvents.Select(EventEditItem.FromEvent).ToArray();

        await base.OnInitializedAsync();
    }
}