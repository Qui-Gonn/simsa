namespace Simsa.Features.EventManagement;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Interfaces.Repositories;
using Simsa.Model;

public class EventService : GenericItemService<Event>, IEventService
{
    public EventService(IGenericRepository<Event> repository)
        : base(repository)
    {
    }
}