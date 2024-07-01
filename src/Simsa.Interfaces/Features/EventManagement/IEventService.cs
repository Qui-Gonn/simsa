namespace Simsa.Interfaces.Features.EventManagement;

using Simsa.Model;

public interface IEventService
{
    Task AddAsync(Event item, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Event[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Event?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Event item, CancellationToken cancellationToken = default);
}