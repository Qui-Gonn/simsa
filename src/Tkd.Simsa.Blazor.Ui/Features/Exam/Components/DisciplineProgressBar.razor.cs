using Microsoft.AspNetCore.Components;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.Ui.Features.Exam.Components;

/// <summary>
/// Component code-behind for DisciplineProgressBar.
/// </summary>
public partial class DisciplineProgressBar : ComponentBase
{
    /// <summary>
    /// Gets or sets the list of disciplines for the examination.
    /// </summary>
    [Parameter]
    public IReadOnlyList<DisciplineDto> Disciplines { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the current active discipline.
    /// </summary>
    [Parameter]
    public DisciplineDto? CurrentDiscipline { get; set; }
    
    /// <summary>
    /// Gets or sets the list of completed disciplines.
    /// </summary>
    [Parameter]
    public IReadOnlyList<DisciplineDto> CompletedDisciplines { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the completion percentage.
    /// </summary>
    [Parameter]
    public decimal CompletionPercentage { get; set; }
    
    /// <summary>
    /// Event callback when a discipline is selected.
    /// </summary>
    [Parameter]
    public EventCallback<DisciplineDto> OnDisciplineSelected { get; set; }
    
    /// <summary>
    /// Gets the CSS class for a progress step based on its state.
    /// </summary>
    /// <param name="discipline">The discipline to get the class for</param>
    /// <returns>CSS class string</returns>
    private string GetStepClass(DisciplineDto discipline)
    {
        if (CompletedDisciplines.Any(cd => cd.Name == discipline.Name))
            return "completed";
            
        if (CurrentDiscipline?.Name == discipline.Name)
            return "current";
            
        return string.Empty;
    }
}
