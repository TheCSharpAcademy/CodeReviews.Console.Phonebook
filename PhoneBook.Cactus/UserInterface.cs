﻿using Spectre.Console;

namespace PhoneBook.Cactus;

public class UserInterface
{
    public static void ShowContacts(List<Contact> contacts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("PhoneNumber");

        foreach (Contact contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }

    public static void ShowContact(Contact contact)
    {
        var panel = new Panel($@"Id: {contact.Id}  Name: {contact.Name}  Email: {contact.Email}  PhoneNumber: {contact.PhoneNumber}");
        panel.Header = new PanelHeader("Contact Info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press any key to go back to Main Menu");
        Console.ReadLine();
        Console.Clear();
    }
}

