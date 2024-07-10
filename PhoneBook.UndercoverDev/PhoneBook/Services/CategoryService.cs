using PhoneBook.Controllers;

namespace PhoneBook.Services
{
    public class CategoryService
    {
        public static void AddCategory()
        {
            var category = UserInteraction.UserInteractions.GetCategoryDetails();
            CategoryController.Add(category);
        }

        internal static void ViewCategories()
        {
            throw new NotImplementedException();
        }
    }
}