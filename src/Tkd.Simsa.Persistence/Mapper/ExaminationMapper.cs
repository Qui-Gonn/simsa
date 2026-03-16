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
        var participationData = JsonSerializer.Deserialize<ParticipationData>(entity.ParticipationData) ?? ParticipationData.NoParticipationData;
        
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
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            ParticipationData = participationData,
            StartDate = entity.StartDate,
            Disciplines = disciplines,
            Progress = examinationProgress
        };
    }

    public ExaminationEntity UpdateEntity(ExaminationEntity entity, Examination model)
    {
        entity.Description = model.Description;
        entity.Name = model.Name;
        entity.ParticipationData = JsonSerializer.Serialize(model.ParticipationData);
        entity.StartDate = model.StartDate;
        
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
