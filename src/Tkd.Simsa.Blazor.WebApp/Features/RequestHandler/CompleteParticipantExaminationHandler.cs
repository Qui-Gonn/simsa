using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for completing an examination for a participant.
/// </summary>
internal class CompleteParticipantExaminationHandler : IRequestHandler<CompleteParticipantExaminationCommand, ExaminationResultDto?>
{
    private readonly IExaminationResultRepository _resultRepository;
    private readonly IExaminationRepository _examinationRepository;
    
    public CompleteParticipantExaminationHandler(
        IExaminationResultRepository resultRepository,
        IExaminationRepository examinationRepository)
    {
        _resultRepository = resultRepository;
        _examinationRepository = examinationRepository;
    }
    
    public async Task<ExaminationResultDto?> Handle(CompleteParticipantExaminationCommand request, CancellationToken cancellationToken)
    {
        // Get examination and current result
        var examination = await _examinationRepository.GetByIdAsync(request.ExaminationId, cancellationToken);
        if (examination == null)
            return null;
            
        var result = await _resultRepository.GetByParticipantAsync(request.ExaminationId, request.ParticipantId, cancellationToken);
        if (result == null)
            return null;
            
        // Verify all disciplines have been completed
        if (!result.IsComplete(examination.Disciplines))
        {
            throw new InvalidOperationException("Cannot complete examination - not all disciplines have been evaluated");
        }
        
        // The result is already complete, just return it
        return result.ToDto();
    }
}
