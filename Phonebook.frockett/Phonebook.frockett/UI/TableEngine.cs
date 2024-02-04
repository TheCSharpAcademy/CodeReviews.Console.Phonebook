using Phonebook.frockett.DTOs;
using Spectre.Console;

namespace Phonebook.frockett.UI
{
    public class TableEngine
    {
        public void DisplayContact(ContactDTO contactToDisplay)
        {
            Table table = new Table();

            string? groupName;

            if (contactToDisplay.ContactGroupName != null)
            {
                groupName = contactToDisplay.ContactGroupName;
            }
            else
            {
                groupName = "N/A";
            }
            table.AddColumns(new string[] { "Name", "Phone Number", "Email", "Group" });
            table.AddRow(contactToDisplay.Name, contactToDisplay.PhoneNumber, contactToDisplay.Email, groupName);

            AnsiConsole.Write(table);
        }

    }
}
