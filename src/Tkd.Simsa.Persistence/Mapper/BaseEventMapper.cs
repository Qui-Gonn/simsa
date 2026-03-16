namespace Tkd.Simsa.Persistence.Mapper;

using System.Text.Json;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;

/// <summary>
/// Base mapper providing common functionality for mapping between event entities and domain models.
/// </summary>
internal static class BaseEventMapper
{
    /// <summary>
    /// Maps common event properties from entity to domain model.
    /// </summary>
    /// <param name="entity">The entity to map from</param>
    /// <param name="participationData">The deserialized participation data</param>
    /// <returns>A tuple containing the common properties</returns>
    internal static (Guid Id, string Name, string Description, DateOnly StartDate, ParticipationData ParticipationData) 
        MapCommonPropertiesFromEntity(BaseEventEntity entity, ParticipationData participationData)
    {
        return (
            entity.Id,
            entity.Name,
            entity.Description,
            entity.StartDate,
            participationData
        );
    }

    /// <summary>
    /// Maps common event properties from domain model to entity.
    /// </summary>
    /// <param name="domainModel">The domain model to map from</param>
    /// <param name="participationDataJson">The serialized participation data</param>
    /// <param name="entity">The entity to populate</param>
    internal static void MapCommonPropertiesToEntity(BaseEvent domainModel, string participationDataJson, BaseEventEntity entity)
    {
        entity.Id = domainModel.Id;
        entity.Name = domainModel.Name;
        entity.Description = domainModel.Description;
        entity.StartDate = domainModel.StartDate;
        entity.ParticipationData = participationDataJson;
    }

    /// <summary>
    /// Serializes participation data to JSON.
    /// </summary>
    /// <param name="participationData">The participation data to serialize</param>
    /// <returns>JSON string representation</returns>
    internal static string SerializeParticipationData(ParticipationData participationData)
    {
        return JsonSerializer.Serialize(participationData);
    }

    /// <summary>
    /// Deserializes participation data from JSON.
    /// </summary>
    /// <param name="participationDataJson">The JSON string to deserialize</param>
    /// <returns>The deserialized participation data</returns>
    internal static ParticipationData DeserializeParticipationData(string participationDataJson)
    {
        if (string.IsNullOrWhiteSpace(participationDataJson))
            return ParticipationData.NoParticipationData;

        try
        {
            return JsonSerializer.Deserialize<ParticipationData>(participationDataJson) 
                   ?? ParticipationData.NoParticipationData;
        }
        catch (JsonException)
        {
            return ParticipationData.NoParticipationData;
        }
    }
}
