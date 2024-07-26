using Spectre.Console;

namespace Phonebook
{
    internal class Display
    {
        static public void MainMenu()
        {        
            SelectionPrompt<string> selectionPrompt = new();
            string[] menuPrompts =
            {
               "[green]Add contact[/]",             
               "[yellow]Show contact info[/]",
               "[purple]Update contact[/]",
               "[red]Delete contact[/]"
            };
            selectionPrompt.AddChoices(menuPrompts);
            selectionPrompt.Title("[yellow]Please choose an option to perform[/]");
            
            
            string mainMenuSelection = AnsiConsole.Prompt(selectionPrompt).ToString();

            switch (mainMenuSelection)
            {
                case "[green]Add contact[/]":
                    AddContact();
                    break;
                case "[yellow]Show contact info[/]":
                    int currentSelect = SelectContact();
                    ShowContact(currentSelect);
                    break;
                case "[purple]Update contact[/]":
                    currentSelect = SelectContact();
                    UpdateContact(currentSelect);
                    break;
                case "[red]Delete contact[/]":
                    currentSelect = SelectContact();
                    DeleteContact(currentSelect);
                    break;
            }
        }

        static public void AddContact()
        {
            using var db = new PhonebookContext();
            Contact contact = new();

            TextPrompt<string> nameText = new TextPrompt<string>("Input name of contact [blue] First and Last Name[/]");
            contact.Name = AnsiConsole.Prompt<string>(nameText);

            TextPrompt<string> emailText = new TextPrompt<string>("Input email address [blue] Use format youremail@email.com[/]");
            emailText.Validate(email =>
            {
                if (!email.Contains(".com"))
                {
                    return ValidationResult.Error("Please make sure the email contains .com");
                }
                else if (!email.Contains("@"))
                {
                    return ValidationResult.Error("Please make sure the email contains @");
                }
                else
                {
                    return ValidationResult.Success();
                }
            });
            contact.Email = AnsiConsole.Prompt<string> (emailText);

            TextPrompt<string> phoneText = new TextPrompt<string>("Input phone number of contact [blue] Use format ***-****[/]");
            phoneText.Validate(phoneNumber =>
            {
                if (!phoneNumber.Contains("-"))
                {
                    return ValidationResult.Error("Incorrect format, please try again");
                }
                else if (phoneNumber.Length != 8 )
                {
                    return ValidationResult.Error("Not enough numbers, please try again");
                }
                else
                {
                    return ValidationResult.Success();
                }
            });
            contact.PhoneNumber = AnsiConsole.Prompt<string>(phoneText);

            db.Add(contact);
            db.SaveChanges();
          
        }

        static public void ShowContact(int contactId)
        {
            using var db = new PhonebookContext();
            Contact currentContact = db.Contacts.Find(contactId);

            AnsiConsole.Clear();
            AnsiConsole.WriteLine(currentContact.Name);
            AnsiConsole.WriteLine(currentContact.Email);
            AnsiConsole.WriteLine(currentContact.PhoneNumber);
        }

        static public int SelectContact()
        {
            AnsiConsole.Clear();
            using var db = new PhonebookContext();
            var contactSelect = new Spectre.Console.SelectionPrompt<string>();

            foreach (Contact c in db.Contacts)
            {
                contactSelect.AddChoice(c.Name);
            }

            string contactName = AnsiConsole.Prompt<string>(contactSelect);

            int contactId = db.Contacts.First(c => c.Name == contactName).Id;
            return contactId;
        }

        static public void UpdateContact(int contactId)
        {
            using var db = new PhonebookContext();
            var contactDetailChoice = new SelectionPrompt<string>();
            Contact contact = db.Contacts.Find(contactId);

            contactDetailChoice.AddChoice("Name");
            contactDetailChoice.AddChoice("Email");
            contactDetailChoice.AddChoice("Phone Number");
            contactDetailChoice.Title("Please select which detail you would like to update");

            string detailSelect = AnsiConsole.Prompt<string>(contactDetailChoice);

            switch (detailSelect)
            {
                case "Name":
                    contact.Name = AnsiConsole.Ask<string>("Please write updated name.");
                    db.SaveChanges();
                    break;

                case "Email":
                    TextPrompt<string> emailText = new TextPrompt<string>("Input updated email address [blue] Use format youremail@email.com[/]");
                    emailText.Validate(email =>
                    {
                        if (!email.Contains(".com"))
                        {
                            return ValidationResult.Error("Please make sure the email contains .com");
                        }
                        else if (!email.Contains("@"))
                        {
                            return ValidationResult.Error("Please make sure the email contains @");
                        }
                        else
                        {
                            return ValidationResult.Success();
                        }
                    });
                    contact.Email = AnsiConsole.Prompt<string>(emailText);
                    db.SaveChanges();
                    break;

                case "Phone Number":
                    TextPrompt<string> phoneText = new TextPrompt<string>("Input updated phone number of contact [blue] Use format ***-****[/]");
                    phoneText.Validate(phoneNumber =>
                    {
                        if (!phoneNumber.Contains("-"))
                        {
                            return ValidationResult.Error("Incorrect format, please try again");
                        }
                        else if (phoneNumber.Length != 8)
                        {
                            return ValidationResult.Error("Not enough numbers, please try again");
                        }
                        else
                        {
                            return ValidationResult.Success();
                        }
                    });
                    db.SaveChanges();
                    break;
            }
            

        }

        static public void DeleteContact(int contactId)
        {
            using var db = new PhonebookContext();
            Contact contact = db.Contacts.Find(contactId);

            if (contact != null)
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
        }
    }
}
