using Tkd.Simsa.Domain.Common;

namespace Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Database entity representing an examination event.
/// </summary>
internal class ExaminationEntity : IHasId<Guid>
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; } = Guid.CreateVersion7();

    /// <summary>
    /// Gets or sets the examination name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the examination description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the examination start date.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Gets or sets the serialized participation data.
    /// </summary>
    public string ParticipationData { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the JSON serialized examination progress.
    /// </summary>
    public string? ProgressJson { get; set; }

    /// <summary>
    /// Navigation property to examination disciplines.
    /// </summary>
    public ICollection<DisciplineEntity> Disciplines { get; set; } = new List<DisciplineEntity>();

    /// <summary>
    /// Navigation property to examination results.
    /// </summary>
    public ICollection<ExaminationResultEntity> ExaminationResults { get; set; } = new List<ExaminationResultEntity>();
}
