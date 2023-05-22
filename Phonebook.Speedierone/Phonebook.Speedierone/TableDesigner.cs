using ConsoleTableExt;
using Phonebook.Models;

namespace Phonebook
{
    internal class TableDesigner
    {
        public static void DisplayTable(List<Contact> contacts)
        {
            var tableData = new List<List<Object>>();
            foreach (Contact contact in contacts)
            {
                tableData.Add(new List<Object>
                {
                    contact.Name,
                    contact.PhoneNumber,
                    contact.Email,
                    contact.ContactGroup
                });
            }
            ConsoleTableBuilder.From(tableData).WithColumn("Name", "Phonenumber", "Email", "ContactGroup").ExportAndWriteLine();
        }
    }
}
