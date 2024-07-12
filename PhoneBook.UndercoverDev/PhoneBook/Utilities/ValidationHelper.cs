using PhoneBook.Models;

namespace PhoneBook.Utilities
{
    public class ValidationHelper
    {
        internal static bool CategoryExists(ContactContext context, string categoryName)
            => context.Categories.Any(c => c.Name.ToLower().Equals(categoryName.ToLower()));

        internal static bool ContactExists(ContactContext contactContext, string phoneNumber) => contactContext.Contacts.Any(c => c.PhoneNumber.Equals(phoneNumber));
    }
}