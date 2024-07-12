namespace Simsa.Wasm.Features;

using Simsa.Core.Services;
using Simsa.Model;

public class GenericItemService<TItem> : IGenericItemService<TItem>
    where TItem : IHasId<Guid>
{
    private readonly ApiHttpClient apiClient;

    private readonly string endpoint;

    private readonly string idEndpoint;

    public GenericItemService(ApiHttpClient apiClient, string endpoint)
    {
        this.apiClient = apiClient;
        this.endpoint = endpoint;
        this.idEndpoint = $"{this.endpoint}/{{0}}";
    }

    public async ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PostAsync(this.endpoint, item, cancellationToken);

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.DeleteAsync(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<TItem[]>(this.endpoint, cancellationToken) ?? [];

    public async ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<TItem>(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PutAsync(this.BuildIdEndpoint(item.Id), item, cancellationToken);

    private string BuildIdEndpoint(Guid id) => string.Format(this.idEndpoint, id);
}