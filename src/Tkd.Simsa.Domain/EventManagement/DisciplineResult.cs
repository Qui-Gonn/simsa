namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Individual discipline result within an examination.
/// </summary>
public record DisciplineResult(
    DisciplineRating Rating,
    ExaminationNotes Notes,
    DateTime DocumentedAt,
    string DocumentedBy)
{
    /// <summary>
    /// Creates a new discipline result.
    /// </summary>
    /// <param name="rating">The rating for this discipline</param>
    /// <param name="notes">Additional notes</param>
    /// <param name="documentedBy">Who documented this result</param>
    /// <returns>A new discipline result</returns>
    public static DisciplineResult Create(DisciplineRating rating, ExaminationNotes notes, string documentedBy)
    {
        ArgumentNullException.ThrowIfNull(rating);
        ArgumentNullException.ThrowIfNull(notes);
        
        if (string.IsNullOrWhiteSpace(documentedBy))
            throw new ArgumentException("DocumentedBy cannot be empty", nameof(documentedBy));
            
        return new DisciplineResult(rating, notes, DateTime.UtcNow, documentedBy.Trim());
    }
    
    /// <summary>
    /// Updates the rating for this result.
    /// </summary>
    /// <param name="newRating">The new rating</param>
    /// <param name="documentedBy">Who is updating the rating</param>
    /// <returns>Updated discipline result</returns>
    public DisciplineResult UpdateRating(DisciplineRating newRating, string documentedBy)
    {
        ArgumentNullException.ThrowIfNull(newRating);
        
        if (string.IsNullOrWhiteSpace(documentedBy))
            throw new ArgumentException("DocumentedBy cannot be empty", nameof(documentedBy));
            
        return this with 
        { 
            Rating = newRating, 
            DocumentedAt = DateTime.UtcNow,
            DocumentedBy = documentedBy.Trim()
        };
    }
    
    /// <summary>
    /// Adds or updates notes for this result.
    /// </summary>
    /// <param name="additionalNotes">Notes to add</param>
    /// <param name="documentedBy">Who is adding the notes</param>
    /// <returns>Updated discipline result</returns>
    public DisciplineResult AddNotes(ExaminationNotes additionalNotes, string documentedBy)
    {
        ArgumentNullException.ThrowIfNull(additionalNotes);
        
        if (string.IsNullOrWhiteSpace(documentedBy))
            throw new ArgumentException("DocumentedBy cannot be empty", nameof(documentedBy));
            
        var updatedNotes = Notes.Append(additionalNotes.Content);
        
        return this with 
        { 
            Notes = updatedNotes,
            DocumentedAt = DateTime.UtcNow,
            DocumentedBy = documentedBy.Trim()
        };
    }
}
