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

        internal static List<Category> GetCategories()
        {
            using var context = new ContactContext();
            var categories = context.Categories.ToList();

            return categories;
        }

        internal static Category GetCategoryById(int id)
        {
            using var context = new ContactContext();
            var category = context.Categories.Single(c => c.CategoryId == id);
            return category;
        }
    }
}