using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Database entity representing examination disciplines.
/// </summary>
internal class DisciplineEntity : IHasId<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();
    
    /// <summary>
    /// Gets or sets the discipline type.
    /// </summary>
    public DisciplineType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the discipline name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the discipline description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the order in examination sequence.
    /// </summary>
    public int Order { get; set; }
    
    /// <summary>
    /// Gets or sets the examination identifier.
    /// </summary>
    public Guid ExaminationId { get; set; }
    
    /// <summary>
    /// Navigation property to the examination.
    /// </summary>
    public ExaminationEntity Examination { get; set; } = null!;
}
