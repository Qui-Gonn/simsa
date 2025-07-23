using Microsoft.AspNetCore.Components;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.Ui.Features.Exam.Components;

/// <summary>
/// Component code-behind for DisciplineRatingComponent.
/// </summary>
public partial class DisciplineRatingComponent : ComponentBase
{
    private bool isNumericMode = true;
    private int numericValue = 6;
    private bool? passFailValue;
    
    /// <summary>
    /// Gets or sets the discipline being rated.
    /// </summary>
    [Parameter]
    public DisciplineDto? Discipline { get; set; }
    
    /// <summary>
    /// Gets or sets the current rating.
    /// </summary>
    [Parameter]
    public DisciplineRatingDto? Rating { get; set; }
    
    /// <summary>
    /// Event callback when rating changes.
    /// </summary>
    [Parameter]
    public EventCallback<DisciplineRatingDto> OnRatingChanged { get; set; }
    
    /// <summary>
    /// Gets whether numeric mode is active.
    /// </summary>
    private bool IsNumericMode => isNumericMode;
    
    protected override void OnParametersSet()
    {
        if (Rating != null)
        {
            numericValue = Rating.NumericValue;
            passFailValue = Rating.Passed;
            isNumericMode = Rating.NumericValue > 0 || Rating.NumericValue == 0; // Default to numeric if we have a value
        }
    }
    
    /// <summary>
    /// Sets the rating mode.
    /// </summary>
    /// <param name="numeric">True for numeric mode, false for pass/fail</param>
    private void SetRatingMode(bool numeric)
    {
        isNumericMode = numeric;
        
        if (numeric && passFailValue.HasValue)
        {
            // Convert pass/fail to numeric
            numericValue = passFailValue.Value ? 10 : 0;
            passFailValue = null;
        }
        else if (!numeric)
        {
            // Convert numeric to pass/fail
            passFailValue = numericValue >= 6;
        }
        
        UpdateRating();
    }
    
    /// <summary>
    /// Sets a specific numeric value.
    /// </summary>
    /// <param name="value">The numeric value to set</param>
    private async Task SetNumericValue(int value)
    {
        numericValue = value;
        await OnNumericValueChanged();
    }
    
    /// <summary>
    /// Handles numeric value changes.
    /// </summary>
    private async Task OnNumericValueChanged()
    {
        UpdateRating();
        await InvokeAsync(StateHasChanged);
    }
    
    /// <summary>
    /// Sets pass/fail value.
    /// </summary>
    /// <param name="passed">True for pass, false for fail</param>
    private void SetPassFail(bool passed)
    {
        passFailValue = passed;
        numericValue = passed ? 10 : 0;
        UpdateRating();
    }
    
    /// <summary>
    /// Updates the rating and notifies parent component.
    /// </summary>
    private void UpdateRating()
    {
        var newRating = isNumericMode 
            ? new DisciplineRatingDto(numericValue, numericValue >= 6, GetRatingDescription(numericValue))
            : new DisciplineRatingDto(passFailValue == true ? 10 : 0, passFailValue == true, passFailValue == true ? "Pass" : "Fail");
            
        OnRatingChanged.InvokeAsync(newRating);
    }
    
    /// <summary>
    /// Gets the description for a numeric rating value.
    /// </summary>
    /// <param name="value">The numeric value</param>
    /// <returns>Description string</returns>
    private static string GetRatingDescription(int value) => value switch
    {
        0 => "No Show",
        1 => "Poor",
        2 => "Very Poor",
        3 => "Below Average",
        4 => "Below Average",
        5 => "Below Pass",
        6 => "Pass",
        7 => "Good",
        8 => "Very Good",
        9 => "Excellent",
        10 => "Outstanding",
        _ => value.ToString()
    };
}
