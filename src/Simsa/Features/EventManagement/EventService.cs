namespace Simsa.Features.EventManagement;

using Simsa.Core.Features.EventManagement;
using Simsa.Core.Repositories;
using Simsa.Model;

public class EventService : GenericItemService<Event>, IEventService
{
    public EventService(IGenericRepository<Event> repository)
        : base(repository)
    {
    }
}