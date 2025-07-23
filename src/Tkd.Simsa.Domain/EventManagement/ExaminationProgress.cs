namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Value object to track current discipline and completion status in an examination.
/// </summary>
public record ExaminationProgress(
    IReadOnlyList<Discipline> Disciplines,
    Discipline? CurrentDiscipline,
    IReadOnlySet<Discipline> CompletedDisciplines)
{
    /// <summary>
    /// Gets the total number of disciplines in the examination.
    /// </summary>
    public int TotalDisciplines => Disciplines.Count;
    
    /// <summary>
    /// Gets the number of completed disciplines.
    /// </summary>
    public int CompletedCount => CompletedDisciplines.Count;
    
    /// <summary>
    /// Gets the completion percentage (0-100).
    /// </summary>
    public decimal CompletionPercentage => TotalDisciplines == 0 ? 0 : (decimal)CompletedCount / TotalDisciplines * 100;
    
    /// <summary>
    /// Gets whether the examination is completed.
    /// </summary>
    public bool IsCompleted => CompletedCount == TotalDisciplines;
    
    /// <summary>
    /// Creates initial examination progress with the first discipline as current.
    /// </summary>
    /// <param name="disciplines">The disciplines for the examination</param>
    /// <returns>Initial progress state</returns>
    public static ExaminationProgress CreateInitial(IReadOnlyList<Discipline> disciplines)
    {
        ArgumentNullException.ThrowIfNull(disciplines);
        
        return new ExaminationProgress(
            disciplines,
            disciplines.FirstOrDefault(),
            new HashSet<Discipline>());
    }
    
    /// <summary>
    /// Moves to the specified discipline.
    /// </summary>
    /// <param name="discipline">The discipline to set as current</param>
    /// <returns>Updated progress with new current discipline</returns>
    /// <exception cref="ArgumentException">Thrown when discipline is not in the examination</exception>
    public ExaminationProgress SetCurrentDiscipline(Discipline discipline)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        
        if (!Disciplines.Contains(discipline))
            throw new ArgumentException($"Discipline '{discipline.Name}' is not part of this examination");
            
        return this with { CurrentDiscipline = discipline };
    }
    
    /// <summary>
    /// Marks a discipline as completed.
    /// </summary>
    /// <param name="discipline">The discipline to mark as completed</param>
    /// <returns>Updated progress with the discipline marked as completed</returns>
    /// <exception cref="ArgumentException">Thrown when discipline is not in the examination</exception>
    public ExaminationProgress CompleteDiscipline(Discipline discipline)
    {
        ArgumentNullException.ThrowIfNull(discipline);
        
        if (!Disciplines.Contains(discipline))
            throw new ArgumentException($"Discipline '{discipline.Name}' is not part of this examination");
            
        var newCompleted = new HashSet<Discipline>(CompletedDisciplines) { discipline };
        
        // Move to next discipline if current was completed
        var nextDiscipline = discipline == CurrentDiscipline ? GetNextDiscipline(discipline) : CurrentDiscipline;
        
        return new ExaminationProgress(Disciplines, nextDiscipline, newCompleted);
    }
    
    /// <summary>
    /// Gets the next discipline in sequence after the specified discipline.
    /// </summary>
    /// <param name="discipline">The current discipline</param>
    /// <returns>Next discipline or null if this was the last one</returns>
    private Discipline? GetNextDiscipline(Discipline discipline)
    {
        var currentIndex = Disciplines.ToList().FindIndex(d => d == discipline);
        return currentIndex >= 0 && currentIndex < Disciplines.Count - 1 
            ? Disciplines[currentIndex + 1] 
            : null;
    }
}
