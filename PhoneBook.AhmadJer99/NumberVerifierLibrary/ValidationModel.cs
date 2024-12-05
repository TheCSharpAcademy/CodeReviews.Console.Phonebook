using Newtonsoft.Json;

namespace NumberVerifierLibrary;

internal class ValidatedPhoneNumber
{
    [JsonProperty("valid")]
    public bool IsNumberValid { get; set; }

    [JsonProperty("local_format")]
    public string? LocalNumber { get; set; }

    [JsonProperty("country_prefix")]
    public string? CountryPreFix { get; set; }

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("line_type")]
    public string? LineType { get; set; }

    [JsonProperty("country_name")]
    public string? CountryName { get; set; }

}