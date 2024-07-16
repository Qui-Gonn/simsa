namespace Simsa.Features;

using MediatR;

using Simsa.Core.Features;
using Simsa.Core.Services;

public abstract class GetAllItemsHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<GetAllItemsQuery<TItem>, IEnumerable<TItem>>
{
    public async Task<IEnumerable<TItem>> Handle(GetAllItemsQuery<TItem> query, CancellationToken cancellationToken)
        => await service.GetAllAsync(cancellationToken);
}

public abstract class GetItemByIdHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<GetItemByIdQuery<TItem>, TItem?>
{
    public async Task<TItem?> Handle(GetItemByIdQuery<TItem> query, CancellationToken cancellationToken)
        => await service.GetByIdAsync(query.Id, cancellationToken);
}

public abstract class AddItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<AddItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(AddItemCommand<TItem> command, CancellationToken cancellationToken)
        => await service.AddAsync(command.Item, cancellationToken);
}

public abstract class UpdateItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<UpdateItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(UpdateItemCommand<TItem> command, CancellationToken cancellationToken)
        => await service.UpdateAsync(command.Item, cancellationToken);
}

public abstract class DeleteItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<DeleteItemCommand<TItem>>
{
    public async Task Handle(DeleteItemCommand<TItem> command, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(command.Id, cancellationToken);
    }
}