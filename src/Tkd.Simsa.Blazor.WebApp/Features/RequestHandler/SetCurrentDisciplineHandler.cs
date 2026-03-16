using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for setting the current discipline in an examination.
/// </summary>
internal class SetCurrentDisciplineHandler : IRequestHandler<SetCurrentDisciplineCommand, ExaminationProgressDto?>
{
    private readonly IExaminationRepository _examinationRepository;
    
    public SetCurrentDisciplineHandler(IExaminationRepository examinationRepository)
    {
        _examinationRepository = examinationRepository;
    }
    
    public async Task<ExaminationProgressDto?> Handle(SetCurrentDisciplineCommand request, CancellationToken cancellationToken)
    {
        // Get current examination
        var examination = await _examinationRepository.GetByIdAsync(request.ExaminationId, cancellationToken);
        if (examination?.Progress == null)
            return null;
            
        // Validate discipline is part of examination
        if (!examination.Disciplines.Contains(request.Discipline))
            throw new ArgumentException($"Discipline '{request.Discipline.Name}' is not part of examination '{examination.Name}'");
        
        // Update progress
        var updatedProgress = examination.Progress.SetCurrentDiscipline(request.Discipline);
        var updatedExamination = await _examinationRepository.UpdateProgressAsync(
            request.ExaminationId, 
            updatedProgress, 
            cancellationToken);
            
        if (updatedExamination?.Progress == null)
            return null;
            
        var disciplineDtos = examination.Disciplines
            .Select(d => new DisciplineDto(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        return updatedExamination.Progress.ToProgressDto(disciplineDtos);
    }
}
