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

    public SimsaEndpointGroup<TItem> MapFullGroup<TItem>()
        where TItem : IHasId<Guid>
        => this.MapFullGroup<TItem>(typeof(TItem).Name);

    public SimsaEndpointGroup<TItem> MapFullGroup<TItem>(string groupName)
        where TItem : IHasId<Guid>
        => this.MapGroup<TItem>()
            .MapGet()
            .MapGetById()
            .MapPost()
            .MapPut()
            .MapDelete();

    public SimsaEndpointGroup<TItem> MapGroup<TItem>()
        where TItem : IHasId<Guid>
        => this.MapGroup<TItem>(typeof(TItem).Name);

    public SimsaEndpointGroup<TItem> MapGroup<TItem>(string groupName)
        where TItem : IHasId<Guid>
        => new (this.ApiRouteGroupBuilder, groupName);
}

public class SimsaEndpointGroup<TItem>
    where TItem : IHasId<Guid>
{
    private readonly string groupName;

    public SimsaEndpointGroup(RouteGroupBuilder apiRouteGroupBuilder, string groupName)
    {
        this.groupName = groupName;
        this.RouteGroupBuilder = apiRouteGroupBuilder.MapGroup(this.groupName);
    }

    private RouteGroupBuilder RouteGroupBuilder { get; }

    public SimsaEndpointGroup<TItem> MapDelete()
    {
        this.RouteGroupBuilder.MapDelete("/{id:guid}", Delete).WithName($"Delete{this.groupName}");
        return this;
    }

    public SimsaEndpointGroup<TItem> MapGet()
    {
        this.RouteGroupBuilder.MapGet("/", Get).WithName($"Get{this.groupName}");
        return this;
    }

    public SimsaEndpointGroup<TItem> MapGetById()
    {
        this.RouteGroupBuilder.MapGet("/{id:guid}", GetById).WithName($"Get{this.groupName}ById");
        return this;
    }

    public SimsaEndpointGroup<TItem> MapPost()
    {
        this.RouteGroupBuilder.MapPost("/", this.Post).WithName($"Add{this.groupName}");
        return this;
    }

    public SimsaEndpointGroup<TItem> MapPut()
    {
        this.RouteGroupBuilder.MapPut("/{id:guid}", Put).WithName($"Update{this.groupName}");
        return this;
    }

    private static async Task<NoContent> Delete(Guid id, IMediator mediator)
    {
        await mediator.Send(new DeleteItemCommand<TItem>(id));
        return TypedResults.NoContent();
    }

    private static async Task<Ok<IEnumerable<TItem>>> Get(IMediator mediator)
        => TypedResults.Ok(await mediator.Send(new GetAllItemsQuery<TItem>()));

    private static async Task<Results<Ok<TItem>, NotFound>> GetById(Guid id, IMediator mediator)
        => await mediator.Send(new GetItemByIdQuery<TItem>(id)) is { } itemById ? TypedResults.Ok(itemById) : TypedResults.NotFound();

    private static async Task<Ok<TItem>> Put(Guid id, TItem itemToUpdate, IMediator mediator)
        => TypedResults.Ok(await mediator.Send(new UpdateItemCommand<TItem>(itemToUpdate)));

    private async Task<Created<TItem>> Post(TItem itemToAdd, IMediator mediator)
    {
        var newItem = await mediator.Send(new AddItemCommand<TItem>(itemToAdd));
        return TypedResults.Created($"{SimsaEndpointsBuilder.ApiRoute}/{this.groupName}/{newItem?.Id ?? itemToAdd.Id}", newItem);
    }
}