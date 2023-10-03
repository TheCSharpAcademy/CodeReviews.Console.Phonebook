namespace Phonebook.Forser
{
    internal class MainMenuController
    {
        private readonly PhonebookContext db = new PhonebookContext();
        internal void MainMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                AnsiConsole.Clear();
                Helper.RenderTitle("Main Menu");
                int selectedOption = AnsiConsole.Prompt(DrawMenu()).Id;

                switch (selectedOption)
                {
                    case 0:
                        ListContacts();
                        break;
                    case 1:
                        AddContact();
                        break;
                    case 2:
                        EditContact();
                        break;
                    case 3:
                        DeleteContact();
                        break;
                    case -1:
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    default:
                        AnsiConsole.WriteLine("Not a valid option, select an option from the Main Menu");
                        break;
                }
            }
        }
        private void ListContacts()
        {
            AnsiConsole.Clear();
            Helper.RenderTitle("List of contacts");
            var contacts = db.Contacts
                .OrderBy(c => c.Id);

            Table table = new Table();
            table.Expand();
            table.AddColumns("Name", "Email", "Phonenumber");

            foreach(var contact in contacts)
            {
                table.AddRow($"{contact.Name}", $"{contact.Email}", $"{contact.PhoneNumber}");
            }
            AnsiConsole.Write(table);

            AnsiConsole.WriteLine("Press any key to return to main menu");
            Console.ReadLine();
        }
        private void DeleteContact()
        {
            AnsiConsole.Clear();
            Helper.RenderTitle("Delete a contact");
            var contacts = db.Contacts
                .OrderBy(c => c.Id);

            int selectedContact = AnsiConsole.Prompt(DrawContacts(contacts)).Id;

            if (selectedContact != 0)
            {
                db.Contacts.Remove(db.Contacts.Find(selectedContact));
                db.SaveChanges();
                AnsiConsole.WriteLine($"Contact has been deleted, press any key to return to main menu");
            }
            else
            {
                AnsiConsole.WriteLine($"Failed to delete contact with ID: {selectedContact}, press any key to return to main menu");
            }
            Console.ReadLine();
        }
        private void EditContact()
        {
            bool contactEdited = false;

            AnsiConsole.Clear();
            Helper.RenderTitle("Edit a contact");
            var contacts = db.Contacts
                .OrderBy(c => c.Id);

            int selectedContact = AnsiConsole.Prompt(DrawContacts(contacts)).Id;

            if (selectedContact != 0)
            {
                contactEdited = EditContactById(contacts.Where(c => c.Id == selectedContact).Single());
            }

            if (contactEdited)
            {
                AnsiConsole.WriteLine("Contact has been updated");
            }
            else
            {
                AnsiConsole.WriteLine($"Could not updated contact {contacts.Where(c => c.Id == selectedContact).Single()}");
            }
            AnsiConsole.WriteLine("Press any key to return to main menu");
            Console.ReadLine();
        }
        private bool EditContactById(Contact contact)
        {
            bool anyChanges = false;
            AnsiConsole.Clear();
            Helper.RenderTitle($"Editing the contact : {contact.Name}");
            string personEmail, personPhone;
            string personName = AnsiConsole.Ask<string>("Enter a the name of contact:", contact.Name);

            do
            {
                personEmail = AnsiConsole.Ask<string>("Enter the email of the contact:", contact.Email);
                if (!Validator.ValidateEmail(personEmail))
                {
                    AnsiConsole.WriteLine("Invalid email address, try again");
                }
            } while (!Validator.ValidateEmail(personEmail));

            do
            {
                personPhone = AnsiConsole.Ask<string>("Enter the phonenumber of the contact:", contact.PhoneNumber);
                if (!Validator.ValidatePhoneNumber(personPhone))
                {
                    AnsiConsole.WriteLine("Invalid phonenumber, Please enter a phonenumber with digits only. Try again");
                }
            } while (!Validator.ValidatePhoneNumber(personPhone));

            if (personName != contact.Name)
            {
                contact.Name = personName;
                anyChanges = true;
            }
            if (personEmail != contact.Email && Validator.ValidateEmail(contact.Email))
            {
                contact.Email = personEmail;
                anyChanges = true;
            }
            if (personPhone != contact.PhoneNumber && Validator.ValidatePhoneNumber(contact.PhoneNumber))
            {
                contact.PhoneNumber = personPhone;
                anyChanges = true;
            }

            if (anyChanges)
            {
                db.Contacts.Update(contact);
                db.SaveChanges();
            }

            return anyChanges;
        }
        private void AddContact()
        {
            AnsiConsole.Clear();
            Helper.RenderTitle("Add new contact");
            string personName = AnsiConsole.Ask<string>("Enter the name of the contact:");
            string personEmail, personPhone;

            do
            {
                personEmail = AnsiConsole.Ask<string>("Enter the email of the contact:");
                if (!Validator.ValidateEmail(personEmail))
                {
                    AnsiConsole.WriteLine("Invalid email address, try again");
                }
            } while (!Validator.ValidateEmail(personEmail));

            do
            {
                personPhone = AnsiConsole.Ask<string>("Enter the phonenumber of the contact:");
                if (!Validator.ValidatePhoneNumber(personPhone))
                {
                    AnsiConsole.WriteLine("Invalid phonenumber, Please enter a phonenumber with digits only. Try again");
                }
            } while (!Validator.ValidatePhoneNumber(personPhone));

            db.Add(new Contact(personName, personEmail, personPhone));
            int result = db.SaveChanges();

            if (result > 0)
            {
                AnsiConsole.WriteLine("Contact has been saved, press any key to return to main menu");
                Console.ReadLine();
            }
        }
        private static SelectionPrompt<Menu> DrawMenu()
        {
            SelectionPrompt<Menu> menu  = new()
            {
                HighlightStyle = Helper.HighLightStyle
            };

            menu.Title("Select an [B]option[/]");
            menu.AddChoices(new List<Menu>()
            {
                new() { Id = 0, Text = "List contacts" },
                new() { Id = 1, Text = "Add new contact" },
                new() { Id = 2, Text = "Edit an contact" },
                new() { Id = 3, Text = "Delete an contact" },
                new() { Id = -1, Text = "Exit application" }
            });

            return menu;
        }
        private static SelectionPrompt<Menu> DrawContacts(IQueryable<Contact> contacts)
        {
            SelectionPrompt<Menu> menu = new()
            {
                HighlightStyle = Helper.HighLightStyle
            };

            menu.Title("Select a [B]contact[/]");
            foreach(var contact in contacts.ToList())
            {
                menu.AddChoice(new() { Id = contact.Id, Text = contact.Name });
            }

            return menu;
        }
    }
}