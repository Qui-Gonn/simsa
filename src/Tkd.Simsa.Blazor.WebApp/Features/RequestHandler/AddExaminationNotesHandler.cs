using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for adding notes to examination results.
/// </summary>
internal class AddExaminationNotesHandler : IRequestHandler<AddExaminationNotesCommand, ExaminationResultDto?>
{
    private readonly IExaminationResultRepository _resultRepository;
    private readonly IEventRepository _eventRepository;
    
    public AddExaminationNotesHandler(
        IExaminationResultRepository resultRepository,
        IEventRepository eventRepository)
    {
        _resultRepository = resultRepository;
        _eventRepository = eventRepository;
    }
    
    public async Task<ExaminationResultDto?> Handle(AddExaminationNotesCommand request, CancellationToken cancellationToken)
    {
        // Validate examination exists
        var examination = await _eventRepository.GetExaminationByIdAsync(request.ExaminationId, cancellationToken);
        if (examination == null)
            return null;
            
        // Validate discipline is part of examination
        if (!examination.ExaminationDisciplines.Contains(request.Discipline))
            throw new ArgumentException($"Discipline '{request.Discipline.Name}' is not part of examination '{examination.Name}'");
        
        // Add notes to examination result
        var result = await _resultRepository.AddDisciplineNotesAsync(
            request.ExaminationId,
            request.ParticipantId,
            request.Discipline,
            request.Notes,
            request.DocumentedBy,
            cancellationToken);
            
        return result?.ToDto();
    }
}
