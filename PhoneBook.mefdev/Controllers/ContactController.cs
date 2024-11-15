using PhoneBook.mefdev.Models;
using PhoneBook.mefdev.Service;
using PhoneBook.mefdev.Shared.Interfaces;
using Spectre.Console;

namespace PhoneBook.mefdev.Controllers
{
	internal class ContactController: BaseController, IBaseController
	{
        private readonly ContactService _contactService;
        private readonly CategoryService _categoryService;

        public ContactController(ContactService contactService, CategoryService categoryService)
        {
            _contactService = contactService;
            _categoryService = categoryService;
        }

        public void AddItem()
        {
            RenderCustomLine("DodgerBlue1", "CREATE A CONTACT");
  
            var categories = _categoryService.GetAllCategories();
            if (categories != null)
            {
                var categoryName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
               .Title("Select a [green]category[/] to add")
               .PageSize(10)
               .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
               .AddChoices(categories.Select(c => c.Name)));
                var category = _categoryService.GetCategoryByName(categoryName);
                if (category == null)
                {
                    AnsiConsole.MarkupLine("[green]A category is not found[/]");
                    return;
                }
                DisplayMessage("The category is ready", "green");
                string name = GetName();
                string email = GetEmail();
                string phone = GetPhoneNumber();

                _contactService.AddContact(new Contact
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    CategoryId = category.Id,
                });
                DisplayMessage("Inserting a new contact...", "green");
            }
        }

        public void DeleteItem()
        {
            RenderCustomLine("DodgerBlue1", "DELETE A CONTACT");

            var contacts = _contactService.GetContacts();
            if (contacts == null)
            {
                DisplayMessage("Contacts are not found or Empty", "red");
                return;
            }
            var contactName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select a [red]contact[/] to remove")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(contacts.Select(c => c.Name)));
            if (ConfirmDeletion(contactName))
            {
                var isDeleted = _contactService.DeleteContactByName(contactName);
                if (!isDeleted)
                {
                    DisplayMessage("A contact cannot be deleted", "red");
                    return;
                }
                DisplayMessage("Deleting a contact...", "green");
            }
        }

        public void UpdateItem()
        {
            RenderCustomLine("DodgerBlue1", "Update A CONTACT");

            var contacts = _contactService.GetContacts();
            if (contacts == null)
            {
                DisplayMessage("contacts are not found or Empty", "red");
                return;
            }
            var contactName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
               .Title("Select a [red]contact[/] to update")
               .PageSize(10)
               .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
               .AddChoices(contacts.Select(c => c.Name)));
            var contact = _contactService.GetContactByName(contactName);
            if (contact == null)
            {
                DisplayMessage("contact is not found", "red");
                return;
            }
            string name = GetName(contact.Name);
            string email = GetEmail(contact.Email);
            string phone = GetPhoneNumber(contact.Phone);

            _contactService.UpdateContact(contact, new Contact
            {
                Name = name,
                Email = email,
                Phone = phone,
            });
            DisplayMessage("Updating a new contact...", "green");
        }

        public void ViewItem()
        {
            RenderCustomLine("DodgerBlue1", "CONTACT");
            var contacts = _contactService.GetContacts();
            if (contacts == null)
            {
                DisplayMessage("Contacts are not found or Empty", "red");
                return;
            }
            var contactName = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a [green]Contact[/] to view")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .AddChoices(contacts.Select(c => c.Name)));
            var contact = _contactService.GetContactByName(contactName);
            if (contact == null)
            {
                DisplayMessage("A contact is not found", "red");
                return;
            }
            DisplayMessage("Contact is ready", "green");
            DisplayItemTable(contact);

        }

        public void ViewItems()
        {
            RenderCustomLine("DodgerBlue1", "CONTACTS");
           
            var contacts = _contactService.GetContacts();
            if (contacts == null)
            {
                DisplayMessage("Contacts are not found or Empty", "red");
                return;
            }
            DisplayMessage("contacts are ready", "green");
            DisplayAllItems(contacts);
        }
    }
}