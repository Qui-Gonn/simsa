namespace Simsa.Ui.Components;

using Microsoft.AspNetCore.Components;

public partial class EditableWraper<TItem>
{
    public enum DisplayMode
    {
        Overview,

        Details,

        Edit
    }

    [Parameter]
    public RenderFragment<TItem> DetailsTemplate { get; set; } = default!;

    [Parameter]
    public RenderFragment<TItem> EditModeTemplate { get; set; } = default!;

    public TItem? Item { get; set; }

    [Parameter]
    public DisplayMode Mode { get; set; } = DisplayMode.Overview;

    [Parameter]
    public RenderFragment OverviewTemplate { get; set; } = default!;

    public void StartEditMode(TItem item)
    {
        this.Mode = DisplayMode.Edit;
        this.Item = item;
    }
}