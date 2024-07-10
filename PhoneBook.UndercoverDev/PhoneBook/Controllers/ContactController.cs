using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class ContactController
    {
        public static void Add(Contact contact)
        {
            using var context = new ContactContext();
            context.Add(contact);
            context.SaveChanges();
        }
    }
}