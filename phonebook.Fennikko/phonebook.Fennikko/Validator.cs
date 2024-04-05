﻿using System.Text.RegularExpressions;
using Spectre.Console;

namespace phonebook.Fennikko;

public class Validator
{

    public static string GetPhoneNumberInput()
    {
        var phoneNumberInput = AnsiConsole.Prompt(
            new TextPrompt<string>("Contact's phone number including country code [green](Example 1321.456.7890)[/]: ")
                .PromptStyle("blue")
                .AllowEmpty());

        while (string.IsNullOrWhiteSpace(phoneNumberInput))
        {
            AnsiConsole.Write("Cannot be empty, please try again. Press any key to continue");
            Console.ReadKey();
            phoneNumberInput = AnsiConsole.Prompt(
                new TextPrompt<string>("Contact's phone number including country code [green](Example 1321.456.7890)[/]: ")
                    .PromptStyle("blue")
                    .AllowEmpty());
        }

        var editedPhoneNumber = Regex.Replace(phoneNumberInput, @"[^0-9]", "");
        var fullPhoneNumber = "+" + editedPhoneNumber;
        return fullPhoneNumber;
    }
}