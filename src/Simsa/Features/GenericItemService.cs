namespace Simsa.Features;

using Simsa.Interfaces.Services;
using Simsa.Model;

public class GenericItemService<TItem> : IGenericItemService<TItem>
    where TItem : IHasId<Guid>
{
    protected static List<TItem> Items = [];

    public Task AddAsync(TItem item, CancellationToken cancellationToken = default)
    {
        Items.Add(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Items.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    public Task<TItem[]> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(Items.ToArray());

    public Task<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(Items.Find(e => e.Id == id));

    public Task UpdateAsync(TItem item, CancellationToken cancellationToken = default)
    {
        var index = Items.FindIndex(e => e.Id == item.Id);
        if (index > -1 && index < Items.Count())
        {
            Items[index] = item;
        }

        return Task.CompletedTask;
    }
}