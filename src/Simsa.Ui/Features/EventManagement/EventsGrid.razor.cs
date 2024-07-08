namespace Simsa.Ui.Features.EventManagement;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public partial class EventsGrid
{
    [Inject]
    public IDialogService Dialog { get; set; } = default!;

    [Inject]
    public IEventService EventService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<Event> Events { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await this.ReloadDataAsync();
        await base.OnInitializedAsync();
    }

    private void Add()
        => this.NavigationManager.NavigateTo("/events/add");

    private async Task Delete(Event item)
    {
        var confirmed = await this.Dialog.ShowMessageBox(
            $"Delete {item.Name}",
            $"Are you sure you want to delete \"{item.Name}\"? This action can not be undone!",
            "Yes",
            "No");

        if (confirmed ?? false)
        {
            await this.EventService.DeleteAsync(item.Id);
            await this.ReloadDataAsync();
        }
    }

    private void Edit(Event item)
        => this.NavigationManager.NavigateTo($"/events/edit/{item.Id}");

    private async Task ReloadDataAsync()
        => this.Events = await this.EventService.GetAllAsync();
}