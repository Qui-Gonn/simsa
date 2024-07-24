namespace Simsa.Core.Features;

using MediatR;

public record GetAllItemsQuery<TItem> : IRequest<IEnumerable<TItem>>;

public record GetItemByIdQuery<TItem>(Guid Id) : IRequest<TItem?>;

public record AddItemCommand<TItem>(TItem Item) : IRequest<TItem?>;

public record UpdateItemCommand<TItem>(TItem Item) : IRequest<TItem?>;

public record DeleteItemCommand<TItem>(Guid Id) : IRequest;