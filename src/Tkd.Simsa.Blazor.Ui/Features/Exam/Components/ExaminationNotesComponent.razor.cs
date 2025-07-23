using Microsoft.AspNetCore.Components;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.Ui.Features.Exam.Components;

/// <summary>
/// Component code-behind for ExaminationNotesComponent.
/// </summary>
public partial class ExaminationNotesComponent : ComponentBase
{
    private string notesContent = string.Empty;
    private bool showTemplates = false;
    
    private readonly Dictionary<string, string> noteTemplates = new()
    {
        { "Excellent", "Excellent technique and performance. Shows mastery of the discipline." },
        { "Good", "Good performance with minor areas for improvement." },
        { "Needs Work", "Technique needs improvement. Recommend additional practice." },
        { "Form Issues", "Form and posture need attention. Focus on fundamental positions." },
        { "Timing", "Work on timing and rhythm. Practice with metronome or counting." },
        { "Power", "Increase power and intensity in techniques." },
        { "Balance", "Improve balance and stability during execution." },
        { "Confidence", "Build confidence through repetition and practice." },
        { "Focus", "Maintain better focus and concentration during performance." },
        { "Breathing", "Pay attention to proper breathing techniques." }
    };
    
    /// <summary>
    /// Gets or sets the participant being documented.
    /// </summary>
    [Parameter]
    public ParticipantDto? Participant { get; set; }
    
    /// <summary>
    /// Gets or sets the discipline being documented.
    /// </summary>
    [Parameter]
    public DisciplineDto? Discipline { get; set; }
    
    /// <summary>
    /// Gets or sets the current notes content.
    /// </summary>
    [Parameter]
    public string Notes { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets existing notes from previous evaluations.
    /// </summary>
    [Parameter]
    public string ExistingNotes { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the author of existing notes.
    /// </summary>
    [Parameter]
    public string ExistingNotesAuthor { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the date of existing notes.
    /// </summary>
    [Parameter]
    public DateTime? ExistingNotesDate { get; set; }
    
    /// <summary>
    /// Event callback when notes change.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnNotesChanged { get; set; }
    
    /// <summary>
    /// Gets the maximum length for notes.
    /// </summary>
    private const int MaxLength = 2000;
    
    /// <summary>
    /// Gets whether there are existing notes to display.
    /// </summary>
    private bool HasExistingNotes => !string.IsNullOrEmpty(ExistingNotes);
    
    protected override void OnParametersSet()
    {
        notesContent = Notes;
    }
    
    /// <summary>
    /// Handles notes content changes.
    /// </summary>
    private async Task HandleNotesChanged()
    {
        await OnNotesChanged.InvokeAsync(notesContent);
    }
    
    /// <summary>
    /// Clears the notes content.
    /// </summary>
    private async Task ClearNotes()
    {
        notesContent = string.Empty;
        await OnNotesChanged.InvokeAsync(notesContent);
        await InvokeAsync(StateHasChanged);
    }
    
    /// <summary>
    /// Toggles the template panel visibility.
    /// </summary>
    private void ToggleTemplates()
    {
        showTemplates = !showTemplates;
    }
    
    /// <summary>
    /// Adds a template to the notes content.
    /// </summary>
    /// <param name="templateText">The template text to add</param>
    private async Task AddTemplate(string templateText)
    {
        if (!string.IsNullOrEmpty(notesContent) && !notesContent.EndsWith("\n"))
        {
            notesContent += "\n";
        }
        
        notesContent += templateText;
        
        // Ensure we don't exceed max length
        if (notesContent.Length > MaxLength)
        {
            notesContent = notesContent[..MaxLength];
        }
        
        await OnNotesChanged.InvokeAsync(notesContent);
        await InvokeAsync(StateHasChanged);
    }
}
