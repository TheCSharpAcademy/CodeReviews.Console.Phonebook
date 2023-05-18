using ConsoleTableExt;
using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    contact.PhoneNumber
                });
            }
            ConsoleTableBuilder.From(tableData).WithColumn("Name", "Phonenumber").ExportAndWriteLine();
        }
    }
}
