using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class CategoryController
    {
        public static void Add(Category category)
        {
            using var context = new ContactContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }

        internal static void Delete(Category category)
        {
            using var context = new ContactContext();
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        internal static List<Category> GetCategories()
        {
            using var context = new ContactContext();
            var categories = context.Categories.ToList();

            return categories;
        }

        internal static void Update(Category category)
        {
            using var context = new ContactContext();
            context.Categories.Update(category);
            context.SaveChanges();
        }
    }
}