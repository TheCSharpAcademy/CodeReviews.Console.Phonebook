using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Phonebook;

internal enum MainMenuOptions
{
    CurrentContacts,
    AddContact,
    About,
    Quit
}

internal enum ContactMenuOptions
{
    ViewFull,
    Edit,
    Delete,
    Back
}

internal enum EditMenuOptions
{
    Name,
    Email,
    PhoneNumber
}

internal enum SendMessageOptions
{
    Email,
    SMS
}
