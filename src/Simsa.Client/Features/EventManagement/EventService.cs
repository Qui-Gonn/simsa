namespace Simsa.Client.Features.EventManagement;

using System.Net.Http.Json;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Model;

public class EventService : IEventService
{
    private const string Endpoint = "api/events";

    private readonly HttpClient httpClient;

    public EventService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async ValueTask<Event[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var allEvents = await this.httpClient.GetFromJsonAsync<Event[]>(Endpoint, cancellationToken);
        return allEvents ?? [];
    }
}