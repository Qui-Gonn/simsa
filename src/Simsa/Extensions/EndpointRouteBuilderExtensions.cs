namespace Simsa.Extensions;

using Microsoft.AspNetCore.Http.HttpResults;

using Simsa.Interfaces.Features.EventManagement;
using Simsa.Interfaces.Features.PersonManagement;
using Simsa.Interfaces.Services;
using Simsa.Model;

public static class RouteGroupBuilderExtensions
{
    private const string ApiRoute = "/api";

    public static void AddApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup(ApiRoute)
            .MapRouteGroup<Event, IEventService>(
                new EndpointDefinition("events", "GetEvents", "GetEventById", "AddEvent", "UpdateEvent", "DeleteEvent"))
            .MapRouteGroup<Person, IPersonService>(
                new EndpointDefinition("persons", "GetPersons", "GetPersonById", "AddPerson", "UpdatePerson", "DeletePerson"));
    }

    private static RouteGroupBuilder MapEndpoints<TItem, TService>(this RouteGroupBuilder routeGroupBuilder, EndpointDefinition definition)
        where TItem : IHasId<Guid>
        where TService : IGenericItemService<TItem>
    {
        routeGroupBuilder.MapGet(
                string.Empty,
                async (TService service)
                    => TypedResults.Ok(await service.GetAllAsync()))
            .WithName(definition.GetEndpoint);
        routeGroupBuilder.MapGet(
                "{id:guid}",
                async Task<Results<Ok<TItem>, NotFound>> (Guid id, TService service)
                    => await service.GetByIdAsync(id) is { } itemById ? TypedResults.Ok(itemById) : TypedResults.NotFound())
            .WithName(definition.GetByIdEndpoint);
        routeGroupBuilder.MapPost(
                string.Empty,
                async (TItem newItem, TService service) =>
                {
                    await service.AddAsync(newItem);
                    return TypedResults.Created($"{ApiRoute}/{definition.GroupName}/{newItem.Id}", newItem);
                })
            .WithName(definition.AddEndpoint);
        routeGroupBuilder.MapPut(
                "{id:guid}",
                async (Guid _, TItem updatedItem, TService service) =>
                {
                    await service.UpdateAsync(updatedItem);
                    return TypedResults.Ok();
                })
            .WithName(definition.UpdateEnpoint);
        routeGroupBuilder.MapDelete(
                "{id:guid}",
                async (Guid id, TService service) =>
                {
                    await service.DeleteAsync(id);
                    return TypedResults.NoContent();
                })
            .WithName(definition.DeleteEndpoint);

        return routeGroupBuilder;
    }

    private static RouteGroupBuilder MapRouteGroup<TItem, TService>(this RouteGroupBuilder apiRouteGroup, EndpointDefinition definition)
    {
        apiRouteGroup.MapGroup(definition.GroupName)
            .MapEndpoints<Event, IEventService>(definition);

        return apiRouteGroup;
    }

    private record EndpointDefinition(
        string GroupName,
        string GetEndpoint,
        string GetByIdEndpoint,
        string AddEndpoint,
        string UpdateEnpoint,
        string DeleteEndpoint);
}