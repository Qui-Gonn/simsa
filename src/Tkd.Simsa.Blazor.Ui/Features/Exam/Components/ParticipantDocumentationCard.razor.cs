using Microsoft.AspNetCore.Components;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.Ui.Features.Exam.Components;

/// <summary>
/// Component code-behind for ParticipantDocumentationCard.
/// </summary>
public partial class ParticipantDocumentationCard : ComponentBase
{
    private DisciplineRatingDto? currentRating;
    private string currentNotes = string.Empty;
    private DisciplineRatingDto? originalRating;
    private string originalNotes = string.Empty;
    
    /// <summary>
    /// Gets or sets the participant being documented.
    /// </summary>
    [Parameter]
    public required ParticipantDto Participant { get; set; }
    
    /// <summary>
    /// Gets or sets the current discipline being evaluated.
    /// </summary>
    [Parameter]
    public DisciplineDto? CurrentDiscipline { get; set; }
    
    /// <summary>
    /// Gets or sets the existing examination result for this participant.
    /// </summary>
    [Parameter]
    public ExaminationResultDto? ExistingResult { get; set; }
    
    /// <summary>
    /// Event callback when a result is saved.
    /// </summary>
    [Parameter]
    public EventCallback<ParticipantResultSavedEventArgs> OnResultSaved { get; set; }
    
    /// <summary>
    /// Gets whether the form can be saved.
    /// </summary>
    private bool CanSave => CurrentDiscipline != null && currentRating != null && !string.IsNullOrEmpty(currentNotes.Trim());
    
    /// <summary>
    /// Gets whether there are unsaved changes.
    /// </summary>
    private bool HasUnsavedChanges => 
        currentRating?.NumericValue != originalRating?.NumericValue ||
        currentRating?.Passed != originalRating?.Passed ||
        currentNotes != originalNotes;
    
    protected override void OnParametersSet()
    {
        LoadCurrentDisciplineData();
    }
    
    /// <summary>
    /// Loads data for the current discipline if it exists in the results.
    /// </summary>
    private void LoadCurrentDisciplineData()
    {
        if (CurrentDiscipline != null && ExistingResult?.Results != null)
        {
            var existingDisciplineResult = ExistingResult.Results
                .FirstOrDefault(kvp => kvp.Key.Name == CurrentDiscipline.Name);
                
            if (existingDisciplineResult.Key != null)
            {
                originalRating = existingDisciplineResult.Value.Rating;
                originalNotes = existingDisciplineResult.Value.Notes;
                currentRating = originalRating;
                currentNotes = originalNotes;
            }
            else
            {
                // No existing result for this discipline
                ResetForm();
            }
        }
        else
        {
            ResetForm();
        }
    }
    
    /// <summary>
    /// Resets the form to default values.
    /// </summary>
    private void ResetForm()
    {
        currentRating = null;
        currentNotes = string.Empty;
        originalRating = null;
        originalNotes = string.Empty;
    }
    
    /// <summary>
    /// Handles rating changes.
    /// </summary>
    /// <param name="rating">The new rating</param>
    private void OnRatingChanged(DisciplineRatingDto rating)
    {
        currentRating = rating;
        StateHasChanged();
    }
    
    /// <summary>
    /// Handles notes changes.
    /// </summary>
    /// <param name="notes">The new notes</param>
    private void OnNotesChanged(string notes)
    {
        currentNotes = notes;
        StateHasChanged();
    }
    
    /// <summary>
    /// Saves the current result.
    /// </summary>
    private async Task SaveResult()
    {
        if (!CanSave || CurrentDiscipline == null || currentRating == null)
            return;
            
        var eventArgs = new ParticipantResultSavedEventArgs(
            Participant,
            CurrentDiscipline,
            currentRating,
            currentNotes.Trim());
            
        await OnResultSaved.InvokeAsync(eventArgs);
        
        // Update the original values to reflect the saved state
        originalRating = currentRating;
        originalNotes = currentNotes;
        
        StateHasChanged();
    }
    
    /// <summary>
    /// Clears the form.
    /// </summary>
    private void ClearForm()
    {
        currentRating = null;
        currentNotes = string.Empty;
        StateHasChanged();
    }
    
    /// <summary>
    /// Loads the existing result into the form.
    /// </summary>
    private void LoadExistingResult()
    {
        currentRating = originalRating;
        currentNotes = originalNotes;
        StateHasChanged();
    }
    
    /// <summary>
    /// Checks if there's an existing result for the current discipline.
    /// </summary>
    /// <returns>True if there's an existing result</returns>
    private bool HasExistingResult()
    {
        return CurrentDiscipline != null && 
               ExistingResult?.Results != null &&
               ExistingResult.Results.Any(kvp => kvp.Key.Name == CurrentDiscipline.Name);
    }
    
    /// <summary>
    /// Gets existing notes for the current discipline.
    /// </summary>
    /// <returns>Existing notes or empty string</returns>
    private string GetExistingNotes()
    {
        if (CurrentDiscipline == null || ExistingResult?.Results == null)
            return string.Empty;
            
        var existingResult = ExistingResult.Results
            .FirstOrDefault(kvp => kvp.Key.Name == CurrentDiscipline.Name);
            
        return existingResult.Key != null ? existingResult.Value.Notes : string.Empty;
    }
    
    /// <summary>
    /// Gets the author of existing notes for the current discipline.
    /// </summary>
    /// <returns>Author name or empty string</returns>
    private string GetExistingNotesAuthor()
    {
        if (CurrentDiscipline == null || ExistingResult?.Results == null)
            return string.Empty;
            
        var existingResult = ExistingResult.Results
            .FirstOrDefault(kvp => kvp.Key.Name == CurrentDiscipline.Name);
            
        return existingResult.Key != null ? existingResult.Value.DocumentedBy : string.Empty;
    }
    
    /// <summary>
    /// Gets the date of existing notes for the current discipline.
    /// </summary>
    /// <returns>Date or null</returns>
    private DateTime? GetExistingNotesDate()
    {
        if (CurrentDiscipline == null || ExistingResult?.Results == null)
            return null;
            
        var existingResult = ExistingResult.Results
            .FirstOrDefault(kvp => kvp.Key.Name == CurrentDiscipline.Name);
            
        return existingResult.Key != null ? existingResult.Value.DocumentedAt : null;
    }
}

/// <summary>
/// Event arguments for when a participant result is saved.
/// </summary>
public record ParticipantResultSavedEventArgs(
    ParticipantDto Participant,
    DisciplineDto Discipline,
    DisciplineRatingDto Rating,
    string Notes);
