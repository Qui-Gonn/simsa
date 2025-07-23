using System.Linq.Expressions;
using System.Text.Json;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;

namespace Tkd.Simsa.Persistence.Mapper;

/// <summary>
/// Mapper for ExaminationResult entity and domain model.
/// </summary>
internal class ExaminationResultMapper : IMapper<ExaminationResultEntity, ExaminationResult>
{
    public IPropertyMapper<ExaminationResultEntity, ExaminationResult> PropertyMapper { get; } = new ExaminationResultPropertyMapper();

    public ExaminationResultEntity ToEntity(ExaminationResult model)
        => UpdateEntity(new ExaminationResultEntity { Id = model.Id }, model);

    public ExaminationResult ToModel(ExaminationResultEntity entity)
    {
        var resultsData = JsonSerializer.Deserialize<Dictionary<string, DisciplineResultData>>(entity.ResultsJson) 
                         ?? new Dictionary<string, DisciplineResultData>();
        
        var results = new Dictionary<Discipline, DisciplineResult>();
        
        foreach (var (disciplineName, resultData) in resultsData)
        {
            // Note: In a full implementation, we'd need to resolve the Discipline from the examination
            // For now, we'll create a minimal discipline representation
            var discipline = new Discipline(
                resultData.DisciplineType,
                disciplineName,
                string.Empty,
                resultData.Order);
                
            var rating = DisciplineRating.FromNumeric(resultData.RatingValue);
            var notes = ExaminationNotes.Create(resultData.Notes);
            
            var disciplineResult = DisciplineResult.Create(
                rating,
                notes,
                resultData.DocumentedBy);
            
            results[discipline] = disciplineResult;
        }
        
        return new ExaminationResult
        {
            Id = entity.Id,
            ParticipantId = entity.ParticipantId,
            ExaminationId = entity.ExaminationId,
            Results = results,
            LastUpdated = entity.LastUpdated
        };
    }

    public ExaminationResultEntity UpdateEntity(ExaminationResultEntity entity, ExaminationResult model)
    {
        entity.ParticipantId = model.ParticipantId;
        entity.ExaminationId = model.ExaminationId;
        entity.LastUpdated = model.LastUpdated;
        
        // Serialize results to JSON
        var resultsData = model.Results.ToDictionary(
            kvp => kvp.Key.Name,
            kvp => new DisciplineResultData(
                kvp.Key.Type,
                kvp.Key.Order,
                kvp.Value.Rating.NumericValue,
                kvp.Value.Notes.Content,
                kvp.Value.DocumentedAt,
                kvp.Value.DocumentedBy));
                
        entity.ResultsJson = JsonSerializer.Serialize(resultsData);
        
        return entity;
    }

    private class ExaminationResultPropertyMapper : PropertyMapperBase<ExaminationResultEntity, ExaminationResult>
    {
        protected override Dictionary<string, Expression<Func<ExaminationResultEntity, object>>> PropertyMap { get; } = new()
        {
            { nameof(ExaminationResult.Id), i => i.Id },
            { nameof(ExaminationResult.ParticipantId), i => i.ParticipantId },
            { nameof(ExaminationResult.ExaminationId), i => i.ExaminationId },
            { nameof(ExaminationResult.LastUpdated), i => i.LastUpdated }
        };
    }
    
    /// <summary>
    /// Data structure for JSON serialization of discipline results.
    /// </summary>
    private record DisciplineResultData(
        DisciplineType DisciplineType,
        int Order,
        int RatingValue,
        string Notes,
        DateTime DocumentedAt,
        string DocumentedBy);
}
