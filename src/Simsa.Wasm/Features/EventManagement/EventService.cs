namespace Simsa.Wasm.Features.EventManagement;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public class EventService : IEventService
{
    private const string Endpoint = "api/events";

    private const string IdEndpoint = $"{Endpoint}/{{0}}";

    private readonly ApiHttpClient apiClient;

    public EventService(ApiHttpClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public async Task AddAsync(Event item, CancellationToken cancellationToken = default)
        => await this.apiClient.PostAsync(Endpoint, item, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.DeleteAsync(BuildIdEndpoint(id), cancellationToken);

    public async Task<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<Event[]>(Endpoint, cancellationToken) ?? [];

    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<Event>(BuildIdEndpoint(id), cancellationToken);

    public async Task UpdateAsync(Event item, CancellationToken cancellationToken = default)
        => await this.apiClient.PutAsync(BuildIdEndpoint(item.Id), item, cancellationToken);

    private static string BuildIdEndpoint(Guid id) => string.Format(IdEndpoint, id);
}