﻿namespace Simsa.Ui.Features.PersonManagement;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Simsa.Core.Features.PersonManagement;
using Simsa.Model;

public partial class PersonsGrid
{
    [Inject]
    public IDialogService Dialog { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IPersonService PersonService { get; set; } = default!;

    private IEnumerable<Person> Persons { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await this.ReloadDataAsync();
        await base.OnInitializedAsync();
    }

    private void Add()
        => this.NavigationManager.NavigateTo("/persons/add");

    private async Task Delete(Person item)
    {
        var confirmed = await this.Dialog.ShowMessageBox(
            $"Delete {item.FirstName} {item.LastName}",
            $"Are you sure you want to delete \"{item.FirstName} {item.LastName}\"? This action can not be undone!",
            "Yes",
            "No");

        if (confirmed ?? false)
        {
            await this.PersonService.DeleteAsync(item.Id);
            await this.ReloadDataAsync();
        }
    }

    private void Edit(Person item)
        => this.NavigationManager.NavigateTo($"/persons/edit/{item.Id}");

    private async Task ReloadDataAsync()
        => this.Persons = await this.PersonService.GetAllAsync();
}