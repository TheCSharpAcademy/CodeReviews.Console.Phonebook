using Spectre.Console;

namespace PhoneBook.Doc415;

internal class CountrySelection
{
    static string[] countryList = { "ar", "be", "bg", "bs", "de", "el", "en", "es", "fa", "fi", "fr", "hr", "hu", "hy", "id", "it", "iw", "ja", "ko", "nl", "pl", "pt", "ro", "ru", "sq", "sr", "sv", "th", "tr", "uk", "vl", "zh", "zh_Hant" };
    static public string CountryCode;
    static public void InitDefaultCountry()
    {
        if (!File.Exists("CountryCode.txt"))
        {
            using StreamWriter writer = new("CountryCode.txt");
            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                .Title("Please select default country")
                                .AddChoices(countryList));
            writer.WriteLine(selection.ToUpper()+"\n");
            CountryCode = selection.ToUpper();
        } 
        else
        {
            using StreamReader reader = new("CountryCode.txt");
            CountryCode = reader.ReadLine();
        }
    }
}
