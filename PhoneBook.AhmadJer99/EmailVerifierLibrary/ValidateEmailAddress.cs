using Spectre.Console;

namespace EmailVerifierLibrary;

public class ValidateEmailAddress
{
    private string? _emailAddress;
    private ValidatedEmailAddress? _emailDetails;

    public ValidateEmailAddress(string emailAddress)
    {
        _emailAddress = emailAddress;
    }

    public async Task InitializeAsync()
    {
        await LoadEmailDetailsAsync();
    }

    public bool IsEmailAddressFormatValid()
    {
        return _emailDetails?.IsFormatValid ?? false;
    }

    public double GetEmailAddressValidConfidenceScore()
    {
        return _emailDetails?.Confidence ?? 0.0;
    }

    public bool EmailCanRecieveMail()
    {
        return _emailDetails?.MxCheck ?? false;
    }

    public async Task<string> CheckEmailPossibleTypoFixAsync()
    {
        if (string.IsNullOrWhiteSpace(_emailDetails?.CorrectedEmail))
            return "";

        AnsiConsole.Markup($"[yellow]Did you mean -{_emailDetails.CorrectedEmail}? [/]");
        while (true)
        {
            var userChoice = AnsiConsole.Ask<string>("(Y/N)");

            if (userChoice.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                _emailAddress = _emailDetails.CorrectedEmail;
                await LoadEmailDetailsAsync();
                return _emailDetails.CorrectedEmail;
            }
            if (userChoice.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                return "";
            AnsiConsole.MarkupLine("[red]Invalid Choice![/]");
        }
    }

    private async Task LoadEmailDetailsAsync()
    {
        HttpRequestCreator httpRequestCreator = new();
        var client = httpRequestCreator.CreateHttpClient();
        var apiKey = httpRequestCreator.LoadApiKey();
        var requestUri = httpRequestCreator.LoadHttpValidationConnectionString();
        var fullUri = requestUri + apiKey + "&email=" + _emailAddress;

        _emailDetails = await ApiRequest.ProcessRequestAsync<ValidatedEmailAddress>(client, fullUri);
    }
}