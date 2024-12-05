using Spectre.Console;

namespace NumberVerifierLibrary;

public class ValidatePhoneNumber
{
    private readonly string? _countryCode;
    private readonly string? _phoneNumber;

    public ValidatePhoneNumber(string countryCode, string phoneNumber)
    {
        _countryCode = countryCode;
        _phoneNumber = phoneNumber;
    }

    public bool IsPhoneNumberFormatValid()
    {
        if (!double.TryParse(_phoneNumber, out double _))
        {
            AnsiConsole.MarkupLine("[red]Invalid Phone Number-No characters allowed in the phone number![/]");
            return false;
        }
        if (_phoneNumber.Length < 3) // the only allowed range for any phone number
        {
            AnsiConsole.MarkupLine("[red]Invalid Phone Number-The entered number is below the allowed length![/]");
            return false;
        }
        if (_phoneNumber.Length > 17)
        {
            AnsiConsole.MarkupLine("[red]Invalid Phone Number-The entered number exceeds the allowed length![/]");
            return false;
        }
        AnsiConsole.MarkupLine($"[green]Valid Phone Number Format!: {_phoneNumber}[/]");
        return true;
    }

    public async Task<bool> IsPhoneNumberValidAsync()
    {
        var phoneDetails = await LoadPhoneDetailsAsync();
        return phoneDetails.IsNumberValid;
    }

    private async Task<ValidatedPhoneNumber> LoadPhoneDetailsAsync()
    {
        HttpRequestCreator httpRequestCreator = new();
        var client = httpRequestCreator.CreateHttpClient();
        var apiKey = httpRequestCreator.LoadApiKey();
        var requestUri = httpRequestCreator.LoadHttpValidationConnectionString();
        var fullUri = requestUri + apiKey + "&number=" + _phoneNumber + "&country_code=" + _countryCode;

        return await ApiRequest.ProcessRequestAsync<ValidatedPhoneNumber>(client, fullUri);
    }
}