using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for completing a discipline across all participants.
/// </summary>
internal class CompleteDisciplineHandler : IRequestHandler<CompleteDisciplineCommand, ExaminationProgressDto?>
{
    private readonly IEventRepository _eventRepository;
    
    public CompleteDisciplineHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<ExaminationProgressDto?> Handle(CompleteDisciplineCommand request, CancellationToken cancellationToken)
    {
        // Get current examination
        var examination = await _eventRepository.GetExaminationByIdAsync(request.ExaminationId, cancellationToken);
        if (examination?.ExaminationProgress == null)
            return null;
            
        // Validate discipline is part of examination
        if (!examination.ExaminationDisciplines.Contains(request.Discipline))
            throw new ArgumentException($"Discipline '{request.Discipline.Name}' is not part of examination '{examination.Name}'");
        
        // Complete the discipline and update progress
        var updatedProgress = examination.ExaminationProgress.CompleteDiscipline(request.Discipline);
        var updatedExamination = await _eventRepository.UpdateExaminationProgressAsync(
            request.ExaminationId, 
            updatedProgress, 
            cancellationToken);
            
        if (updatedExamination?.ExaminationProgress == null)
            return null;
            
        var disciplineDtos = examination.ExaminationDisciplines
            .Select(d => new DisciplineDto(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        return updatedExamination.ExaminationProgress.ToProgressDto(disciplineDtos);
    }
}
