using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting examination progress.
/// </summary>
internal class GetExaminationProgressHandler : IRequestHandler<GetExaminationProgressQuery, ExaminationProgressDto?>
{
    private readonly IEventRepository _eventRepository;
    
    public GetExaminationProgressHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<ExaminationProgressDto?> Handle(GetExaminationProgressQuery request, CancellationToken cancellationToken)
    {
        var progress = await _eventRepository.GetExaminationProgressAsync(request.ExaminationId, cancellationToken);
        
        if (progress == null)
            return null;
            
        var disciplineDtos = progress.Disciplines
            .Select(d => new DisciplineDto(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        return progress.ToProgressDto(disciplineDtos);
    }
}
