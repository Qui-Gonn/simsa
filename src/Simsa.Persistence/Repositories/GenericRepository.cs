namespace Simsa.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using Simsa.Interfaces.Repositories;
using Simsa.Model;
using Simsa.Persistence.Mapper;

internal abstract class GenericRepository<TEntity, TModel> : IGenericRepository<TModel>
    where TEntity : class, IHasId<Guid>
    where TModel : class, IHasId<Guid>
{
    public GenericRepository(SimsaDbContext dbContext, IMapper<TEntity, TModel> mapper)
    {
        this.DbContext = dbContext;
        this.Mapper = mapper;
    }

    protected DbSet<TEntity> Data => this.DbContext.Set<TEntity>();

    protected SimsaDbContext DbContext { get; }

    protected IMapper<TEntity, TModel> Mapper { get; }

    public async ValueTask<TModel?> AddAsync(TModel model, bool save, CancellationToken cancellationToken = default)
    {
        var entity = this.Mapper.ToEntity(model);
        var entityEntry = this.Data.Add(entity);

        if (save)
        {
            await this.SaveChangesAsync(cancellationToken);
            return await this.GetByIdAsync(entity.Id, cancellationToken);
        }

        return this.Mapper.ToModel(entityEntry.Entity);
    }

    public async ValueTask DeleteAsync(Guid id, bool save, CancellationToken cancellationToken = default)
    {
        if (await this.GetEntityByIdAsync(id, cancellationToken) is { } existingEntity)
        {
            this.Data.Remove(existingEntity);
        }

        if (save)
        {
            await this.SaveChangesAsync(cancellationToken);
        }
    }

    public async ValueTask<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await this.Data
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Select(this.Mapper.ToModel);

    public async ValueTask<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.Data.FindAsync(id) is { } entity
            ? this.Mapper.ToModel(entity)
            : default;

    public async ValueTask<TEntity?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.Data.FindAsync(id);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => this.DbContext.SaveChangesAsync(cancellationToken);

    public async ValueTask<TModel?> UpdateAsync(TModel model, bool save, CancellationToken cancellationToken = default)
    {
        if (await this.GetEntityByIdAsync(model.Id, cancellationToken) is not { } existingEntity)
        {
            return default;
        }

        this.Mapper.UpdateEntity(existingEntity, model);
        var entityEntry = this.Data.Update(existingEntity);

        if (save)
        {
            await this.SaveChangesAsync(cancellationToken);
            return await this.GetByIdAsync(model.Id, cancellationToken);
        }

        return this.Mapper.ToModel(entityEntry.Entity);
    }
}