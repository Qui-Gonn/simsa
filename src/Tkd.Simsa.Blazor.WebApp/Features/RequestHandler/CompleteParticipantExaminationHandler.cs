using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for completing an examination for a participant.
/// </summary>
internal class CompleteParticipantExaminationHandler : IRequestHandler<CompleteParticipantExaminationCommand, ExaminationResultDto?>
{
    private readonly IExaminationResultRepository _resultRepository;
    private readonly IEventRepository _eventRepository;
    
    public CompleteParticipantExaminationHandler(
        IExaminationResultRepository resultRepository,
        IEventRepository eventRepository)
    {
        _resultRepository = resultRepository;
        _eventRepository = eventRepository;
    }
    
    public async Task<ExaminationResultDto?> Handle(CompleteParticipantExaminationCommand request, CancellationToken cancellationToken)
    {
        // Get examination and current result
        var examination = await _eventRepository.GetExaminationByIdAsync(request.ExaminationId, cancellationToken);
        if (examination == null)
            return null;
            
        var result = await _resultRepository.GetByParticipantAsync(request.ExaminationId, request.ParticipantId, cancellationToken);
        if (result == null)
            return null;
            
        // Verify all disciplines have been completed
        if (!result.IsComplete(examination.ExaminationDisciplines))
        {
            throw new InvalidOperationException("Cannot complete examination - not all disciplines have been evaluated");
        }
        
        // The result is already complete, just return it
        return result.ToDto();
    }
}
