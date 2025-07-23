using Microsoft.EntityFrameworkCore;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;

namespace Tkd.Simsa.Persistence.Repositories;

/// <summary>
/// Repository implementation for examination results.
/// </summary>
internal class ExaminationResultRepository : GenericRepository<ExaminationResultEntity, ExaminationResult>, IExaminationResultRepository
{
    public ExaminationResultRepository(SimsaDbContext dbContext, IMapper<ExaminationResultEntity, ExaminationResult> mapper)
        : base(dbContext, mapper)
    {
    }
    
    /// <summary>
    /// Gets examination results for a specific participant.
    /// </summary>
    public async Task<ExaminationResult?> GetByParticipantAsync(Guid examinationId, Guid participantId, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<ExaminationResultEntity>()
            .FirstOrDefaultAsync(er => er.ExaminationId == examinationId && er.ParticipantId == participantId, cancellationToken);
            
        return entity != null ? Mapper.ToModel(entity) : null;
    }
    
    /// <summary>
    /// Gets all examination results for an examination.
    /// </summary>
    public async Task<IEnumerable<ExaminationResult>> GetAllByExaminationAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var entities = await DbContext.Set<ExaminationResultEntity>()
            .Where(er => er.ExaminationId == examinationId)
            .ToListAsync(cancellationToken);
            
        return entities.Select(e => Mapper.ToModel(e));
    }
    
    /// <summary>
    /// Updates a discipline result for a participant.
    /// </summary>
    public async Task<ExaminationResult?> UpdateDisciplineResultAsync(
        Guid examinationId,
        Guid participantId,
        Discipline discipline,
        DisciplineRating rating,
        ExaminationNotes notes,
        string documentedBy,
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<ExaminationResultEntity>()
            .FirstOrDefaultAsync(er => er.ExaminationId == examinationId && er.ParticipantId == participantId, cancellationToken);
            
        ExaminationResult result;
        
        if (entity == null)
        {
            // Create new examination result
            result = ExaminationResult.Create(participantId, examinationId);
            result = result.UpdateDisciplineResult(discipline, rating, notes, documentedBy);
            
            entity = Mapper.ToEntity(result);
            DbContext.Set<ExaminationResultEntity>().Add(entity);
        }
        else
        {
            // Update existing examination result
            result = Mapper.ToModel(entity);
            result = result.UpdateDisciplineResult(discipline, rating, notes, documentedBy);
            
            Mapper.UpdateEntity(entity, result);
        }
        
        await DbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
        
    /// <summary>
    /// Adds notes to a discipline result for a participant.
    /// </summary>
    public async Task<ExaminationResult?> AddDisciplineNotesAsync(
        Guid examinationId,
        Guid participantId,
        Discipline discipline,
        ExaminationNotes notes,
        string documentedBy,
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<ExaminationResultEntity>()
            .FirstOrDefaultAsync(er => er.ExaminationId == examinationId && er.ParticipantId == participantId, cancellationToken);
            
        ExaminationResult result;
        
        if (entity == null)
        {
            // Create new examination result with just notes
            result = ExaminationResult.Create(participantId, examinationId);
            result = result.AddDisciplineNotes(discipline, notes, documentedBy);
            
            entity = Mapper.ToEntity(result);
            DbContext.Set<ExaminationResultEntity>().Add(entity);
        }
        else
        {
            // Update existing examination result
            result = Mapper.ToModel(entity);
            result = result.AddDisciplineNotes(discipline, notes, documentedBy);
            
            Mapper.UpdateEntity(entity, result);
        }
        
        await DbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}
