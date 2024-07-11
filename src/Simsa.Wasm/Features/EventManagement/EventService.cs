namespace Simsa.Wasm.Features.EventManagement;

using Simsa.Core.Features.EventManagement;
using Simsa.Model;

public class EventService : GenericItemService<Event>, IEventService
{
    private const string Endpoint = "api/events";

    public EventService(ApiHttpClient apiClient)
        : base(apiClient, Endpoint)
    {
    }
}