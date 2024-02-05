using Phonebook.frockett.DTOs;
using Phonebook.frockett.Models;

namespace Phonebook.frockett.Utility;

public class ModelMapper
{
    public static ContactDTO ToContactDto(Contact contact) 
    {
        string? groupName = null;
        if (contact.ContactGroup != null)
        {
            groupName = contact.ContactGroup.Name;
        }

        return new ContactDTO()
        {
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            ContactGroupName = groupName,
            Id = contact.ContactId
        };
    }

    public static Contact ToContactDomainModel(ContactDTO contactDto)
    {
        return new Contact()
        {
            Name = contactDto.Name,
            Email = contactDto.Email,
            PhoneNumber = contactDto.PhoneNumber,
            ContactId = contactDto.Id
            //ContactGroup = contactDto.ContactGroupName
            // TODO get contact group by searching contact group name from service layer
        };
    }

    public static ContactGroupDTO ToGroupDto(ContactGroup contactGroup)
    {
        ContactGroupDTO contactGroupDTO = new ContactGroupDTO();
        contactGroupDTO.Name = contactGroup.Name;
        contactGroupDTO.Id = contactGroup.ContactGroupId;

        if (contactGroup.Contacts != null)
        {
            foreach (Contact contact in contactGroup.Contacts)
            {
                ContactDTO contactToAdd = ToContactDto(contact);
                contactGroupDTO.Contacts.Add(contactToAdd);
            }
        }


        return contactGroupDTO;
    }

    public static ContactGroup ToGroupDomainModel(ContactGroupDTO contactGroupDto)
    {
        ContactGroup contactGroup = new ContactGroup();
        contactGroup.Name = contactGroupDto.Name;
        contactGroup.ContactGroupId = contactGroupDto.Id;

        foreach(ContactDTO contact in contactGroupDto.Contacts)
        {
            Contact contactToAdd = ToContactDomainModel(contact);
            contactGroup.Contacts.Add(contactToAdd);
        }
        // TODO make sure to get contact group info in service layer by searching by name

        return contactGroup;
    }
}
