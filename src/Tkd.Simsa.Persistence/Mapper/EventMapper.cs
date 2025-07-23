namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;
using System.Text.Json;

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
        var participationData = JsonSerializer.Deserialize<ParticipationData>(entity.ParticipationData) ?? ParticipationData.NoParticipationData;
        
        // Map disciplines
        var disciplines = entity.Disciplines
            .OrderBy(d => d.Order)
            .Select(d => Discipline.Create(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        // Map examination progress if present
        ExaminationProgress? examinationProgress = null;
        if (!string.IsNullOrEmpty(entity.ExaminationProgressJson) && disciplines.Count > 0)
        {
            var progressData = JsonSerializer.Deserialize<ExaminationProgressData>(entity.ExaminationProgressJson);
            if (progressData != null)
            {
                var currentDiscipline = disciplines.FirstOrDefault(d => d.Name == progressData.CurrentDisciplineName);
                var completedDisciplines = disciplines.Where(d => progressData.CompletedDisciplineNames.Contains(d.Name)).ToHashSet();
                examinationProgress = new ExaminationProgress(disciplines, currentDiscipline, completedDisciplines);
            }
        }
        
        return new Event
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            ParticipationData = participationData,
            StartDate = entity.StartDate,
            ExaminationDisciplines = disciplines,
            ExaminationProgress = examinationProgress
        };
    }

    public EventEntity UpdateEntity(EventEntity entity, Event model)
    {
        entity.Description = model.Description;
        entity.Name = model.Name;
        entity.ParticipationData = JsonSerializer.Serialize(model.ParticipationData);
        entity.StartDate = model.StartDate;
        
        // Update examination progress if present
        if (model.ExaminationProgress != null)
        {
            var progressData = new ExaminationProgressData(
                model.ExaminationProgress.CurrentDiscipline?.Name ?? string.Empty,
                model.ExaminationProgress.CompletedDisciplines.Select(d => d.Name).ToList());
            entity.ExaminationProgressJson = JsonSerializer.Serialize(progressData);
        }
        else
        {
            entity.ExaminationProgressJson = null;
        }
        
        // Update disciplines collection
        UpdateDisciplines(entity, model.ExaminationDisciplines);
        
        return entity;
    }
    
    private static void UpdateDisciplines(EventEntity entity, IReadOnlyList<Discipline> disciplines)
    {
        // Clear existing disciplines
        entity.Disciplines.Clear();
        
        // Add new disciplines
        foreach (var discipline in disciplines)
        {
            entity.Disciplines.Add(new DisciplineEntity
            {
                Type = discipline.Type,
                Name = discipline.Name,
                Description = discipline.Description,
                Order = discipline.Order,
                EventId = entity.Id
            });
        }
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
    
    /// <summary>
    /// Data structure for JSON serialization of examination progress.
    /// </summary>
    private record ExaminationProgressData(
        string CurrentDisciplineName,
        IReadOnlyList<string> CompletedDisciplineNames);
}