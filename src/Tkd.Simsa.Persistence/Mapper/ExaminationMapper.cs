using System.Linq.Expressions;
using System.Text.Json;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;

namespace Tkd.Simsa.Persistence.Mapper;

/// <summary>
/// Mapper for Examination entity and domain model.
/// </summary>
internal class ExaminationMapper : IMapper<ExaminationEntity, Examination>
{
    public IPropertyMapper<ExaminationEntity, Examination> PropertyMapper { get; } = new ExaminationPropertyMapper();

    public ExaminationEntity ToEntity(Examination model)
        => UpdateEntity(new ExaminationEntity { Id = model.Id }, model);

    public Examination ToModel(ExaminationEntity entity)
    {
        var participationData = BaseEventMapper.DeserializeParticipationData(entity.ParticipationData);
        var (id, name, description, startDate, _) = BaseEventMapper.MapCommonPropertiesFromEntity(entity, participationData);
        
        // Map disciplines
        var disciplines = entity.Disciplines
            .OrderBy(d => d.Order)
            .Select(d => Discipline.Create(d.Type, d.Name, d.Description, d.Order))
            .ToList();
            
        // Map examination progress if present
        ExaminationProgress? examinationProgress = null;
        if (!string.IsNullOrEmpty(entity.ProgressJson) && disciplines.Count > 0)
        {
            var progressData = JsonSerializer.Deserialize<ExaminationProgressData>(entity.ProgressJson);
            if (progressData != null)
            {
                var currentDiscipline = disciplines.FirstOrDefault(d => d.Name == progressData.CurrentDisciplineName);
                var completedDisciplines = disciplines.Where(d => progressData.CompletedDisciplineNames.Contains(d.Name)).ToHashSet();
                examinationProgress = new ExaminationProgress(disciplines, currentDiscipline, completedDisciplines);
            }
        }
        
        return new Examination
        {
            Id = id,
            Name = name,
            Description = description,
            StartDate = startDate,
            ParticipationData = participationData,
            Disciplines = disciplines,
            Progress = examinationProgress
        };
    }

    public ExaminationEntity UpdateEntity(ExaminationEntity entity, Examination model)
    {
        var participationDataJson = BaseEventMapper.SerializeParticipationData(model.ParticipationData);
        BaseEventMapper.MapCommonPropertiesToEntity(model, participationDataJson, entity);
        
        // Update examination progress if present
        if (model.Progress != null)
        {
            var progressData = new ExaminationProgressData(
                model.Progress.CurrentDiscipline?.Name ?? string.Empty,
                model.Progress.CompletedDisciplines.Select(d => d.Name).ToList());
            entity.ProgressJson = JsonSerializer.Serialize(progressData);
        }
        else
        {
            entity.ProgressJson = null;
        }
        
        // Update disciplines collection
        UpdateDisciplines(entity, model.Disciplines);
        
        return entity;
    }
    
    private static void UpdateDisciplines(ExaminationEntity entity, IReadOnlyList<Discipline> disciplines)
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
                ExaminationId = entity.Id
            });
        }
    }

    private class ExaminationPropertyMapper : PropertyMapperBase<ExaminationEntity, Examination>
    {
        protected override Dictionary<string, Expression<Func<ExaminationEntity, object>>> PropertyMap { get; } = new()
        {
            { nameof(Examination.Id), i => i.Id },
            { nameof(Examination.Description), i => i.Description },
            { nameof(Examination.Name), i => i.Name },
            { nameof(Examination.StartDate), i => i.StartDate }
        };
    }
    
    /// <summary>
    /// Data structure for JSON serialization of examination progress.
    /// </summary>
    private record ExaminationProgressData(
        string CurrentDisciplineName,
        IReadOnlyList<string> CompletedDisciplineNames);
}
