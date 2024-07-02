namespace Simsa.Wasm.Features.EventManagement;

using System.Net.Http.Json;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public class EventService : IEventService
{
    private const string Endpoint = "api/events";

    private const string IdEndpoint = $"{Endpoint}/{{0}}";

    private readonly HttpClient httpClient;

    public EventService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task AddAsync(Event item, CancellationToken cancellationToken = default)
        => await this.httpClient.PostAsJsonAsync(Endpoint, item, cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.httpClient.DeleteAsync(BuildIdEndpoint(id), cancellationToken);

    public async Task<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.httpClient.GetFromJsonAsync<Event[]>(Endpoint, cancellationToken) ?? [];

    public async Task<Event?> GetById(Guid id, CancellationToken cancellationToken = default)
        => await this.httpClient.GetFromJsonAsync<Event>(BuildIdEndpoint(id), cancellationToken);

    public async Task UpdateAsync(Event item, CancellationToken cancellationToken = default)
        => await this.httpClient.PutAsJsonAsync(BuildIdEndpoint(item.Id), item, cancellationToken);

    private static string BuildIdEndpoint(Guid id) => string.Format(IdEndpoint, id);
}