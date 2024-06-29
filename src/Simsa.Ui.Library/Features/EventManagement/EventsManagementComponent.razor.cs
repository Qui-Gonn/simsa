namespace Simsa.Ui.Library.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public partial class EventsManagementComponent
{
    [Inject]
    public IEventService EventService { get; set; } = default!;

    private EventEditItem[] AllEvents { get; set; } = [];

    private MudDataGrid<EventEditItem> GridRef { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await this.ReloadDataAsync();
        await base.OnInitializedAsync();
    }

    private Task AddNewEventAsync()
        => this.GridRef.SetEditingItemAsync(EventEditItem.FromEvent(new Event()));

    private async Task DeleteEventAsync(EventEditItem item)
    {
        await this.EventService.DeleteAsync(item.Source.Id);
        await this.ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        var allEvents = await this.EventService.GetAllAsync();
        this.AllEvents = allEvents.Select(EventEditItem.FromEvent).ToArray();
    }
}