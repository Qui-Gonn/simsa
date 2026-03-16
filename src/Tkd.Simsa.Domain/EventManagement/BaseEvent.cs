namespace Tkd.Simsa.Domain.EventManagement;

using Tkd.Simsa.Domain.Common;

/// <summary>
/// Abstract base class for all event types in the system.
/// Contains shared properties and behavior common to all events.
/// </summary>
public abstract record BaseEvent : IModelWithId<Guid>
{
    /// <summary>
    /// Gets the unique identifier for this event.
    /// </summary>
    public Guid Id { get; init; } = Guid.CreateVersion7();

    /// <summary>
    /// Gets the event name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the event description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the event start date.
    /// </summary>
    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);

    /// <summary>
    /// Gets the participation data for this event.
    /// </summary>
    public ParticipationData ParticipationData { get; init; } = ParticipationData.NoParticipationData;

    /// <summary>
    /// Factory method for creating basic events.
    /// </summary>
    /// <param name="name">The event name</param>
    /// <param name="description">The event description</param>
    /// <param name="startDate">The event start date</param>
    /// <param name="participationData">The participation data</param>
    /// <returns>A new event</returns>
    protected static void ValidateCommonProperties(
        string name,
        string description,
        ParticipationData participationData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(participationData);
    }
}
