namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Value object for rating scale in discipline evaluation.
/// </summary>
public record DisciplineRating
{
    /// <summary>
    /// Gets the numeric value of the rating (0-10).
    /// </summary>
    public int NumericValue { get; }
    
    /// <summary>
    /// Gets whether the discipline was passed.
    /// </summary>
    public bool Passed { get; }
    
    /// <summary>
    /// Gets a descriptive text for the rating.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Predefined rating for passing.
    /// </summary>
    public static DisciplineRating Pass => new(true, 10, "Pass");
    
    /// <summary>
    /// Predefined rating for failing.
    /// </summary>
    public static DisciplineRating Fail => new(false, 0, "Fail");
    
    private DisciplineRating(bool passed, int numericValue, string description)
    {
        Passed = passed;
        NumericValue = numericValue;
        Description = description;
    }
    
    /// <summary>
    /// Creates a numeric rating.
    /// </summary>
    /// <param name="value">The numeric value (0-10)</param>
    /// <returns>A rating instance</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when value is outside 0-10 range</exception>
    public static DisciplineRating FromNumeric(int value)
    {
        if (value < 0 || value > 10)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating value must be between 0 and 10");
            
        var passed = value >= 6; // 6 is typically passing grade
        var description = value switch
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
        
        return new DisciplineRating(passed, value, description);
    }
    
    /// <summary>
    /// Creates a simple pass/fail rating.
    /// </summary>
    /// <param name="passed">Whether the discipline was passed</param>
    /// <returns>A pass/fail rating instance</returns>
    public static DisciplineRating FromPassFail(bool passed)
    {
        return passed ? Pass : Fail;
    }
    
    public override string ToString() => $"{Description} ({NumericValue})";
}
