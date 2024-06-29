namespace Simsa.Features.EventManagement;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public class EventService : IEventService
{
    public ValueTask<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var date1 = DateOnly.FromDateTime(DateTime.UtcNow);
        var date2 = DateOnly.FromDateTime(DateTime.UtcNow - TimeSpan.FromDays(1));
        var date3 = DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(1));
        return
            ValueTask.FromResult<Event[]>(
            [
                new Event
                {
                    Description = $"Prüfung am {date1:D}",
                    Id = Guid.NewGuid(),
                    Name = "Prüfung 2",
                    Participations = Participations.NoParticipations,
                    StartDate = date1
                },
                new Event
                {
                    Description = $"Prüfung am {date2:D}",
                    Id = Guid.NewGuid(),
                    Name = "Prüfung 1",
                    Participations = Participations.NoParticipations,
                    StartDate = date2
                },
                new Event
                {
                    Description = $"Prüfung am {date3:D}",
                    Id = Guid.NewGuid(),
                    Name = "Prüfung 3",
                    Participations = Participations.NoParticipations,
                    StartDate = date3
                }
            ]);
    }
}