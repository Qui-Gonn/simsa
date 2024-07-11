namespace Simsa.Core.Repositories;

public interface IGenericRepository<TModel>
{
    ValueTask<TModel?> AddAsync(TModel model, bool save, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(Guid id, bool save, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

    ValueTask<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    ValueTask<TModel?> UpdateAsync(TModel model, bool save, CancellationToken cancellationToken = default);
}