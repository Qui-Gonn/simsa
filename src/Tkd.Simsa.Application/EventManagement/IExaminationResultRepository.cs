using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Application.EventManagement;

/// <summary>
/// Repository interface for examination results.
/// </summary>
public interface IExaminationResultRepository : IGenericRepository<ExaminationResult>
{
    /// <summary>
    /// Gets examination results for a specific participant.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="participantId">The participant identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The examination result or null if not found</returns>
    Task<ExaminationResult?> GetByParticipantAsync(Guid examinationId, Guid participantId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all examination results for an examination.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of examination results</returns>
    Task<IEnumerable<ExaminationResult>> GetAllByExaminationAsync(Guid examinationId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a discipline result for a participant.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="participantId">The participant identifier</param>
    /// <param name="discipline">The discipline</param>
    /// <param name="rating">The rating</param>
    /// <param name="notes">The notes</param>
    /// <param name="documentedBy">Who is documenting the result</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated examination result</returns>
    Task<ExaminationResult?> UpdateDisciplineResultAsync(
        Guid examinationId,
        Guid participantId,
        Discipline discipline,
        DisciplineRating rating,
        ExaminationNotes notes,
        string documentedBy,
        CancellationToken cancellationToken = default);
        
    /// <summary>
    /// Adds notes to a discipline result for a participant.
    /// </summary>
    /// <param name="examinationId">The examination identifier</param>
    /// <param name="participantId">The participant identifier</param>
    /// <param name="discipline">The discipline</param>
    /// <param name="notes">The notes to add</param>
    /// <param name="documentedBy">Who is adding the notes</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated examination result</returns>
    Task<ExaminationResult?> AddDisciplineNotesAsync(
        Guid examinationId,
        Guid participantId,
        Discipline discipline,
        ExaminationNotes notes,
        string documentedBy,
        CancellationToken cancellationToken = default);
}
