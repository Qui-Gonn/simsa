namespace Simsa.Interfaces.Features.EventManagement;

using Simsa.Model;

public interface IEventService
{
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Event[]> GetAllAsync(CancellationToken cancellationToken = default);
}