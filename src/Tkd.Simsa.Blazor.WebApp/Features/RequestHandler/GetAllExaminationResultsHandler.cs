using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting all examination results for an examination.
/// </summary>
internal class GetAllExaminationResultsHandler : IRequestHandler<GetAllExaminationResultsQuery, IEnumerable<ExaminationResultDto>>
{
    private readonly IExaminationResultRepository _resultRepository;
    
    public GetAllExaminationResultsHandler(IExaminationResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }
    
    public async Task<IEnumerable<ExaminationResultDto>> Handle(GetAllExaminationResultsQuery request, CancellationToken cancellationToken)
    {
        var results = await _resultRepository.GetAllByExaminationAsync(request.ExaminationId, cancellationToken);
        
        return results.Select(r => r.ToDto());
    }
}
