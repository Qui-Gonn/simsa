using MediatR;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting examination results for a specific participant.
/// </summary>
internal class GetExaminationResultsHandler : IRequestHandler<GetExaminationResultsQuery, ExaminationResultDto?>
{
    private readonly IExaminationResultRepository _resultRepository;
    
    public GetExaminationResultsHandler(IExaminationResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }
    
    public async Task<ExaminationResultDto?> Handle(GetExaminationResultsQuery request, CancellationToken cancellationToken)
    {
        var result = await _resultRepository.GetByParticipantAsync(request.ExaminationId, request.ParticipantId, cancellationToken);
        
        return result?.ToDto();
    }
}

/// <summary>
/// Extension methods for mapping ExaminationResult to DTOs.
/// </summary>
internal static class ExaminationResultMappingExtensions
{
    public static ExaminationResultDto ToDto(this ExaminationResult result)
    {
        var resultDtos = result.Results.ToDictionary(
            kvp => new DisciplineDto(kvp.Key.Type, kvp.Key.Name, kvp.Key.Description, kvp.Key.Order),
            kvp => new DisciplineResultDto(
                new DisciplineRatingDto(kvp.Value.Rating.NumericValue, kvp.Value.Rating.Passed, kvp.Value.Rating.Description),
                kvp.Value.Notes.Content,
                kvp.Value.DocumentedAt,
                kvp.Value.DocumentedBy));
                
        return new ExaminationResultDto(
            result.Id,
            result.ParticipantId,
            result.ExaminationId,
            resultDtos,
            result.LastUpdated);
    }
}
