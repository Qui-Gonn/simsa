namespace Simsa.Ui.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using Simsa.Core.Features.EventManagement;
using Simsa.Model;

public partial class EventsEditPage
{
    [Parameter]
    public Guid? EventId { get; set; }

    [Inject]
    public IEventService EventService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string Error { get; set; } = string.Empty;

    private EventEditItem Event { get; set; } = EventEditItem.FromEvent(new Event());

    private bool IsError => !string.IsNullOrEmpty(this.Error);

    private bool IsNew { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (this.EventId is { } eventId)
        {
            var eventByItd = await this.EventService.GetByIdAsync(eventId);

            if (eventByItd is not null)
            {
                this.Event = EventEditItem.FromEvent(eventByItd!);
            }
            else
            {
                this.Error = $"Event with id {eventId} not found.";
            }
        }

        this.IsNew = this.EventId is null;

        await base.OnParametersSetAsync();
    }

    private async Task SaveAsync(EventEditItem item)
    {
        if (this.IsNew)
        {
            await this.EventService.AddAsync(item.Source);
        }
        else
        {
            await this.EventService.UpdateAsync(item.Source);
        }

        this.NavigationManager.NavigateTo("/events");
    }
}