namespace Simsa.Ui.Features.PersonManagement;

using Microsoft.AspNetCore.Components;

using Simsa.Core.Features.PersonManagement;
using Simsa.Model;

public partial class PersonEditPage
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public Guid? PersonId { get; set; }

    [Inject]
    public IPersonService PersonService { get; set; } = default!;

    private string Error { get; set; } = string.Empty;

    private bool IsError => !string.IsNullOrEmpty(this.Error);

    private bool IsNew { get; set; }

    private PersonEditItem Person { get; set; } = PersonEditItem.FromPerson(new Person());

    protected override async Task OnParametersSetAsync()
    {
        if (this.PersonId is { } personId)
        {
            var personByItd = await this.PersonService.GetByIdAsync(personId);

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
            await this.PersonService.AddAsync(item.Source);
        }
        else
        {
            await this.PersonService.UpdateAsync(item.Source);
        }

        this.NavigationManager.NavigateTo("/persons");
    }
}