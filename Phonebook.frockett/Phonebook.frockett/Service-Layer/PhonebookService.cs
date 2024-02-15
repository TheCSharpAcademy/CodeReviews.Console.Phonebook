using Phonebook.frockett.DataLayer;
using Phonebook.frockett.DTOs;
using Phonebook.frockett.Models;
using Phonebook.frockett.Utility;

namespace Phonebook.frockett.Service_Layer;

public class PhonebookService
{
    private readonly PhoneBookRepository repository;

    public PhonebookService(PhoneBookRepository phoneBookRepository)
    {
        repository = phoneBookRepository;
    }

    public bool DeleteContact(ContactDTO contactDto)
    {
        Contact contactToDelete = ModelMapper.ToContactDomainModel(contactDto);

        return repository.RemoveContactById(contactToDelete);
    }

    internal bool AddContact(string name, string email, string phoneNumber)
    {
        Contact contactToAdd = ModelMapper.ToContactDomainModel(new ContactDTO { Name = name, Email = email, PhoneNumber = phoneNumber });

        if (repository.AddContact(contactToAdd) != null) //If the contact isn't null, that means a contact was created, so return true.
            return true;
        else
            return false;
    }

    internal bool UpdateGroupName(string newName, ContactGroupDTO groupToEdit)
    {
        return repository.UpdateContactGroupName(groupToEdit.Id, newName);
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

    internal bool DeleteGroup(ContactGroupDTO groupToDelete, bool shouldDeleteContacts)
    {
        return repository.DeleteContactGroup(groupToDelete.Id, shouldDeleteContacts);
    }

    internal bool AddNewGroup(string groupName)
    {
        if (repository.AddContactGroup(groupName) != null) return true;
        else return false;
    }

    internal bool UpdateContact(ContactDTO updatedContact)
    {
        Contact contactToUpdate = ModelMapper.ToContactDomainModel(updatedContact);

        if (repository.UpdateContactInfo(contactToUpdate) != null)
            return true;
        else 
            return false;
    }

    internal bool AddContactToGroup(ContactGroupDTO groupAddedTo, int contactId)
    {
        int groupId = groupAddedTo.Id;
        return repository.AddContactToGroup(contactId, groupId);
    }

    internal bool RemoveContactFromGroup(ContactGroupDTO contactGroup, int contactId)
    {
        ContactGroup groupToRemoveFrom = repository.GetGroupByName(contactGroup.Name);
        return repository.RemoveContactFromGroup(contactId, groupToRemoveFrom.ContactGroupId);
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
