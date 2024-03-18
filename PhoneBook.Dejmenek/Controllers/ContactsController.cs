﻿using PhoneBook.Dejmenek.Data.Repositories;
using PhoneBook.Dejmenek.Helpers;
using PhoneBook.Dejmenek.Models;
using PhoneBook.Dejmenek.Services;
using Spectre.Console;

namespace PhoneBook.Dejmenek.Controllers;

public class ContactsController
{
    private readonly ContactsRepository _contactsRepository;
    private readonly CategoriesRepository _categoriesRepository;
    private readonly UserInteractionService _userInteractionService;
    private readonly SendEmailService _sendEmailService;

    public ContactsController(
        ContactsRepository contactsRepository, CategoriesRepository categoriesRepository,
        UserInteractionService userInteractionService, SendEmailService sendEmailService
    )
    {
        _contactsRepository = contactsRepository;
        _categoriesRepository = categoriesRepository;
        _userInteractionService = userInteractionService;
        _sendEmailService = sendEmailService;
    }

    public void AddContact()
    {
        string name = _userInteractionService.GetContactName();
        int? categoryId = null;
        string? email = null;

        if (_userInteractionService.GetConfirmation("Do you want to assign a category to this contact?"))
        {
            var categories = _categoriesRepository.GetCategories();

            if (categories.Count == 0)
            {
                AnsiConsole.MarkupLine("There are currently no categories available.");
            }
            else
            {
                string? categoryName = _userInteractionService.GetCategory(Mapper.ToCategoryDTOs(categories));
                categoryId = categories.Single(c => c.Name == categoryName).Id;
            }
        }

        if (_userInteractionService.GetConfirmation("Do you want to assign an email to this contact?"))
        {
            email = _userInteractionService.GetEmail();
        }

        string phoneNumber = _userInteractionService.GetPhoneNumber();

        while (_contactsRepository.PhoneNumberExists(phoneNumber))
        {
            phoneNumber = _userInteractionService.GetPhoneNumber();
        }

        Contact contact = new Contact
        {
            CategoryId = categoryId,
            Name = name,
            PhoneNumber = phoneNumber,
            Email = email,
        };

        _contactsRepository.AddContact(contact);
    }

    public void UpdateContact()
    {
        Contact contactToUpdate = GetContact();

        if (ContactNotExist(contactToUpdate))
        {
            AnsiConsole.MarkupLine("There are currently no contacts available. Please create some contacts before updating a contact.");
            return;
        }

        if (_userInteractionService.GetConfirmation("Do you want to update the name? (y/n)"))
        {
            contactToUpdate.Name = _userInteractionService.GetContactName();
        }

        if (_userInteractionService.GetConfirmation("Do you want to update the category? (y/n)"))
        {
            List<Category> categories = _categoriesRepository.GetCategories();
            if (categories.Count == 0)
            {
                AnsiConsole.MarkupLine("There are currently no categories available.");
            }
            else
            {
                string categoryName = _userInteractionService.GetCategory(Mapper.ToCategoryDTOs(categories));
                Category chosenCategory = categories.Single(c => c.Name == categoryName);

                contactToUpdate.CategoryId = chosenCategory.Id;
            }
        }

        if (_userInteractionService.GetConfirmation("Do you want to update the phone number? (y/n)"))
        {
            string phoneNumber = _userInteractionService.GetPhoneNumber();

            while (_contactsRepository.PhoneNumberExists(phoneNumber))
            {
                phoneNumber = _userInteractionService.GetPhoneNumber();
            }

            contactToUpdate.PhoneNumber = phoneNumber;
        }

        if (_userInteractionService.GetConfirmation("Do you want to update the email? (y/n)"))
        {
            contactToUpdate.Email = _userInteractionService.GetEmail();
        }

        _contactsRepository.UpdateContact(contactToUpdate);
    }

    public void DeleteContact()
    {
        Contact contactToDelete = GetContact();

        if (ContactNotExist(contactToDelete))
        {
            AnsiConsole.MarkupLine("There are currently no contacts available. Please create some contacts before deleting a contact.");
            return;
        }

        _contactsRepository.DeleteContact(contactToDelete.Id);
    }

    public List<ContactDTO> GetContactsByCategory()
    {
        List<Category> categories = _categoriesRepository.GetCategories();

        if (categories.Count == 0)
        {
            return [];
        }

        string categoryName = _userInteractionService.GetCategory(Mapper.ToCategoryDTOs(categories));
        int categoryId = categories.Single(cat => cat.Name == categoryName).Id;

        var contacts = _contactsRepository.GetContactsByCategory(categoryId);

        if (contacts.Count == 0)
        {
            return [];
        }

        return Mapper.ToContactDTOs(contacts);
    }

    public Contact GetContact()
    {
        List<Contact> contacts = _contactsRepository.GetAllContacts();

        if (contacts.Count == 0)
        {
            return new Contact { Id = 0, Name = "No Contact Found" };
        }

        string contactPhoneNumber = _userInteractionService.GetContact(Mapper.ToContactDTOs(contacts));
        Contact chosenContact = contacts.Single(c => c.PhoneNumber == contactPhoneNumber);

        return chosenContact;
    }

    public List<ContactDTO> GetAllContacts()
    {
        var contacts = _contactsRepository.GetAllContacts();

        if (contacts.Count == 0)
        {
            return [];
        }

        return Mapper.ToContactDTOs(contacts);
    }

    public void SendEmail()
    {
        Contact contact = GetContact();

        if (contact.Email is not null)
        {
            _sendEmailService.Setup();
            _sendEmailService.SendEmail(contact.Email);
        }
        else if (ContactNotExist(contact))
        {
            AnsiConsole.MarkupLine("There are currently no contacts available. Please create some contacts before sending an email to one of them.");
        }
        else
        {
            AnsiConsole.MarkupLine("Chosen contact doesn't have setted email");
        }
    }

    public bool ContactNotExist(Contact contact)
    {
        return contact.Id == 0 && contact.Name == "No Contact Found";
    }
}
