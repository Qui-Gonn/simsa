namespace Tkd.Simsa.Application.EventManagement;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Domain.EventManagement;

public interface IEventRepository : IGenericRepository<Event>
{
    /// <summary>
    /// Gets an examination by its identifier with disciplines loaded.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The examination or null if not found</returns>
    Task<Event?> GetExaminationByIdAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all participants for an examination.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of participants</returns>
    Task<IEnumerable<Participant>> GetExaminationParticipantsAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the current examination progress.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The examination progress or null if not found</returns>
    Task<ExaminationProgress?> GetExaminationProgressAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="progress">The new progress state</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated event or null if not found</returns>
    Task<Event?> UpdateExaminationProgressAsync(Guid examinationId, ExaminationProgress progress, CancellationToken cancellationToken = default);
}