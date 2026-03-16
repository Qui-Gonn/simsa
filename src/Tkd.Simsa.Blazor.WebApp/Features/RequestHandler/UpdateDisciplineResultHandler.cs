using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for updating discipline results.
/// </summary>
internal class UpdateDisciplineResultHandler : IRequestHandler<UpdateDisciplineResultCommand, ExaminationResultDto?>
{
    private readonly IExaminationResultRepository _resultRepository;
    private readonly IExaminationRepository _examinationRepository;
    
    public UpdateDisciplineResultHandler(
        IExaminationResultRepository resultRepository,
        IExaminationRepository examinationRepository)
    {
        _resultRepository = resultRepository;
        _examinationRepository = examinationRepository;
    }
    
    public async Task<ExaminationResultDto?> Handle(UpdateDisciplineResultCommand request, CancellationToken cancellationToken)
    {
        // Validate examination exists
        var examination = await _examinationRepository.GetByIdAsync(request.ExaminationId, cancellationToken);
        if (examination == null)
            return null;
            
        // Validate discipline is part of examination
        if (!examination.Disciplines.Contains(request.Discipline))
            throw new ArgumentException($"Discipline '{request.Discipline.Name}' is not part of examination '{examination.Name}'");
        
        // Update or create examination result
        var result = await _resultRepository.UpdateDisciplineResultAsync(
            request.ExaminationId,
            request.ParticipantId,
            request.Discipline,
            request.Rating,
            request.Notes,
            request.DocumentedBy,
            cancellationToken);
            
        return result?.ToDto();
    }
}
