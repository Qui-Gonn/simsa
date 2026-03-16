namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Aggregate root representing an examination event with specific examination behavior.
/// Inherits from BaseEvent to share common event properties and behavior.
/// </summary>
public record Examination : BaseEvent
{
    public static readonly Examination Empty = new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Description = string.Empty,
        StartDate = DateOnly.MinValue,
        ParticipationData = ParticipationData.NoParticipationData,
        Disciplines = [],
        Progress = null
    };

    /// <summary>
    /// Gets the examination disciplines.
    /// </summary>
    public IReadOnlyList<Discipline> Disciplines { get; init; } = [];

    /// <summary>
    /// Gets the examination progress tracking.
    /// </summary>
    public ExaminationProgress? Progress { get; init; }

    /// <summary>
    /// Factory method for creating examination events.
    /// </summary>
    /// <param name="name">The examination name</param>
    /// <param name="description">The examination description</param>
    /// <param name="startDate">The examination start date</param>
    /// <param name="disciplines">The disciplines for this examination</param>
    /// <param name="participationData">The participation data</param>
    /// <returns>A new examination</returns>
    public static Examination Create(
        string name,
        string description,
        DateOnly startDate,
        IReadOnlyList<Discipline> disciplines,
        ParticipationData participationData)
    {
        ValidateCommonProperties(name, description, participationData);
        ArgumentNullException.ThrowIfNull(disciplines);

        if (disciplines.Count == 0)
            throw new ArgumentException("Examination must have at least one discipline", nameof(disciplines));

        var progress = ExaminationProgress.CreateInitial(disciplines);

        return new Examination
        {
            Name = name.Trim(),
            Description = description.Trim(),
            StartDate = startDate,
            Disciplines = disciplines,
            Progress = progress,
            ParticipationData = participationData
        };
    }

    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    /// <param name="newProgress">The new progress state</param>
    /// <returns>Updated examination with new progress</returns>
    public Examination UpdateProgress(ExaminationProgress newProgress)
    {
        ArgumentNullException.ThrowIfNull(newProgress);
        return this with { Progress = newProgress };
    }
}
