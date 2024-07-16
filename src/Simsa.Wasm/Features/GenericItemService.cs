namespace Simsa.Wasm.Features;

using Microsoft.Extensions.Options;

using Simsa.Core.Services;
using Simsa.Model;

public class GenericItemService<TItem> : IGenericItemService<TItem>
    where TItem : IHasId<Guid>
{
    private readonly ApiHttpClient apiClient;

    private readonly EndpointConfig<TItem> endpointConfig;

    public GenericItemService(ApiHttpClient apiClient, IOptions<EndpointConfig<TItem>> options)
    {
        this.apiClient = apiClient;
        this.endpointConfig = options.Value;
    }

    private string Endpoint => this.endpointConfig.ApiEndpoint;

    public async ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PostAsync(this.Endpoint, item, cancellationToken);

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.DeleteAsync(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<TItem[]>(this.Endpoint, cancellationToken) ?? [];

    public async ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<TItem>(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PutAsync(this.BuildIdEndpoint(item.Id), item, cancellationToken);

    private string BuildIdEndpoint(Guid id) => string.Format($"{this.Endpoint}/{{0}}", id);
}