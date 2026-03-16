namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;

internal class EventMapper : IMapper<EventEntity, Event>
{
    public IPropertyMapper<EventEntity, Event> PropertyMapper { get; } = new EventPropertyMapper();

    public EventEntity ToEntity(Event model)
        => this.UpdateEntity(new EventEntity { Id = model.Id }, model);

    public Event ToModel(EventEntity entity)
    {
        var participationData = BaseEventMapper.DeserializeParticipationData(entity.ParticipationData);
        var (id, name, description, startDate, _) = BaseEventMapper.MapCommonPropertiesFromEntity(entity, participationData);
        
        return new Event
        {
            Id = id,
            Name = name,
            Description = description,
            StartDate = startDate,
            ParticipationData = participationData
        };
    }

    public EventEntity UpdateEntity(EventEntity entity, Event model)
    {
        var participationDataJson = BaseEventMapper.SerializeParticipationData(model.ParticipationData);
        BaseEventMapper.MapCommonPropertiesToEntity(model, participationDataJson, entity);
        
        return entity;
    }

    private class EventPropertyMapper : PropertyMapperBase<EventEntity, Event>
    {
        protected override Dictionary<string, Expression<Func<EventEntity, object>>> PropertyMap { get; } = new ()
        {
            { nameof(Event.Id), i => i.Id },
            { nameof(Event.Description), i => i.Description },
            { nameof(Event.Name), i => i.Name },
            { nameof(Event.StartDate), i => i.StartDate }
        };
    }
}