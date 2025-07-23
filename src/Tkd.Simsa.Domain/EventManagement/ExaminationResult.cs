using Tkd.Simsa.Domain.Common;

namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Aggregate root for examination results of a participant.
/// </summary>
public record ExaminationResult : IModelWithId<Guid>
{
    /// <summary>
    /// Gets the unique identifier for this examination result.
    /// </summary>
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    /// <summary>
    /// Gets the participant identifier.
    /// </summary>
    public required Guid ParticipantId { get; init; }
    
    /// <summary>
    /// Gets the examination identifier.
    /// </summary>
    public required Guid ExaminationId { get; init; }
    
    /// <summary>
    /// Gets the results for each discipline.
    /// </summary>
    public required IReadOnlyDictionary<Discipline, DisciplineResult> Results { get; init; }
    
    /// <summary>
    /// Gets when this result was last updated.
    /// </summary>
    public DateTime LastUpdated { get; init; } = DateTime.UtcNow;
    
    /// <summary>
    /// Creates a new examination result for a participant.
    /// </summary>
    /// <param name="participantId">The participant identifier</param>
    /// <param name="examinationId">The examination identifier</param>
    /// <returns>A new examination result with empty results</returns>
    public static ExaminationResult Create(Guid participantId, Guid examinationId)
    {
        if (participantId == Guid.Empty)
            throw new ArgumentException("ParticipantId cannot be empty", nameof(participantId));
            
        if (examinationId == Guid.Empty)
            throw new ArgumentException("ExaminationId cannot be empty", nameof(examinationId));
            
        return new ExaminationResult
        {
            ParticipantId = participantId,
            ExaminationId = examinationId,
            Results = new Dictionary<Discipline, DisciplineResult>()
        };
    }
    
    /// <summary>
    /// Updates the result for a specific discipline.
    /// </summary>
    /// <param name="discipline">The discipline to update</param>
    /// <param name="rating">The rating for the discipline</param>
    /// <param name="notes">Additional notes</param>
    /// <param name="documentedBy">Who is documenting this result</param>
    /// <returns>Updated examination result</returns>
    public ExaminationResult UpdateDisciplineResult(
        Discipline discipline, 
        DisciplineRating rating, 
        ExaminationNotes notes, 
        string documentedBy)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        ArgumentNullException.ThrowIfNull(rating);
        ArgumentNullException.ThrowIfNull(notes);
        
        var disciplineResult = DisciplineResult.Create(rating, notes, documentedBy);
        var updatedResults = new Dictionary<Discipline, DisciplineResult>(Results)
        {
            [discipline] = disciplineResult
        };
        
        return this with 
        { 
            Results = updatedResults,
            LastUpdated = DateTime.UtcNow
        };
    }
    
    /// <summary>
    /// Adds notes to an existing discipline result.
    /// </summary>
    /// <param name="discipline">The discipline to add notes to</param>
    /// <param name="additionalNotes">The notes to add</param>
    /// <param name="documentedBy">Who is adding the notes</param>
    /// <returns>Updated examination result</returns>
    public ExaminationResult AddDisciplineNotes(
        Discipline discipline, 
        ExaminationNotes additionalNotes, 
        string documentedBy)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        ArgumentNullException.ThrowIfNull(additionalNotes);
        
        if (!Results.TryGetValue(discipline, out var existingResult))
        {
            // Create new result with just notes
            var emptyRating = DisciplineRating.FromNumeric(0);
            return UpdateDisciplineResult(discipline, emptyRating, additionalNotes, documentedBy);
        }
        
        var updatedResult = existingResult.AddNotes(additionalNotes, documentedBy);
        var updatedResults = new Dictionary<Discipline, DisciplineResult>(Results)
        {
            [discipline] = updatedResult
        };
        
        return this with 
        { 
            Results = updatedResults,
            LastUpdated = DateTime.UtcNow
        };
    }
    
    /// <summary>
    /// Gets the result for a specific discipline.
    /// </summary>
    /// <param name="discipline">The discipline to get results for</param>
    /// <returns>The discipline result or null if not found</returns>
    public DisciplineResult? GetDisciplineResult(Discipline discipline)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        return Results.TryGetValue(discipline, out var result) ? result : null;
    }
    
    /// <summary>
    /// Gets whether all disciplines have been completed.
    /// </summary>
    /// <param name="allDisciplines">All disciplines in the examination</param>
    /// <returns>True if all disciplines have results</returns>
    public bool IsComplete(IReadOnlyList<Discipline> allDisciplines)
    {
        ArgumentNullException.ThrowIfNull(allDisciplines);
        return allDisciplines.All(d => Results.ContainsKey(d));
    }
    
    /// <summary>
    /// Gets the overall pass status for the examination.
    /// </summary>
    /// <param name="allDisciplines">All disciplines in the examination</param>
    /// <returns>True if all disciplines are passed</returns>
    public bool HasPassed(IReadOnlyList<Discipline> allDisciplines)
    {
        ArgumentNullException.ThrowIfNull(allDisciplines);
        
        if (!IsComplete(allDisciplines))
            return false;
            
        return allDisciplines.All(d => Results[d].Rating.Passed);
    }
}
