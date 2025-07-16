namespace Tkd.Simsa.Blazor.Ui.Features.Exam;

using MediatR;

using Microsoft.AspNetCore.Components;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Blazor.Ui.App;
using Tkd.Simsa.Domain.EventManagement;

public partial class ChooseExamPage : ComponentBase
{
    private IEnumerable<Event>? events;

    [Inject]
    private IMediator Mediator { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        this.events = await this.Mediator.Send(new GetItemsQuery<Event>(QueryParameters<Event>.Empty));
    }

    private void OpenExamDocumentation(Guid eventId)
    {
        this.NavigationManager.NavigateTo($"{RouteConstants.ExamDocumentation}/{eventId}");
    }
}