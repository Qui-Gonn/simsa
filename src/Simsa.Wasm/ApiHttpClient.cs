namespace Simsa.Wasm;

using System.Net.Http.Json;

public class ApiHttpClient
{
    private readonly HttpClient httpClient;

    public ApiHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.DeleteAsync(endpoint, cancellationToken);
        return httpResponseMessage?.IsSuccessStatusCode ?? false;
    }

    public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.GetAsync(endpoint, cancellationToken);
        return await ReturnObjectOrDefault<T>(httpResponseMessage, cancellationToken);
    }

    public async Task<T?> PostAsync<T>(string endpoint, T item, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PostAsync(endpoint, JsonContent.Create(item), cancellationToken);
        return await ReturnObjectOrDefault<T>(httpResponseMessage, cancellationToken);
    }

    public async Task<T?> PutAsync<T>(string endpoint, T item, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PutAsync(endpoint, JsonContent.Create(item), cancellationToken);
        return await ReturnObjectOrDefault<T>(httpResponseMessage, cancellationToken);
    }

    private static async Task<T?> ReturnObjectOrDefault<T>(HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        return httpResponseMessage is { IsSuccessStatusCode: true }
            ? await httpResponseMessage.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken)
            : default;
    }
}