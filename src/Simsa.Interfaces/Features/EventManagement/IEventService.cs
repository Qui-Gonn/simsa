namespace Simsa.Interfaces.Features.EventManagement;

using Simsa.Model;

public interface IEventService
{
    ValueTask<Event[]> GetAllAsync(CancellationToken cancellationToken = default);
}