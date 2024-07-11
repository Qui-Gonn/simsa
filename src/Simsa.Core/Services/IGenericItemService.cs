namespace Simsa.Core.Services;

public interface IGenericItemService<TItem>
{
    ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default);

    ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default);
}