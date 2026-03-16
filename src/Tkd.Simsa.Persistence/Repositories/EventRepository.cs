namespace Tkd.Simsa.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Tkd.Simsa.Application.EventManagement;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;

internal class EventRepository : GenericRepository<EventEntity, Event>, IEventRepository
{
    public EventRepository(SimsaDbContext dbContext, IMapper<EventEntity, Event> mapper)
        : base(dbContext, mapper)
    {
    }
}