using MediatR;

namespace Tkd.Simsa.Application.EventManagement;

/// <summary>
/// Query to get an examination by its ID.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
public record GetExaminationByIdQuery(Guid ExaminationId) : IRequest<ExaminationDto?>;

/// <summary>
/// Query to get all participants of an examination.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
public record GetExaminationParticipantsQuery(Guid ExaminationId) : IRequest<IEnumerable<ParticipantDto>>;

/// <summary>
/// Query to get the current progress of an examination.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
public record GetExaminationProgressQuery(Guid ExaminationId) : IRequest<ExaminationProgressDto?>;

/// <summary>
/// Query to get examination results for a specific participant.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="ParticipantId">The participant identifier</param>
public record GetExaminationResultsQuery(Guid ExaminationId, Guid ParticipantId) : IRequest<ExaminationResultDto?>;

/// <summary>
/// Query to get all examination results for an examination.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
public record GetAllExaminationResultsQuery(Guid ExaminationId) : IRequest<IEnumerable<ExaminationResultDto>>;
