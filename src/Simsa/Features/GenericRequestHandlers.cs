namespace Simsa.Features;

using MediatR;

using Simsa.Core.Features;
using Simsa.Core.Repositories;

internal abstract class GetAllItemsHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetAllItemsQuery<TItem>, IEnumerable<TItem>>
{
    public async Task<IEnumerable<TItem>> Handle(GetAllItemsQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetAllAsync(cancellationToken);
}

internal abstract class GetItemByIdHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetItemByIdQuery<TItem>, TItem?>
{
    public async Task<TItem?> Handle(GetItemByIdQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetByIdAsync(query.Id, cancellationToken);
}

internal abstract class AddItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<AddItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(AddItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.AddAsync(command.Item, true, cancellationToken);
}

internal abstract class UpdateItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<UpdateItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(UpdateItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.UpdateAsync(command.Item, true, cancellationToken);
}

internal abstract class DeleteItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<DeleteItemCommand<TItem>>
{
    public async Task Handle(DeleteItemCommand<TItem> command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.Id, true, cancellationToken);
    }
}