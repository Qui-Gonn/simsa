namespace Simsa.Features.EventManagement;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public class EventService : IEventService
{
    private static readonly List<Event> Events;

    static EventService()
    {
        var date1 = DateOnly.FromDateTime(DateTime.UtcNow);
        var date2 = DateOnly.FromDateTime(DateTime.UtcNow - TimeSpan.FromDays(1));
        var date3 = DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(1));

        Events =
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
        ];
    }

    public Task AddAsync(Event item, CancellationToken cancellationToken = default)
    {
        Events.Add(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Events.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    public Task<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(Events.ToArray());

    public Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(Events.Find(e => e.Id == id));

    public Task UpdateAsync(Event item, CancellationToken cancellationToken = default)
    {
        var index = Events.FindIndex(e => e.Id == item.Id);
        if (index > -1 && index < Events.Count())
        {
            Events[index] = item;
        }

        return Task.CompletedTask;
    }
}