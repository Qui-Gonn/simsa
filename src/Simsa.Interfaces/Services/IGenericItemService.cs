namespace Simsa.Interfaces.Services;

public interface IGenericItemService<TItem>
{
    Task AddAsync(TItem item, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TItem[]> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(TItem item, CancellationToken cancellationToken = default);
}