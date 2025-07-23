using Tkd.Simsa.Domain.Common;

namespace Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Database entity representing examination results for participants.
/// </summary>
internal class ExaminationResultEntity : IHasId<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    /// <summary>
    /// Gets or sets the participant identifier.
    /// </summary>
    public Guid ParticipantId { get; set; }
    
    /// <summary>
    /// Gets or sets the examination identifier.
    /// </summary>
    public Guid ExaminationId { get; set; }
    
    /// <summary>
    /// Gets or sets the JSON serialized discipline results.
    /// </summary>
    public string ResultsJson { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets when this result was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Navigation property to the examination event.
    /// </summary>
    public EventEntity Examination { get; set; } = null!;
}
