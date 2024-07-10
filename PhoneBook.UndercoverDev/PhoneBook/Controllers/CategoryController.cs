using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class CategoryController
    {
        public static void Add(Category category)
        {
            using var context = new ContactContext();
            context.Add(category);
            context.SaveChanges();
        }
    }
}