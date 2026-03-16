namespace Tkd.Simsa.Application.EventManagement;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Repository interface for examination operations.
/// </summary>
public interface IExaminationRepository : IGenericRepository<Examination>
{
    /// <summary>
    /// Gets all participants for an examination.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of participants</returns>
    Task<IEnumerable<Participant>> GetParticipantsAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the current examination progress.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The examination progress or null if not found</returns>
    Task<ExaminationProgress?> GetProgressAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates the examination progress.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="progress">The new progress state</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated examination or null if not found</returns>
    Task<Examination?> UpdateProgressAsync(Guid examinationId, ExaminationProgress progress, CancellationToken cancellationToken = default);
}
