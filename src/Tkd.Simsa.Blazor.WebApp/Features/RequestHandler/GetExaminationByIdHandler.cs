using MediatR;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting an examination by its ID.
/// </summary>
internal class GetExaminationByIdHandler : IRequestHandler<GetExaminationByIdQuery, ExaminationDto?>
{
    private readonly IExaminationRepository _examinationRepository;
    
    public GetExaminationByIdHandler(IExaminationRepository examinationRepository)
    {
        _examinationRepository = examinationRepository;
    }
    
    public async Task<ExaminationDto?> Handle(GetExaminationByIdQuery request, CancellationToken cancellationToken)
    {
        var examination = await _examinationRepository.GetByIdAsync(request.ExaminationId, cancellationToken);
        return examination?.ToExaminationDto();
    }
}

/// <summary>
/// Extension methods for mapping Examination to ExaminationDto.
/// </summary>
internal static class ExaminationMappingExtensions
{
    public static ExaminationDto ToExaminationDto(this Examination examination)
    {
        var disciplineDtos = examination.Disciplines
            .Select(d => new DisciplineDto(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        var progressDto = examination.Progress?.ToProgressDto(disciplineDtos) 
                         ?? new ExaminationProgressDto(null, [], 0, 0);
                         
        var participantDtos = examination.ParticipationData.Participants
            .Select(p => new ParticipantDto(
                p.Id,
                p.PersonInfo.Name.ToString(),
                "Unknown", // Belt rank not available in current PersonInfo
                p.PersonInfo.DateOfBirth.Age,
                p.PersonInfo.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue)))
            .ToList();
            
        return new ExaminationDto(
            examination.Id,
            examination.Name,
            examination.Description,
            examination.StartDate,
            disciplineDtos,
            progressDto,
            participantDtos);
    }
    
    public static ExaminationProgressDto ToProgressDto(this ExaminationProgress progress, IReadOnlyList<DisciplineDto> disciplineDtos)
    {
        var currentDisciplineDto = progress.CurrentDiscipline != null 
            ? disciplineDtos.FirstOrDefault(d => d.Name == progress.CurrentDiscipline.Name)
            : null;
            
        var completedDisciplineDtos = disciplineDtos
            .Where(d => progress.CompletedDisciplines.Any(cd => cd.Name == d.Name))
            .ToList();
            
        return new ExaminationProgressDto(
            currentDisciplineDto,
            completedDisciplineDtos,
            progress.TotalDisciplines,
            progress.CompletionPercentage);
    }
}
