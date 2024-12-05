using System.Configuration;

namespace EmailVerifierLibrary;

public class HttpRequestCreator
{
    public HttpClient CreateHttpClient()
    {
        return new HttpClient();
    }

    public string LoadApiKey()
    {
        return ConfigurationManager.AppSettings.Get("EmailVerifyApiKey") ?? "";
    }

    public string LoadHttpValidationConnectionString()
    {
        return ConfigurationManager.AppSettings.Get("ValidationHttpEndpoint") ?? "";
    }
}