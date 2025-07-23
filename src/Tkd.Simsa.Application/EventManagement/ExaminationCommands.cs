using MediatR;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Application.EventManagement;

/// <summary>
/// Command to update a discipline result for a participant.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="ParticipantId">The participant identifier</param>
/// <param name="Discipline">The discipline being evaluated</param>
/// <param name="Rating">The rating for the discipline</param>
/// <param name="Notes">Additional notes for the result</param>
/// <param name="DocumentedBy">Who is documenting this result</param>
public record UpdateDisciplineResultCommand(
    Guid ExaminationId,
    Guid ParticipantId,
    Discipline Discipline,
    DisciplineRating Rating,
    ExaminationNotes Notes,
    string DocumentedBy) : IRequest<ExaminationResultDto?>;

/// <summary>
/// Command to set the current discipline in an examination.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="Discipline">The discipline to set as current</param>
public record SetCurrentDisciplineCommand(
    Guid ExaminationId,
    Discipline Discipline) : IRequest<ExaminationProgressDto?>;

/// <summary>
/// Command to add notes to a discipline result.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="ParticipantId">The participant identifier</param>
/// <param name="Discipline">The discipline to add notes to</param>
/// <param name="Notes">The notes to add</param>
/// <param name="DocumentedBy">Who is adding the notes</param>
public record AddExaminationNotesCommand(
    Guid ExaminationId,
    Guid ParticipantId,
    Discipline Discipline,
    ExaminationNotes Notes,
    string DocumentedBy) : IRequest<ExaminationResultDto?>;

/// <summary>
/// Command to complete an examination for a participant.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="ParticipantId">The participant identifier</param>
/// <param name="DocumentedBy">Who is completing the examination</param>
public record CompleteParticipantExaminationCommand(
    Guid ExaminationId,
    Guid ParticipantId,
    string DocumentedBy) : IRequest<ExaminationResultDto?>;

/// <summary>
/// Command to complete a discipline across all participants.
/// </summary>
/// <param name="ExaminationId">The examination identifier</param>
/// <param name="Discipline">The discipline to complete</param>
/// <param name="DocumentedBy">Who is completing the discipline</param>
public record CompleteDisciplineCommand(
    Guid ExaminationId,
    Discipline Discipline,
    string DocumentedBy) : IRequest<ExaminationProgressDto?>;
