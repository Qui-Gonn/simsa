using MediatR;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting examination progress.
/// </summary>
internal class GetExaminationProgressHandler : IRequestHandler<GetExaminationProgressQuery, ExaminationProgressDto?>
{
    private readonly IExaminationRepository _examinationRepository;
    
    public GetExaminationProgressHandler(IExaminationRepository examinationRepository)
    {
        _examinationRepository = examinationRepository;
    }
    
    public async Task<ExaminationProgressDto?> Handle(GetExaminationProgressQuery request, CancellationToken cancellationToken)
    {
        var progress = await _examinationRepository.GetProgressAsync(request.ExaminationId, cancellationToken);
        
        if (progress == null)
            return null;
            
        var disciplineDtos = progress.Disciplines
            .Select(d => new DisciplineDto(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        return progress.ToProgressDto(disciplineDtos);
    }
}
