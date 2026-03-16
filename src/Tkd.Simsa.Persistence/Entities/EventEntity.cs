namespace Tkd.Simsa.Persistence.Entities;

using Tkd.Simsa.Domain.Common;

internal class EventEntity : IHasId<Guid>
{
    public string Description { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ParticipationData { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }
    
    /// <summary>
    /// Navigation property to examination disciplines (for backwards compatibility).
    /// </summary>
    public ICollection<DisciplineEntity> Disciplines { get; set; } = new List<DisciplineEntity>();
    
    /// <summary>
    /// Navigation property to examination results (for backwards compatibility).
    /// </summary>
    public ICollection<ExaminationResultEntity> ExaminationResults { get; set; } = new List<ExaminationResultEntity>();
}