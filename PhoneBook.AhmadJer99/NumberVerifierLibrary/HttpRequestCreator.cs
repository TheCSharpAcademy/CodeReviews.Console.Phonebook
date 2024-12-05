using System.Configuration;

namespace NumberVerifierLibrary;

public class HttpRequestCreator
{
    public HttpClient CreateHttpClient()
    {
        return new HttpClient();
    }

    public string LoadApiKey()
    {
        return ConfigurationManager.AppSettings.Get("NumVerifyApiKey") ?? "";
    }

    public string LoadHttpValidationConnectionString()
    {
        return ConfigurationManager.AppSettings.Get("NumberValidationHttpEndpoint") ?? "";
    }
}