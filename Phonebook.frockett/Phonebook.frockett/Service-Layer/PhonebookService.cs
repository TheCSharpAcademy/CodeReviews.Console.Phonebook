using Phonebook.frockett.DataLayer;
using Phonebook.frockett.DTOs;
using Phonebook.frockett.Models;
using Phonebook.frockett.Utility;
using System.Text.RegularExpressions;

namespace Phonebook.frockett.Service_Layer;

public class PhonebookService
{
    private readonly PhoneBookRepository repository;

    public PhonebookService(PhoneBookRepository phoneBookRepository)
    {
        repository = phoneBookRepository;
    }

    public void DeleteContact(ContactDTO contactDto)
    {
        Contact contactToDelete = ModelMapper.ToContactDomainModel(contactDto);
        repository.RemoveContactById(contactToDelete);
    }

    internal void AddContact(string name, string email, string phoneNumber)
    {
        Contact contactToAdd = ModelMapper.ToContactDomainModel(new ContactDTO { Name = name, Email = email, PhoneNumber = phoneNumber });
        repository.AddContact(contactToAdd);
    }

    internal void UpdateGroupName(string newName)
    {
        throw new NotImplementedException();
    }

    internal bool CheckForGroupName(string newName)
    {
        if (repository.GetGroupByName(newName) == null) return false;

        return true;
    }

    internal List<ContactGroupDTO> FetchGroupList()
    {
        List<ContactGroup> contactGroups = repository.GetAllGroups();
        List<ContactGroupDTO> groupsToReturn = new();

        foreach (ContactGroup contactGroup in contactGroups)
        {
            groupsToReturn.Add(ModelMapper.ToGroupDto(contactGroup));
        }

        return groupsToReturn;
    }

    internal void DeleteGroup(ContactGroupDTO groupToDelete, bool shouldDeleteContacts)
    {
        repository.DeleteContactGroup(groupToDelete.Id, shouldDeleteContacts);
    }

    internal void AddNewGroup(string groupName)
    {
        repository.AddContactGroup(groupName);
    }

    internal void UpdateContact(ContactDTO updatedContact)
    {
        Contact contactToUpdate = ModelMapper.ToContactDomainModel(updatedContact);

        repository.UpdateContactInfo(contactToUpdate);
    }

    internal void AddContactToGroup(ContactGroupDTO groupAddedTo, int contactId)
    {
        int groupId = groupAddedTo.Id;
        repository.AddContactToGroup(contactId, groupId);
    }

    internal void RemoveContactFromGroup(ContactGroupDTO contactGroup, int contactId)
    {
        int groupId = contactGroup.Id;
        repository.RemoveContactFromGroup(contactId, groupId);
    }

    internal List<ContactDTO> FetchContactList()
    {
        List<Contact> contacts = repository.GetAllContacts();
        List<ContactDTO> contactsToReturn = new List<ContactDTO>();

        foreach (Contact contact in contacts)
        {
            contactsToReturn.Add(ModelMapper.ToContactDto(contact));
        }

        return contactsToReturn;
    }
}
