namespace Simsa.Persistence.Mapper;

internal interface IMapper<TEntity, TModel>
{
    TEntity ToEntity(TModel model);

    TModel ToModel(TEntity entity);

    TEntity UpdateEntity(TEntity entity, TModel model);
}