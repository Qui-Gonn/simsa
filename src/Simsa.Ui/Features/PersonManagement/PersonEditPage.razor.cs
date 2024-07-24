namespace Simsa.Ui.Features.PersonManagement;

using MediatR;

using Microsoft.AspNetCore.Components;

using Simsa.Core.Features;
using Simsa.Model;

public partial class PersonEditPage
{
    [Inject]
    public IMediator Mediator { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public Guid? PersonId { get; set; }

    private string Error { get; set; } = string.Empty;

    private bool IsError => !string.IsNullOrEmpty(this.Error);

    private bool IsNew { get; set; }

    private PersonEditItem Person { get; set; } = PersonEditItem.FromPerson(new Person());

    protected override async Task OnParametersSetAsync()
    {
        if (this.PersonId is { } personId)
        {
            var personByItd = await this.Mediator.Send(new GetItemByIdQuery<Person>(personId));

            if (personByItd is not null)
            {
                this.Person = PersonEditItem.FromPerson(personByItd!);
            }
            else
            {
                this.Error = $"Person with id {personId} not found.";
            }
        }

        this.IsNew = this.PersonId is null;

        await base.OnParametersSetAsync();
    }

    private async Task SaveAsync(PersonEditItem item)
    {
        if (this.IsNew)
        {
            await this.Mediator.Send(new AddItemCommand<Person>(item.Source));
        }
        else
        {
            await this.Mediator.Send(new UpdateItemCommand<Person>(item.Source));
        }

        this.NavigationManager.NavigateTo("/persons");
    }
}