using Microsoft.AspNetCore.Components;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.Ui.Features.Exam.Components;

/// <summary>
/// Component code-behind for ParticipantSelectionList.
/// </summary>
public partial class ParticipantSelectionList : ComponentBase
{
    private string searchFilter = string.Empty;
    
    /// <summary>
    /// Gets or sets the list of all participants.
    /// </summary>
    [Parameter]
    public IEnumerable<ParticipantDto> Participants { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the list of selected participants.
    /// </summary>
    [Parameter]
    public IEnumerable<ParticipantDto> SelectedParticipants { get; set; } = [];
    
    /// <summary>
    /// Event callback when participant selection changes.
    /// </summary>
    [Parameter]
    public EventCallback<IEnumerable<ParticipantDto>> OnSelectionChanged { get; set; }
    
    /// <summary>
    /// Gets the filtered participants based on search criteria.
    /// </summary>
    private IEnumerable<ParticipantDto> FilteredParticipants =>
        string.IsNullOrEmpty(searchFilter)
            ? Participants
            : Participants.Where(p => p.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase) ||
                                     p.BeltRank.Contains(searchFilter, StringComparison.OrdinalIgnoreCase));
    
    /// <summary>
    /// Gets whether all participants are selected.
    /// </summary>
    private bool IsAllSelected => Participants.Any() && SelectedParticipants.Count() == Participants.Count();
    
    /// <summary>
    /// Checks if a participant is selected.
    /// </summary>
    /// <param name="participant">The participant to check</param>
    /// <returns>True if selected</returns>
    private bool IsSelected(ParticipantDto participant) =>
        SelectedParticipants.Any(sp => sp.Id == participant.Id);
    
    /// <summary>
    /// Toggles the selection of a participant.
    /// </summary>
    /// <param name="participant">The participant to toggle</param>
    private async Task ToggleSelection(ParticipantDto participant)
    {
        var selectedList = SelectedParticipants.ToList();
        
        if (IsSelected(participant))
        {
            selectedList.RemoveAll(sp => sp.Id == participant.Id);
        }
        else
        {
            selectedList.Add(participant);
        }
        
        await OnSelectionChanged.InvokeAsync(selectedList);
    }
    
    /// <summary>
    /// Selects all participants.
    /// </summary>
    private async Task SelectAll()
    {
        await OnSelectionChanged.InvokeAsync(Participants.ToList());
    }
    
    /// <summary>
    /// Clears all participant selections.
    /// </summary>
    private async Task ClearSelection()
    {
        await OnSelectionChanged.InvokeAsync([]);
    }
    
    /// <summary>
    /// Clears the search filter.
    /// </summary>
    private void ClearSearch()
    {
        searchFilter = string.Empty;
    }
}
