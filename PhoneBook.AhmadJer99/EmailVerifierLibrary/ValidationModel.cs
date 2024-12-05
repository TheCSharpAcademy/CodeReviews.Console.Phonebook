using Newtonsoft.Json;

namespace EmailVerifierLibrary;

internal class ValidatedEmailAddress
{
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("did_you_mean")]
    public string? CorrectedEmail { get; set; }

    [JsonProperty("format_valid")]
    public bool IsFormatValid { get; set; }

    [JsonProperty("mx_found")]
    public bool MxCheck { get; set; }

    [JsonProperty("score")]
    public double Confidence { get; set; }
}