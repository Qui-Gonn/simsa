namespace Simsa.Ui.Features;

using System.Linq.Expressions;

using MediatR;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Simsa.Core.Features;
using Simsa.Model;

public partial class DataManagementGrid<TItem>
    where TItem : IHasId<Guid>
{
    [Parameter]
    public required RenderFragment Columns { get; set; }

    [Inject]
    public IDialogService Dialog { get; set; } = default!;

    [Parameter]
    public required Expression<Func<TItem, string>> ItemTextExpression { get; set; }

    [Inject]
    public IMediator Mediator { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public required string PageRoute { get; set; }

    private IEnumerable<TItem> Items { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await this.ReloadDataAsync();
        await base.OnInitializedAsync();
    }

    private void Add()
        => this.NavigationManager.NavigateTo($"{this.PageRoute}/add");

    private async Task Delete(TItem item)
    {
        var confirmed = await this.Dialog.ShowMessageBox(
            $"Delete {this.GetItemText(item)}",
            new MarkupString(
                $"Are you sure you want to delete <strong class=\"mud-secondary-text\">{this.GetItemText(item)}</strong>? This action can not be undone!"),
            "Yes",
            "No");

        if (confirmed ?? false)
        {
            await this.Mediator.Send(new DeleteItemCommand<TItem>(item.Id));
            await this.ReloadDataAsync();
        }
    }

    private void Edit(TItem item)
        => this.NavigationManager.NavigateTo($"{this.PageRoute}/edit/{item.Id}");

    private string GetItemText(TItem item)
        => this.ItemTextExpression.Compile().Invoke(item);

    private async Task ReloadDataAsync()
        => this.Items = await this.Mediator.Send(new GetAllItemsQuery<TItem>());
}