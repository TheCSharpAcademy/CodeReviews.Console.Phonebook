using PhoneBook.w0lvesvvv.Models;
using PhoneBook.w0lvesvvv.Persistance;
using PhoneBook.w0lvesvvv.Utils;
using System.Drawing.Text;

using (var _db = new DataContext())
{
    _db.Database.EnsureCreated();
}

do
{
    string option = DisplayMenu();

    switch (option)
    {
        case "1":
            ListContacts();
            break;
        case "2":
            AddContact();
            break;
        case "3":
            EditContact();
            break;
        case "4":
            DeleteContact();
            break;
        case "0":
            Environment.Exit(0);
            break;
    }
} while (true);

#region Menu
string DisplayMenu()
{
    ConsoleUtils.SetColor(ConsoleColor.Green);
    Console.WriteLine();
    Console.WriteLine("|==================|");
    Console.WriteLine("|    Phone Book    |");
    Console.WriteLine("|==================|");
    Console.WriteLine("|   1 - List       |");
    Console.WriteLine("|   2 - Add        |");
    Console.WriteLine("|   3 - Edit       |");
    Console.WriteLine("|   4 - Delete     |");
    Console.WriteLine("|   0 - Exit       |");
    Console.WriteLine("|==================|");

    Console.WriteLine();
    return UserInput.RequestString("Option selected: ");
}
#endregion

#region Contacts
void ListContacts()
{
    ConsoleUtils.SetColor(ConsoleColor.Yellow);
    Console.WriteLine();

    using (var _db = new DataContext())
    {
        List<Contact> listContacs = _db.Contacts.ToList();

        if (listContacs == null || !listContacs.Any())
        {
            ConsoleUtils.DisplayMessage("There aren't contacts yet. Try creating one first", messageColor: ConsoleColor.Red);
            return;
        }

        TableBuilder.BuildTable(listContacs);
    }
}

void AddContact()
{
    Contact contact = new();

    Console.WriteLine();
    string contactName = UserInput.RequestString("Introduce name: ");
    if (string.IsNullOrWhiteSpace(contactName)) return;
    contact.Name = contactName;

    ConsoleUtils.DisplayMessage("(Phone number format: '6XX XXX XXX' or '976 XXX XXX') ");
    string contactPhone = UserInput.RequestString("Introduce phone number: ");
    if (string.IsNullOrWhiteSpace(contactPhone)) return;
    if (!UserInputValidation.ValidatePhoneNumber(contactPhone))
    {
        Console.WriteLine();
        ConsoleUtils.DisplayMessage("Phone number format not valid.", messageColor: ConsoleColor.Red);
        return;
    }
    contact.PhoneNumber = contactPhone;


    using (var _db = new DataContext())
    {
        _db.Contacts.Add(contact);
        _db.SaveChanges();
    }
}

void EditContact()
{
    Contact contact;

    ListContacts();

    int? contactId = UserInput.RequestNumber("Introduce contact id: ");
    if (contactId == null) return;

    using (var _db = new DataContext())
    {
        contact = _db.Contacts.FirstOrDefault(x => x.Id == contactId) ?? new();
    }

    if (contact == null)
    {
        ConsoleUtils.DisplayMessage("Contact not found by Id", messageColor: ConsoleColor.Red);
        return;
    }

    string contactName = UserInput.RequestString("Introduce new contact name: ");
    if (string.IsNullOrWhiteSpace(contactName)) return;
    contact.Name = contactName;

    string contactPhone = UserInput.RequestString("Introduce new phone number: ");
    if (string.IsNullOrWhiteSpace(contactPhone)) return;
    if (!UserInputValidation.ValidatePhoneNumber(contactPhone))
    {
        Console.WriteLine();
        ConsoleUtils.DisplayMessage("Phone number format not valid.", messageColor: ConsoleColor.Red);
        return;
    }
    contact.PhoneNumber = contactPhone;

    using (var _db = new DataContext())
    {
        _db.Contacts.Update(contact);
        _db.SaveChanges();
    }
}

void DeleteContact()
{
    Contact contact;

    ListContacts();

    Console.WriteLine();
    int? contactId = UserInput.RequestNumber("Introduce contact id: ");
    if (contactId == null) return;

    using (var _db = new DataContext())
    {
        contact = _db.Contacts.FirstOrDefault(x => x.Id == contactId) ?? new();
    }

    if (contact == null)
    {
        ConsoleUtils.DisplayMessage("Contact not found by Id", messageColor: ConsoleColor.Red);
        return;
    }

    using (var _db = new DataContext())
    {
        _db.Contacts.Remove(contact);
        _db.SaveChanges();
    }

}
#endregion
