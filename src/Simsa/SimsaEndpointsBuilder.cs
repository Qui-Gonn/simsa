namespace Simsa;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

using Simsa.Core.Features;
using Simsa.Model;

public class SimsaEndpointsBuilder
{
    public const string ApiRoute = "/api";

    private readonly IEndpointRouteBuilder builder;

    private SimsaEndpointsBuilder(IEndpointRouteBuilder builder)
    {
        this.builder = builder;
        this.ApiRouteGroupBuilder = builder.MapGroup(ApiRoute);
    }

    protected RouteGroupBuilder ApiRouteGroupBuilder { get; }

    public static SimsaEndpointsBuilder Create(IEndpointRouteBuilder builder)
    {
        return new SimsaEndpointsBuilder(builder);
    }

    public ISimsaEndpointGroup<TItem> MapFullGroup<TItem>()
        where TItem : IHasId<Guid>
        => this.MapFullGroup<TItem>(typeof(TItem).Name);

    public ISimsaEndpointGroup<TItem> MapFullGroup<TItem>(string groupName)
        where TItem : IHasId<Guid>
        => this.MapGroup<TItem>()
            .MapGet()
            .MapGetById()
            .MapPost()
            .MapPut()
            .MapDelete();

    public ISimsaEndpointGroup<TItem> MapGroup<TItem>()
        where TItem : IHasId<Guid>
        => this.MapGroup<TItem>(typeof(TItem).Name);

    public ISimsaEndpointGroup<TItem> MapGroup<TItem>(string groupName)
        where TItem : IHasId<Guid>
        => new SimsaEndpointGroup<TItem>(this.ApiRouteGroupBuilder, groupName);
}

public interface ISimsaEndpointGroup<TItem>
    where TItem : IHasId<Guid>
{
    ISimsaEndpointGroup<TItem> MapDelete();

    ISimsaEndpointGroup<TItem> MapGet();

    ISimsaEndpointGroup<TItem> MapGetById();

    ISimsaEndpointGroup<TItem> MapPost();

    ISimsaEndpointGroup<TItem> MapPut();
}

file class SimsaEndpointGroup<TItem> : ISimsaEndpointGroup<TItem>
    where TItem : IHasId<Guid>
{
    private readonly string groupName;

    public SimsaEndpointGroup(RouteGroupBuilder apiRouteGroupBuilder, string groupName)
    {
        this.groupName = groupName;
        this.RouteGroupBuilder = apiRouteGroupBuilder.MapGroup(this.groupName);
    }

    private RouteGroupBuilder RouteGroupBuilder { get; }

    public ISimsaEndpointGroup<TItem> MapDelete()
    {
        this.RouteGroupBuilder.MapDelete(
                "{id:guid}",
                async (Guid id, IMediator mediator) =>
                {
                    await mediator.Send(new DeleteItemCommand<TItem>(id));
                    return TypedResults.NoContent();
                })
            .WithName($"Delete{this.groupName}");
        return this;
    }

    public ISimsaEndpointGroup<TItem> MapGet()
    {
        this.RouteGroupBuilder.MapGet(
                string.Empty,
                async (IMediator mediator)
                    => TypedResults.Ok(await mediator.Send(new GetAllItemsQuery<TItem>())))
            .WithName($"Get{this.groupName}");
        return this;
    }

    public ISimsaEndpointGroup<TItem> MapGetById()
    {
        this.RouteGroupBuilder.MapGet(
                "{id:guid}",
                async Task<Results<Ok<TItem>, NotFound>> (Guid id, IMediator mediator)
                    => await mediator.Send(new GetItemByIdQuery<TItem>(id)) is { } itemById ? TypedResults.Ok(itemById) : TypedResults.NotFound())
            .WithName($"Get{this.groupName}ById");
        return this;
    }

    public ISimsaEndpointGroup<TItem> MapPost()
    {
        this.RouteGroupBuilder.MapPost(
                string.Empty,
                async (TItem itemToAdd, IMediator mediator) =>
                {
                    var newItem = await mediator.Send(new AddItemCommand<TItem>(itemToAdd));
                    return TypedResults.Created($"{SimsaEndpointsBuilder.ApiRoute}/{this.groupName}/{newItem?.Id ?? itemToAdd.Id}", newItem);
                })
            .WithName($"Add{this.groupName}");
        return this;
    }

    public ISimsaEndpointGroup<TItem> MapPut()
    {
        this.RouteGroupBuilder.MapPut(
                "{id:guid}",
                async (Guid id, TItem itemToUpdate, IMediator mediator)
                    => TypedResults.Ok(await mediator.Send(new UpdateItemCommand<TItem>(itemToUpdate))))
            .WithName($"Update{this.groupName}");
        return this;
    }
}