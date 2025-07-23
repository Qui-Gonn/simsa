using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Application.EventManagement;

/// <summary>
/// Data transfer object for examination information.
/// </summary>
public record ExaminationDto(
    Guid Id,
    string Name,
    string Description,
    DateOnly StartDate,
    IReadOnlyList<DisciplineDto> Disciplines,
    ExaminationProgressDto Progress,
    IReadOnlyList<ParticipantDto> Participants);

/// <summary>
/// Data transfer object for discipline information.
/// </summary>
public record DisciplineDto(
    DisciplineType Type,
    string Name,
    string Description,
    int Order);

/// <summary>
/// Data transfer object for examination progress.
/// </summary>
public record ExaminationProgressDto(
    DisciplineDto? CurrentDiscipline,
    IReadOnlyList<DisciplineDto> CompletedDisciplines,
    int TotalDisciplines,
    decimal CompletionPercentage);

/// <summary>
/// Data transfer object for participant information.
/// </summary>
public record ParticipantDto(
    Guid Id,
    string Name,
    string BeltRank,
    int Age,
    DateTime DateOfBirth);

/// <summary>
/// Data transfer object for examination results.
/// </summary>
public record ExaminationResultDto(
    Guid Id,
    Guid ParticipantId,
    Guid ExaminationId,
    IReadOnlyDictionary<DisciplineDto, DisciplineResultDto> Results,
    DateTime LastUpdated);

/// <summary>
/// Data transfer object for discipline result.
/// </summary>
public record DisciplineResultDto(
    DisciplineRatingDto Rating,
    string Notes,
    DateTime DocumentedAt,
    string DocumentedBy);

/// <summary>
/// Data transfer object for discipline rating.
/// </summary>
public record DisciplineRatingDto(
    int NumericValue,
    bool Passed,
    string Description);
