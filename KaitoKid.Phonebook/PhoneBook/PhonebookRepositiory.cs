using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using Spectre.Console;
using System.Data;

namespace PhoneBook
{
    internal class PhonebookRepositiory
    {
        PhonebookContext _context = new();

        public void AddContact(string name, string number, string email, string category)
        {
            int maxNumber = _context.Contact.Max(c => (int?) c.TempId) ?? 0;
            
            Contact contact = new Contact
            {
                Name = name,
                Email = email,
                PhoneNumber = number,
                Category = category == null ? "General": category.Substring(0, 1).ToUpper() + category.Substring(1).ToLower(),
                TempId = maxNumber + 1
            };
             
            _context.Add(contact);
            _context.SaveChanges();
            AnsiConsole.Markup("[blue]Contact added[/]\n\n");
        }

        internal void ViewContacts(string category = "null")
        {
            var entities = category == "null" ? _context.Contact.ToList() : _context.Contact.Where(c => c.Category == category).ToList(); ;

            if (entities.Count == 0)
            {
                AnsiConsole.Markup("[red]Table is empty[/]\n\n");
                return;
            }

            Table table = new Table();
            table.AddColumn("[gold1 bold]Id[/]");
            table.AddColumn("[gold1 bold]Name[/]");
            table.AddColumn("[gold1 bold]Contact Number[/]");
            table.AddColumn("[gold1 bold]Email ID[/]");
            AnsiConsole.Markup("[blue]Phone Book[/]\n");
            foreach (var entity in entities)
            {
                string id = $"[seagreen1]{entity.TempId}[/]"; 
                string name = $"[seagreen1]{entity.Name}[/]";
                string email = $"[seagreen1]{entity.Email}[/]";
                string number = $"[seagreen1]{entity.PhoneNumber}[/]";

                table.AddRow(
                    id, name, number, email
                    );
            }

            AnsiConsole.Write(table);
            Console.WriteLine("\n");

        }

        public void DeleteContact(int id)
        {
            var entity = _context.Contact.FirstOrDefault(x => x.TempId == id);
          
            _context.Remove(entity);
            _context.SaveChanges();

            var rowsToBeUpdated = _context.Contact.Where(x => x.TempId > id).ToList();

            foreach (var row in rowsToBeUpdated)
            {
                row.TempId -= 1;
            }
            
            _context.SaveChanges();

            AnsiConsole.Markup("[blue]Contact deleted[/]\n\n");

            ViewContacts();
        }

        public void UpdateContacts(int id, string updateValue, string updateColumn)
        {
            var entity = _context.Contact.FirstOrDefault(x=> x.TempId == id);

            if (updateColumn == "Name")
            {
                entity.Name = updateValue;
            }
            else if (updateColumn == "PhoneNumber")
            {
                entity.PhoneNumber = updateValue;
            }
            else if (updateColumn == "Email")
            {
                entity.Email = updateValue;
            }
            else
            {
                entity.Category = updateValue;
            }

            _context.Update(entity);
            _context.SaveChanges();

            AnsiConsole.Markup($"[blue]Contact {updateColumn} is updated[/]\n\n");

            ViewContacts();

        }

        internal bool CheckIdExists(int id)
        {
            var entity = _context.Contact.FirstOrDefault(x => x.TempId == id);
            if (entity == null)
            {
                return false;
            }
            return true;
        }

        internal void GetCurrentValue(int id, string updateColumn)
        {
            var entity = _context.Contact
                    .Where(c => c.TempId == id)
                    .Select(c => EF.Property<string>(c, updateColumn))
                    .FirstOrDefault();

            Console.WriteLine("Current value: " + entity);
        }

        internal List<(string, int)> GetCategories()
        {
            var entities = _context.Contact.
                            GroupBy(c => c.Category).
                            Select(g => new
                            {
                                Category = g.Key,
                                Count = g.Count()
                            }).ToList();
           
            return entities.Select(e => (e.Category, e.Count)).ToList();
        }

        internal string GetEmail(int id)
        {
            var entity = _context.Contact.Where(c => c.TempId == id).Select(c => c.Email).FirstOrDefault();
            return entity.ToString();
        }

        internal string GetNumber(int id)
        {
            return _context.Contact.Where(c => c.TempId == id).Select(c => c.PhoneNumber).FirstOrDefault();
        }
    }
}
