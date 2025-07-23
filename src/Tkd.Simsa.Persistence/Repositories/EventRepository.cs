namespace Tkd.Simsa.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;

internal class EventRepository : GenericRepository<EventEntity, Event>, IEventRepository
{
    public EventRepository(SimsaDbContext dbContext, IMapper<EventEntity, Event> mapper)
        : base(dbContext, mapper)
    {
    }
    
    /// <summary>
    /// Gets an examination by its identifier with disciplines loaded.
    /// </summary>
    public async Task<Event?> GetExaminationByIdAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<EventEntity>()
            .Include(e => e.Disciplines)
            .FirstOrDefaultAsync(e => e.Id == examinationId, cancellationToken);
            
        return entity != null ? Mapper.ToModel(entity) : null;
    }
    
    /// <summary>
    /// Gets all participants for an examination.
    /// </summary>
    public async Task<IEnumerable<Participant>> GetExaminationParticipantsAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var eventEntity = await DbContext.Set<EventEntity>()
            .FirstOrDefaultAsync(e => e.Id == examinationId, cancellationToken);
            
        if (eventEntity?.ParticipationData == null)
            return [];
            
        try
        {
            var participationData = JsonSerializer.Deserialize<ParticipationData>(eventEntity.ParticipationData);
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
    public async Task<ExaminationProgress?> GetExaminationProgressAsync(Guid examinationId, CancellationToken cancellationToken = default)
    {
        var examination = await GetExaminationByIdAsync(examinationId, cancellationToken);
        return examination?.ExaminationProgress;
    }
    
    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    public async Task<Event?> UpdateExaminationProgressAsync(Guid examinationId, ExaminationProgress progress, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<EventEntity>()
            .Include(e => e.Disciplines)
            .FirstOrDefaultAsync(e => e.Id == examinationId, cancellationToken);
            
        if (entity == null)
            return null;
            
        var currentEvent = Mapper.ToModel(entity);
        var updatedEvent = currentEvent.UpdateExaminationProgress(progress);
        
        Mapper.UpdateEntity(entity, updatedEvent);
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return updatedEvent;
    }
}