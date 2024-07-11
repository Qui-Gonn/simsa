namespace Simsa.Core.Services;

public interface ICrudManager<TItem, in TId>
{
    Task AddAsync(TItem item, CancellationToken cancellationToken = default);

    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);

    Task<TItem[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(TItem item, CancellationToken cancellationToken = default);
}