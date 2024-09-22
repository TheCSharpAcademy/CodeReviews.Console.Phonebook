namespace PhoneBookLibrary;

public static class ContactMapper
{
    public static ContactDto? MapToDto(Contact contact)
    {
        if(contact == null) return null;
        return new ContactDto
        {
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
        };
    }
}