using Microsoft.EntityFrameworkCore;
using PhoneBook.AnaClos.Dtos;
using PhoneBook.AnaClos.Models;
using PhoneBook.AnaClos.Validators;

namespace PhoneBook.AnaClos.Controllers;

public class ContactsController: IController
{
    ConsoleController _consoleController;
    DataBaseController _dataBaseController;
    CategoriesController _categoriesController;
    ContactValidator _validator = new();

    public ContactsController(ConsoleController consoleController, DataBaseController dataBaseController, CategoriesController categoriesController)
    {
        _consoleController = consoleController;
        _dataBaseController = dataBaseController;
        _categoriesController = categoriesController;
    }

    public void Add()
    {
        string email;
        string phoneNumber;

        Category category = _categoriesController.GetCategoryFromMenu("Contact Category");
        if (category == null)
        {
            return;
        }

        string name = _consoleController.GetString("Contact name");

        do
        {
            email = _consoleController.GetString("Contact Email - Format: example@domain.com");
        } while(!_validator.EmailValidator(email));

        do
        {
            phoneNumber = _consoleController.GetString("Contact Phone Number - Only numbers, 10-15 characters");
        } while (!_validator.PhoneValidator(phoneNumber));
        
        try
        {
            _dataBaseController.Add(new Contact { 
                Name = name, 
                PhoneNumber=phoneNumber, 
                Email=email,
                IdCategory=category.IdCategory,
                Category=category
            }) ;
            _dataBaseController.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _consoleController.MessageAndPressKey("Name is duplicated.", "red");
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
        }
    }

    public void Delete()
    {
        Contact contact = GetContactFromMenu("Select a contact to delete");
        if (contact == null)
        {
            return;
        }
        try
        {
            _dataBaseController.Remove(contact);
            _dataBaseController.SaveChanges();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
        }
    }

    public void Update()
    {
        string option;
        Contact contact = GetContactFromMenu("Select a contact to update");
        if (contact == null)
        {
            return;
        }
        List<string> updateOptions = new List<string> { "Update Name", "Update Category", "Update Email", "Update Phone", "Exit Update"};
        do
        {
            option = _consoleController.Menu("What would you like to do?", "blue", updateOptions);
            switch (option)
            {
                case "Update Name":
                    UpdateName(contact);
                    break;
                case "Update Category":
                    UpdateCategory(contact);                    
                    break;
                case "Update Email":
                    UpdateEmail(contact);                    
                    break;
                case "Update Phone":
                    UpdatePhone(contact);                    
                    break;
                case "Exit Update":
                    break;
            }

        } while (option != "Exit Update");            
    }

    public void View()
    {
        Contact contact = GetContactFromMenu("Select a contact to view details");
        if (contact == null)
        {
            return;
        }

        string[] columns = { "Property", "Value" };

        var recordContacts = ContactToProperties(contact);
        _consoleController.ShowTable("Contact", columns, recordContacts);
        _consoleController.PressKey("Press a key to continue.");
    }

    public void ViewAll()
    {
        string[] columns = { "Id", "Name" };
        var contacts = _dataBaseController.Contacts.ToList<Contact>();
        if (contacts.Count == 0)
        {
            _consoleController.MessageAndPressKey("There is no Contact to select.", "Red");
            return;
        }
        var recordContacts = ContactToRecord(contacts);
        _consoleController.ShowTable("Contacts", columns, recordContacts);
        _consoleController.PressKey("Press a key to continue.");
    }

    public Contact GetContactsFromMenu(string title)
    {
        var contacts = _dataBaseController.Contacts.Include(n => n.Category).ToList<Contact>();

        List<string> stringContacts = ContactToString(contacts);
        stringContacts.Add("Exit Menu");

        string name = _consoleController.Menu(title, "blue", stringContacts);
        if (name == "Exit Menu")
        {
            return null;
        }
        var contact = _dataBaseController.Contacts.SingleOrDefault(x => x.Name == name);
        return contact;
    }

    public List<string> ContactToString(List<Contact> contacts)
    {
        var tableRecord = new List<string>();

        foreach (var contact in contacts)
        {
            tableRecord.Add(contact.Name);
        }
        return tableRecord;
    }

    public List<RecordDto> ContactToRecord(List<Contact> contacts)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var contact in contacts)
        {
            var record = new RecordDto { Column1 = contact.Id.ToString(), Column2 = contact.Name };
            tableRecord.Add(record);
        }
        return tableRecord;
    }
   
    public List<RecordDto> ContactToProperties(Contact contact)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var property in contact.GetType().GetProperties())
        {
            if (property.GetValue(contact) != null)
            {
                string value="";
                
                if (property.Name == "Category")
                {
                    value = contact.Category.Name;
                }
                else 
                {
                    value = property.GetValue(contact).ToString();
                }
                if (property.Name != "IdCategory")
                {
                    var record = new RecordDto { Column1 = property.Name, Column2 = value };
                    tableRecord.Add(record);
                }                    
            }
        }
        return tableRecord;
    }

    public Contact GetContactFromMenu(string title)
    {
        var contacts = _dataBaseController.Contacts.Include(n => n.Category).ToList<Contact>();
        if (contacts.Count == 0)
        {
            _consoleController.MessageAndPressKey("There is no Contact to select.", "Red");
            return null;
        }
        List<string> stringContacts = ContactToString(contacts);
        stringContacts.Add("Exit Menu");

        string name = _consoleController.Menu(title, "blue", stringContacts);
        if (name == "Exit Menu")
        {
            return null;
        }
        var contact = _dataBaseController.Contacts.SingleOrDefault(x => x.Name == name);
        return contact;
    }

    private void UpdateName(Contact contact)
    {
        string contactName = contact.Name;
        string newName = _consoleController.GetString($"Name: (0 to keep the value {contact.Name})");
        if (newName != "0")
        {
            contact.Name = newName;
        }

        try
        {
            _dataBaseController.Update(contact);
            _dataBaseController.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _consoleController.MessageAndPressKey("Name is duplicated.", "red");
            contact.Name = contactName;
            _dataBaseController.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
            _dataBaseController.ChangeTracker.Clear();
        }
    }

    private void UpdateCategory(Contact contact)
    {
        Category newCategory = _categoriesController.GetCategoryFromMenu("Contact Category");
        if (newCategory == null)
        {
            return;
        }
        contact.Category = newCategory;
        contact.IdCategory = newCategory.IdCategory;
        try
        {
            _dataBaseController.Update(contact);
            _dataBaseController.SaveChanges();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
        }        
    }

    private void UpdateEmail(Contact contact)
    {
        string newEmail;
        do
        {
            newEmail = _consoleController.GetString($"Contact Email - Format: example@domain.com (0 to keep the value {contact.Email})");
        } while (newEmail != "0" && !_validator.EmailValidator(newEmail));
        if (newEmail != "0")
        {
            contact.Email = newEmail;
        }
    }

    private void UpdatePhone(Contact contact)
    {
        string newPhoneNumber;
        do
        {
            newPhoneNumber = _consoleController.GetString($"Contact Phone Number - Only numbers, 10-15 characters (0 to keep the value {contact.PhoneNumber})");
        } while (newPhoneNumber != "0" && !_validator.PhoneValidator(newPhoneNumber));

        if (newPhoneNumber != "0")
        {
            contact.PhoneNumber = newPhoneNumber;
        }

        try
        {
            _dataBaseController.Update(contact);
            _dataBaseController.SaveChanges();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
        }
    }
}