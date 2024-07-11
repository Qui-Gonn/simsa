namespace Simsa.Persistence.Mapper;

using Simsa.Model;
using Simsa.Persistence.Entities;

internal class EventMapper : IMapper<EventEntity, Event>
{
    public EventEntity ToEntity(Event model)
        => this.UpdateEntity(new EventEntity { Id = model.Id }, model);

    public Event ToModel(EventEntity entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description ?? string.Empty,
            StartDate = entity.StartDate
        };

    public EventEntity UpdateEntity(EventEntity entity, Event model)
    {
        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.StartDate = model.StartDate;
        return entity;
    }
}