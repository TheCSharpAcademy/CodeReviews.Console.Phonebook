using ConsoleTableExt;

namespace Phonebook.Control;

internal class ViewContact
{
    public static void Menu(Contact contact)
    {
        Menu menu = new Menu
        {
            Titles = new List<string>
            {
                "New Contact"
            },
            Fields = new List<string>
            {
                "Name",
                "Phone",
                "Address",
                "City",
                "State",
                "Zip",
                "Email",
                "Group"
            },
            FieldString = {
                contact.Name,
                contact.Phone,
                contact.Address,
                contact.City,
                contact.State,
                contact.ZipCode.ToString(),
                contact.Email,
                contact.Group
            },
            Message = "1: Edit, 2: Delete, 3: Return"
        };

        bool run = true;

        while (run)
        {
            menu.Draw();
            Console.SetCursorPosition(1, menu.InputRow + 3);
            ConsoleKey input = Console.ReadKey().Key;

            if (input == ConsoleKey.D1 | input == ConsoleKey.NumPad1)
            {
                contact = EditContact.Menu(contact);
                menu.FieldString = new List<string>
                {
                    contact.Name,
                    contact.Phone,
                    contact.Address,
                    contact.City,
                    contact.State,
                    contact.ZipCode.ToString(),
                    contact.Email,
                    contact.Group
                };
                menu.Draw();
            }
            else if (input == ConsoleKey.D2 | input == ConsoleKey.NumPad2)
            {
                Controller.DeleteContact(contact);
                run = false;
            }
            else if (input == ConsoleKey.D3 | input == ConsoleKey.NumPad3)
            {
                Controller.UpdateContactDate(contact);
                return;
            }
        }
    }
}
