using Phonebook.frockett.DTOs;
using Phonebook.frockett.Models;

namespace Phonebook.frockett.Utility;

public class ModelMapper
{
    public ContactDTO ToContactDto(Contact contact) 
    {
        return new ContactDTO()
        {
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            ContactGroupName = contact.ContactGroup.Name
        };
    }

    public Contact ToDomainModel(ContactDTO contactDto)
    {
        return new Contact()
        {
            Name = contactDto.Name,
            Email = contactDto.Email,
            PhoneNumber = contactDto.PhoneNumber
            //ContactGroup = contactDto.ContactGroupName
            // TODO get contact group by searching contact group name from service layer
        };
    }

    public ContactGroupDTO ToGroupDto(ContactGroup contactGroup)
    {
        ContactGroupDTO contactGroupDTO = new ContactGroupDTO();
        contactGroupDTO.Name = contactGroup.Name;

        foreach(Contact contact in contactGroup.Contacts)
        {
            ContactDTO contactToAdd = ToContactDto(contact);
            contactGroupDTO.Contacts.Add(contactToAdd);
        }

        return contactGroupDTO;
    }

    public ContactGroup ToGroupDomainModel(ContactGroupDTO contactGroupDto)
    {
        ContactGroup contactGroup = new ContactGroup();
        contactGroup.Name = contactGroupDto.Name;

        foreach(ContactDTO contact in contactGroupDto.Contacts)
        {
            Contact contactToAdd = ToDomainModel(contact);
            contactGroup.Contacts.Add(contactToAdd);
        }
        // TODO make sure to get contact group info in service layer by searching by name

        return contactGroup;
    }
}
