using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Phonebook.Controllers;
using Console.Phonebook.Data;
using Console.Phonebook.Services;
using Spectre.Console;

namespace Console.Phonebook.View;

internal class UserInterface
{
    private readonly ContactService _contactService;

    public UserInterface(ContactService contactService)
    {
        _contactService = contactService;
    }

    internal void MainMenu()
    {
        var mainMenuOptions = EnumToDisplayNames<MainMenuOptions>();
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What do you want to do next?")
                .AddChoices(mainMenuOptions.Keys)
                .UseConverter(option => mainMenuOptions[option]));

            switch (choice)
            {
                case MainMenuOptions.CurrentContacts:
                case MainMenuOptions.AddContact:
                case MainMenuOptions.About:
                default:
                    return;
            }
        }
    }

    internal static void ContactMenu()
    {
        var contactMenuOptions = EnumToDisplayNames<ContactMenuOptions>();
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ContactMenuOptions>()
                .Title("What do you want to do next?")
                .AddChoices(contactMenuOptions.Keys)
                .UseConverter(option => contactMenuOptions[option]));

            switch (choice)
            {
                case ContactMenuOptions.ViewFull:
                case ContactMenuOptions.Edit:
                case ContactMenuOptions.Delete:
                default:
                    return;
            }
        }
    }

    static Dictionary<TEnum, string> EnumToDisplayNames<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                value => value,
                value => SplitCamelCase(value.ToString())
            );
    }

    internal static string SplitCamelCase(string input)
    {
        return string.Join(" ", System.Text.RegularExpressions.Regex
            .Split(input, @"(?<!^)(?=[A-Z])"));
    }
}

