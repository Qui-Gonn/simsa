namespace Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Database entity representing an examination event.
/// Inherits common event properties from BaseEventEntity.
/// </summary>
internal class ExaminationEntity : BaseEventEntity
{
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
