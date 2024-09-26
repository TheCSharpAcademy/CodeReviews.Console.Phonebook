using Spectre.Console;

namespace Phonebook.Library;

public class Validator
{
    public static string ValidateName()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Enter a name: "));
    }

    public static string ValidateEmail()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Enter an e-mail address: ")
            .Validate((e) => e.Contains("@") switch
            {
                true => ValidationResult.Success(),
                false => ValidationResult.Error(@"E-mail address needs to contain a @ sign.")
            }));
    }

    public static int ValidatePhoneNumber()
    {
        return AnsiConsole.Prompt(new TextPrompt<int>("Enter a phone number: ")
            .InvalidChoiceMessage("Entry must contain only numbers and be 9 digits long.")
            .Validate((n) => n.ToString().Length switch
            { 
                9 => ValidationResult.Success(),
                _ => ValidationResult.Error("Entry must contain only numbers and be 9 digits long.")
            }));
    }

    public static string ValidateCategory()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("[[Optional]] Enter a category")
            .DefaultValue("Other"));
    }

    public static string ValidateString(string type)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>($"Enter a {type}: "));
    }
}
