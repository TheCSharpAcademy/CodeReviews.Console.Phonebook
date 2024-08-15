using Microsoft.Identity.Client;
using PhoneBook.Arashi256.Classes;
using PhoneBook.Arashi256.Controllers;
using PhoneBook.Arashi256.Models;
using Spectre.Console;

namespace PhoneBook.Arashi256.Views
{
    internal class MainView
    {
        private const int QUIT_APPLICATION_OPTION_NUM = 8;
        private Table _tblMainMenu;
        private string _appTitle = "PHONEBOOK";
        private FigletText? _figletAppTitle;
        private string[] _menuOptions =
        {
            "Add a new contact",
            "Update an existing contact",
            "Delete an existing contact",
            "List all contacts",
            "List contacts by category",
            "Category management menu",
            "Send Email",
            "Exit application"
        };
        private ContactController _contactController;
        private CategoryController _categoryController;
        private CategoryView _categoryView;

        public MainView()
        {
            _figletAppTitle = new FigletText(_appTitle);
            _figletAppTitle.Centered();
            _figletAppTitle.Color = Color.SteelBlue1_1;
            _tblMainMenu = new Table();
            _tblMainMenu.AddColumn(new TableColumn("[steelblue]CHOICE[/]").Centered());
            _tblMainMenu.AddColumn(new TableColumn("[steelblue]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblMainMenu.AddRow($"[white]{i + 1}[/]", $"[aqua]{_menuOptions[i]}[/]");
            }
            _tblMainMenu.Alignment(Justify.Center);
            _categoryController = new CategoryController();
            _contactController = new ContactController(_categoryController);
            _categoryView = new CategoryView(_categoryController);
        }

        public void DisplayView()
        {
            int selectedValue = 0;
            do
            {
                Console.Clear();
                AnsiConsole.Write(_figletAppTitle);
                AnsiConsole.Write(new Text("M A I N   M E N U").Centered());
                AnsiConsole.Write(_tblMainMenu);
                selectedValue = CommonUI.SelectNumberInRangeInput($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessMainMenu(selectedValue);
            } while (selectedValue != QUIT_APPLICATION_OPTION_NUM);
            AnsiConsole.MarkupLine("[lime]Goodbye![/]");
        }

        private void ProcessMainMenu(int option)
        {
            AnsiConsole.Markup($"[lightslategrey]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    // Add new contact.
                    AddNewContact();
                    break;
                case 2:
                    // Update an existing contact.
                    UpdateExistingContact();
                    break;
                case 3:
                    // Delete existing contact.
                    DeleteExistingContact();
                    break;
                case 4:
                    // List all contacts.
                    DisplayAllContacts();
                    CommonUI.Pause("grey53");
                    break;
                case 5:
                    // Display contacts by category.
                    DisplayContactsByCategory();
                    break;
                case 6:
                    // Categories sub-menu.
                    _categoryView.DisplayViewMenu();
                    break;
                case 7:
                    // Send email.
                    SendEmail();
                    break;
            }
        }

        private void AddNewContact()
        {
            bool isOkay = false;
            do
            {
                CategoryDto? category = GetCategoryInput();
                if (category == null) return;
                string? title = GetTitleInput();
                if (title == null) return;
                string? name = CommonUI.GetStringInput("Enter the name for the contact: ");
                if (name == null) return;
                string? email = GetEmailAddressInput("What is contact email address?: ");
                if (email == null) return;
                string? phoneNumber = GetTelephoneNumberInput();
                if (phoneNumber == null) return;             
                ContactDto contact = new()
                {
                    Id = null,
                    DisplayId = 1,
                    CategoryId = (int)category.Id,
                    Title = title,
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    CategoryName = category.Name,
                };
                DisplayContact(contact);
                if (AnsiConsole.Confirm($"Are you happy to add this contact?"))
                {
                    if (_contactController.AddContact(contact))
                        AnsiConsole.MarkupLine("[green]New contact added.[/]");
                    else
                        AnsiConsole.MarkupLine("[red]Error adding new contact.[/]");
                    isOkay = true;
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]Re-enter the contact details.[/]");
                    isOkay = false;
                }
            } while (!isOkay);  
            CommonUI.Pause("grey53");
        }

        private void DisplayContact(ContactDto c)
        {
            if (c == null) return;
            Table tblContact = new Table();
            tblContact.AddColumn(new TableColumn($"[cyan]Id[/]").LeftAligned());
            tblContact.AddColumn(new TableColumn($"[white]{c.DisplayId}[/]").LeftAligned());
            tblContact.AddRow($"[cyan]Category[/]", $"[white]{c.CategoryName.ToUpper()}[/]");
            tblContact.AddRow($"[cyan]Title[/]", $"[white]{c.Title.ToUpper()}[/]");
            tblContact.AddRow($"[cyan]Name[/]", $"[white]{c.Name}[/]");
            tblContact.AddRow($"[cyan]Phone[/]", $"[white]{c.PhoneNumber}[/]");
            tblContact.AddRow($"[cyan]Email[/]", $"[white]{c.Email}[/]");
            tblContact.Alignment(Justify.Center);
            AnsiConsole.Write(tblContact);
        }

        private void DisplayAllContacts()
        {
            List<ContactDto> contacts = _contactController.GetAllContacts();
            DisplayContacts(contacts);
        }

        private void DisplayContactsByCategory()
        {
            bool isOkay = false;
            List<CategoryDto> categories = _categoryController.GetCategories();
            if (categories.Count > 0)
            {
                do
                {
                    _categoryView.DisplayCategories(categories);
                    int selection = CommonUI.SelectNumberInRangeInput("(Enter '0' to cancel)\nPlease select a category for it's contacts: ", 0, categories.Count);
                    if (selection > 0)
                    {
                        CategoryDto category = categories[selection - 1];
                        List<ContactDto> contacts = _categoryController.GetContactsForCategory((int)category.Id);
                        DisplayContacts(contacts);
                        isOkay = true;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                        isOkay = true;
                    }
                } while (!isOkay);
            }
            CommonUI.Pause("grey53");
        }

        private void UpdateExistingContact()
        {
            List<ContactDto> contacts = _contactController.GetAllContacts();
            DisplayContacts(contacts);
            if (contacts.Count > 0)
            {
                int selection = CommonUI.SelectNumberInRangeInput("Please select a contact to edit: ", 0, contacts.Count);
                if (selection > 0)
                {
                    ContactDto contact = contacts[selection - 1];
                    DisplayContact(contact);
                    if (AnsiConsole.Confirm($"Are you sure you want to edit this contact?"))
                    {
                        CategoryDto? category = GetCategoryInput();
                        if (category == null) return;
                        string? title = GetTitleInput();
                        if (title == null) return;
                        string? name = CommonUI.GetStringInput("Enter the name for the contact: ");
                        if (name == null) return;
                        string? email = GetEmailAddressInput("What is contact email address?: ");
                        if (email == null) return;
                        string? phoneNumber = GetTelephoneNumberInput();
                        if (phoneNumber == null) return;
                        if (contact.CategoryId != category.Id) contact.CategoryId = (int)category.Id;
                        if (contact.CategoryName != category.Name) contact.CategoryName = category.Name;
                        if (contact.Title != title) contact.Title = title;
                        if (contact.Name != name) contact.Name = name;
                        if (contact.PhoneNumber != phoneNumber) contact.PhoneNumber = phoneNumber;
                        if (contact.Email != email) contact.Email = email;
                        if (_contactController.UpdateContact(contact))
                            AnsiConsole.MarkupLine("[green]Contact updated.[/]");
                        else
                            AnsiConsole.MarkupLine("[red]Error updating contact.[/]");
                    }
                    else
                        return;
                }
                else
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");              
            }
            CommonUI.Pause("grey53");
        }

        private void DeleteExistingContact()
        {
            List<ContactDto> contacts = _contactController.GetAllContacts();
            DisplayContacts(contacts);
            if (contacts.Count > 0)
            {
                int selection = CommonUI.SelectNumberInRangeInput("Please select a contact to delete: ", 0, contacts.Count);
                if (selection > 0)
                {
                    ContactDto contact = contacts[selection - 1];
                    DisplayContact(contact);
                    if (AnsiConsole.Confirm($"Are you sure you want to delete this contact?"))
                    {
                        if (_contactController.DeleteContact(contact))
                            AnsiConsole.MarkupLine("[green]Contact deleted.[/]");
                        else
                            AnsiConsole.MarkupLine("[red]Error deleting contact.[/]");
                    }
                    else
                        return;
                }
                else
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
            }
            CommonUI.Pause("grey53");
        }

        private void DisplayContacts(List<ContactDto> c)
        {
            if (c == null) return;
            if (c.Count == 0)
                AnsiConsole.MarkupLine("[red]\nThere are no contacts to display!\n[/]");
            else
            {
                Table tblContacts = new Table();
                tblContacts.AddColumn(new TableColumn($"[cyan]Id[/]").LeftAligned());
                tblContacts.AddColumn(new TableColumn($"[cyan]Category[/]").LeftAligned());
                tblContacts.AddColumn(new TableColumn($"[cyan]Title[/]").LeftAligned());
                tblContacts.AddColumn(new TableColumn($"[cyan]Name[/]").LeftAligned());
                tblContacts.AddColumn(new TableColumn($"[cyan]Phone Number[/]").LeftAligned());
                tblContacts.AddColumn(new TableColumn($"[cyan]Email[/]").LeftAligned());
                foreach (ContactDto contact in c)
                {
                    tblContacts.AddRow($"[white]{contact.DisplayId}[/]", $"[white]{contact.CategoryName.ToUpper()}[/]", $"[white]{contact.Title.ToUpper()}[/]", $"[white]{contact.Name}[/]", $"[white]{contact.PhoneNumber}[/]", $"[white]{contact.Email}[/]");
                }
                tblContacts.Alignment(Justify.Center);
                AnsiConsole.Write(tblContacts);
            }

        }

        private string? GetTelephoneNumberInput()
        {
            return CommonUI.GetValidatedStringInput("Enter the telephone number for the contact in the format 'XXXXX XXXXXX': ", @"^\d{5} \d{6}$");
        }

        private string? GetTitleInput()
        {
            string title = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                    .Title("Choose the contact title:")
                                    .PageSize(6)
                                    .AddChoices(new[] {"Mr", "Mrs", "Miss", "Ms", "Dr", "[yellow]CANCEL[/]"}));
            return title == "[yellow]CANCEL[/]" ? null : title;
        }

        private string? GetEmailAddressInput(string message)
        {
            return CommonUI.GetValidatedStringInput(message, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private CategoryDto? GetCategoryInput()
        {
            List<CategoryDto> categories = _categoryController.GetCategories();
            _categoryView.DisplayCategories(categories);
            int selection = CommonUI.SelectNumberInRangeInput("Select category to use for this contact: ", 0, categories.Count);
            if (selection == 0)
                return null;
            else
                return categories[selection - 1];
        }

        private void SendEmail()
        {
            Email email = new Email();
            if (email.CheckValidSettings())
            { 
                List<ContactDto> contacts = _contactController.GetAllContacts();
                DisplayContacts(contacts);
                if (contacts.Count > 0)
                {
                    int selection = CommonUI.SelectNumberInRangeInput("(Enter '0' to cancel)\nPlease select a contact to email: ", 0, contacts.Count);
                    if (selection > 0)
                    {
                        ContactDto contact = contacts[selection - 1];
                        DisplayContact(contact);
                        if (AnsiConsole.Confirm($"Are you sure you want to email this contact?"))
                        {
                            string? senderEmail = GetEmailAddressInput("Enter your email address to use as the sender address: ");
                            if (senderEmail == null) return;
                            string? senderName = CommonUI.GetStringInput("Please enter your name to use as the sender name: ");
                            if (senderName == null) return;
                            if (email.Init())
                            {
                                AnsiConsole.MarkupLine("[darkgreen]Email system ready.[/]\n[white]Enter '0' in any prompt to cancel operation.[/]");
                                string? subject = CommonUI.GetStringInput("What is the email subject?: ");
                                string? content = CommonUI.GetStringInput("What is the email message?: ");
                                if (AnsiConsole.Confirm("Confirm to send email? "))
                                {
                                    if (email.SendEmail(senderEmail, senderName, contact, subject, content))
                                        AnsiConsole.MarkupLine($"[green]Email to '{contact.Name}' successfully sent![/]");
                                    else
                                        AnsiConsole.MarkupLine($"[red]Email to '{contact.Name}' failed to send![/]");
                                }
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"[red]Email subsystem failed to initialise. Please check your SMTP settings in App.config.[/]");
                            }
                        }
                        else
                            return;
                    }
                    else
                        AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                }
            }
            else
                AnsiConsole.MarkupLine("[yellow]The Email subsystem cannot be used because the SMTP server details are missing or could not be loaded from App.config.[/]");
            CommonUI.Pause("grey53");
        }
    }
}
