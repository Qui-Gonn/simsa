namespace Simsa.Client.Features.EventManagement;

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

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await this.httpClient.DeleteAsync(BuildIdEndpoint(id), cancellationToken);
    }

    public async ValueTask<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.httpClient.GetFromJsonAsync<Event[]>(Endpoint, cancellationToken) ?? [];

    private static string BuildIdEndpoint(Guid id) => string.Format(IdEndpoint, id);
}