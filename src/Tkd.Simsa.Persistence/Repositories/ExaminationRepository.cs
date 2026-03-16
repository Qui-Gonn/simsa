using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;

namespace Tkd.Simsa.Persistence.Repositories;

/// <summary>
/// Repository implementation for examinations.
/// </summary>
internal class ExaminationRepository : GenericRepository<ExaminationEntity, Examination>, IExaminationRepository
{
    public ExaminationRepository(SimsaDbContext dbContext, IMapper<ExaminationEntity, Examination> mapper)
        : base(dbContext, mapper)
    {
    }
    
    /// <summary>
    /// Gets all participants for an examination.
    /// </summary>
    public async Task<IEnumerable<Participant>> GetParticipantsAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var examinationEntity = await DbContext.Set<ExaminationEntity>()
            .FirstOrDefaultAsync(e => e.Id == examinationId, cancellationToken);
            
        if (examinationEntity?.ParticipationData == null)
            return [];
            
        try
        {
            var participationData = JsonSerializer.Deserialize<ParticipationData>(examinationEntity.ParticipationData);
            return participationData?.Participants ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }
    
    /// <summary>
    /// Gets the current examination progress.
    /// </summary>
    public async Task<ExaminationProgress?> GetProgressAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var examination = await GetByIdAsync(examinationId, cancellationToken);
        return examination?.Progress;
    }
    
    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    public async Task<Examination?> UpdateProgressAsync(Guid examinationId, ExaminationProgress progress, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<ExaminationEntity>()
            .Include(e => e.Disciplines)
            .FirstOrDefaultAsync(e => e.Id == examinationId, cancellationToken);
            
        if (entity == null)
            return null;
            
        var currentExamination = Mapper.ToModel(entity);
        var updatedExamination = currentExamination.UpdateProgress(progress);
        
        Mapper.UpdateEntity(entity, updatedExamination);
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return updatedExamination;
    }
    
    /// <summary>
    /// Gets examination by ID with disciplines loaded.
    /// </summary>
    public new async Task<Examination?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<ExaminationEntity>()
            .Include(e => e.Disciplines)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            
        return entity != null ? Mapper.ToModel(entity) : null;
    }
}
