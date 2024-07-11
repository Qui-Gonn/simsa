namespace Simsa.Features;

using Simsa.Core.Repositories;
using Simsa.Core.Services;
using Simsa.Model;

public class GenericItemService<TItem> : IGenericItemService<TItem>
    where TItem : IHasId<Guid>
{
    public GenericItemService(IGenericRepository<TItem> repository)
    {
        this.Repository = repository;
    }

    protected IGenericRepository<TItem> Repository { get; }

    public ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default)
        => this.Repository.AddAsync(item, true, cancellationToken);

    public ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => this.Repository.DeleteAsync(id, true, cancellationToken);

    public ValueTask<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => this.Repository.GetAllAsync(cancellationToken);

    public ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => this.Repository.GetByIdAsync(id, cancellationToken);

    public ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default)
        => this.Repository.UpdateAsync(item, true, cancellationToken);
}