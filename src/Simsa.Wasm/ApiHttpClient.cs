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
        if (httpResponseMessage?.IsSuccessStatusCode ?? false)
        {
            var result = await httpResponseMessage.Content.ReadFromJsonAsync<T>(cancellationToken);
            return result;
        }

        return default;
    }

    public async Task<bool> PostAsync<T>(string endpoint, T item, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PostAsync(endpoint, JsonContent.Create(item), cancellationToken);
        return httpResponseMessage?.IsSuccessStatusCode ?? false;
    }

    public async Task<bool> PutAsync<T>(string endpoint, T item, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PutAsync(endpoint, JsonContent.Create(item), cancellationToken);
        return httpResponseMessage?.IsSuccessStatusCode ?? false;
    }
}