namespace Simsa.Ui.Features.PersonManagement;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor;

using Simsa.Core.Features.PersonManagement;

public partial class PersonDetailsForm
{
    public MudForm FormRef { get; set; } = default!;

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public EventCallback<PersonEditItem> OnSave { get; set; }

    [Parameter]
    public required PersonEditItem Person { get; set; }

    [Inject]
    public IPersonService PersonService { get; set; } = default!;

    private Variant Variant => Variant.Filled;

    private async Task CancelAsync()
    {
        await this.JsRuntime.InvokeVoidAsync("history.back");
    }

    private async Task SaveAsync()
    {
        if (this.OnSave.HasDelegate)
        {
            await this.OnSave.InvokeAsync(this.Person);
        }
    }
}