namespace Simsa.Ui.Library.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public partial class EventsManagementComponent
{
    private EventEditItem? eventToAdd;

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
        => this.GridRef.SetEditingItemAsync(this.NewEvent());

    private async Task CommittedItemChangesAsync(EventEditItem item)
    {
        if (item == this.eventToAdd)
        {
            await this.EventService.AddAsync(item.Source);
        }
        else
        {
            await this.EventService.UpdateAsync(item.Source);
        }

        await this.ReloadDataAsync();
    }

    private async Task DeleteEventAsync(EventEditItem item)
    {
        await this.EventService.DeleteAsync(item.Source.Id);
        await this.ReloadDataAsync();
    }

    private EventEditItem NewEvent()
        => this.eventToAdd = EventEditItem.FromEvent(new Event());

    private async Task ReloadDataAsync()
    {
        this.eventToAdd = null;
        var allEvents = await this.EventService.GetAllAsync();
        this.AllEvents = allEvents.Select(EventEditItem.FromEvent).ToArray();
    }
}