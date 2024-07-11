namespace Simsa.Persistence.Repositories;

using Simsa.Core.Repositories;
using Simsa.Model;
using Simsa.Persistence.Entities;
using Simsa.Persistence.Mapper;

internal class EventRepository : GenericRepository<EventEntity, Event>, IEventRepository
{
    public EventRepository(SimsaDbContext dbContext, IMapper<EventEntity, Event> mapper)
        : base(dbContext, mapper)
    {
    }
}