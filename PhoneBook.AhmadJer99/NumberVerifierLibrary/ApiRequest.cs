using Newtonsoft.Json;

namespace NumberVerifierLibrary;

public static class ApiRequest
{
    public static async Task<T> ProcessRequestAsync<T>(HttpClient client, string requestUri)
    {
        await using var stream =
            await client.GetStreamAsync(requestUri);

        using var streamReader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(streamReader);

        var serializer = new JsonSerializer();
        var result = serializer.Deserialize<T>(jsonReader);

        return result;
    }
}