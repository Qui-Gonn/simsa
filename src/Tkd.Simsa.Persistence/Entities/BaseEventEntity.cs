namespace Tkd.Simsa.Persistence.Entities;

using Tkd.Simsa.Domain.Common;

/// <summary>
/// Abstract base entity containing common properties for all event types.
/// </summary>
internal abstract class BaseEventEntity : IHasId<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();

    /// <summary>
    /// Gets or sets the event name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event start date.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Gets or sets the serialized participation data.
    /// </summary>
    public string ParticipationData { get; set; } = string.Empty;
}
