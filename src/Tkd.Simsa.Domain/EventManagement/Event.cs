namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Represents a general event in the system.
/// For specialized event types, inherit from BaseEvent or use specific event classes like Examination.
/// </summary>
public record Event : BaseEvent
{
    public static readonly Event Empty = new()
    {
        Id = Guid.Empty,
        Description = string.Empty,
        Name = string.Empty,
        ParticipationData = ParticipationData.NoParticipationData,
        StartDate = DateOnly.MinValue
    };

    /// <summary>
    /// Factory method for creating general events.
    /// </summary>
    /// <param name="name">The event name</param>
    /// <param name="description">The event description</param>
    /// <param name="startDate">The event start date</param>
    /// <param name="participationData">The participation data</param>
    /// <returns>A new event</returns>
    public static Event Create(
        string name,
        string description,
        DateOnly startDate,
        ParticipationData participationData)
    {
        ValidateCommonProperties(name, description, participationData);

        return new Event
        {
            Name = name.Trim(),
            Description = description.Trim(),
            StartDate = startDate,
            ParticipationData = participationData
        };
    }
}