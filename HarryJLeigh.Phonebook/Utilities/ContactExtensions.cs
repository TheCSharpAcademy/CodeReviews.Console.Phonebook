using Phonebook.Models;

namespace Phonebook.Utilities;

public static class ContactExtensions
{
    internal static Contact GetContact()
    {
        string name = UserInputHelper.GetName("add");
        string email = UserInputHelper.GetEmail("add");
        string phone = UserInputHelper.GetPhoneNumber("add");
        string category = Util.Capitalise(UserInputHelper.GetCategory("add"));
        
        return new Contact { Name = name, Email = email, PhoneNumber = phone, Category = category };
    }
}