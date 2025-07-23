using MediatR;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

/// <summary>
/// Handler for getting examination participants.
/// </summary>
internal class GetExaminationParticipantsHandler : IRequestHandler<GetExaminationParticipantsQuery, IEnumerable<ParticipantDto>>
{
    private readonly IEventRepository _eventRepository;
    
    public GetExaminationParticipantsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<IEnumerable<ParticipantDto>> Handle(GetExaminationParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await _eventRepository.GetExaminationParticipantsAsync(request.ExaminationId, cancellationToken);
        
        return participants.Select(p => new ParticipantDto(
            p.Id,
            p.PersonInfo.Name.ToString(),
            "Unknown", // Belt rank not available in current PersonInfo
            p.PersonInfo.DateOfBirth.Age,
            p.PersonInfo.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue)));
    }
}
