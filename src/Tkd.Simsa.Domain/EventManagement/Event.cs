namespace Tkd.Simsa.Domain.EventManagement;

using Tkd.Simsa.Domain.Common;

public record Event : IModelWithId<Guid>
{
    public static readonly Event Empty = new ()
    {
        Id = Guid.Empty,
        Description = string.Empty,
        Name = string.Empty,
        ParticipationData = ParticipationData.NoParticipationData,
        StartDate = DateOnly.MinValue,
        ExaminationDisciplines = [],
        ExaminationProgress = null
    };

    public string Description { get; init; } = string.Empty;

    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Name { get; init; } = string.Empty;

    public ParticipationData ParticipationData { get; init; } = ParticipationData.NoParticipationData;

    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    /// <summary>
    /// Gets the examination disciplines if this is an examination event.
    /// </summary>
    public IReadOnlyList<Discipline> ExaminationDisciplines { get; init; } = [];
    
    /// <summary>
    /// Gets the examination progress tracking if this is an examination event.
    /// </summary>
    public ExaminationProgress? ExaminationProgress { get; init; }
    
    /// <summary>
    /// Gets whether this event is an examination.
    /// </summary>
    public bool IsExamination => ExaminationDisciplines.Count > 0;
    
    /// <summary>
    /// Factory method for creating examination events.
    /// </summary>
    /// <param name="name">The examination name</param>
    /// <param name="description">The examination description</param>
    /// <param name="startDate">The examination start date</param>
    /// <param name="disciplines">The disciplines for this examination</param>
    /// <param name="participationData">The participation data</param>
    /// <returns>A new examination event</returns>
    public static Event CreateExamination(
        string name,
        string description,
        DateOnly startDate,
        IReadOnlyList<Discipline> disciplines,
        ParticipationData participationData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(disciplines);
        ArgumentNullException.ThrowIfNull(participationData);
        
        if (disciplines.Count == 0)
            throw new ArgumentException("Examination must have at least one discipline", nameof(disciplines));
            
        var progress = ExaminationProgress.CreateInitial(disciplines);
        
        return new Event
        {
            Name = name.Trim(),
            Description = description.Trim(),
            StartDate = startDate,
            ExaminationDisciplines = disciplines,
            ExaminationProgress = progress,
            ParticipationData = participationData
        };
    }
    
    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    /// <param name="newProgress">The new progress state</param>
    /// <returns>Updated event with new progress</returns>
    /// <exception cref="InvalidOperationException">Thrown when event is not an examination</exception>
    public Event UpdateExaminationProgress(ExaminationProgress newProgress)
    {
        if (!IsExamination)
            throw new InvalidOperationException("Cannot update examination progress on non-examination event");
            
        ArgumentNullException.ThrowIfNull(newProgress);
        
        return this with { ExaminationProgress = newProgress };
    }
}