using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.tonyissa.UI;

public static class UserInputHandler
{
    public static string GetName()
    {
        return AnsiConsole.Ask<string>("Which name do you want to lookup?");
    }

    public static int GetContactPosFromList(List<Contact> contacts)
    {
        try
        {
            var input = AnsiConsole.Ask<int>("Enter the ID for the contact you want to select");

            if (input < 1 || input > contacts.Count)
            {
                throw new ArgumentOutOfRangeException(null, "Argument out of range. Try again");
            }

            return input - 1;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            return GetContactPosFromList(contacts);
        }
    }
}