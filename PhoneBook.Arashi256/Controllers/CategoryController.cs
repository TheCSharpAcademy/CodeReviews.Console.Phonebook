using PhoneBook.Arashi256.Data;
using PhoneBook.Arashi256.Models;

namespace PhoneBook.Arashi256.Controllers
{
    internal class CategoryController
    {
        private readonly DataContext _db = new DataContext();

        public bool AddCategory(CategoryDto c)
        {
            Category category = new Category()
            {
                Name = c.Name,
            };
            try
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem adding this category: '{ex.Message}'");
                return false;
            }
        }

        public List<CategoryDto> GetCategories()
        {
            List<CategoryDto> displayCategories = new List<CategoryDto>();
            List<Category> categories = _db.Categories.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                displayCategories.Add(new CategoryDto()
                {
                    Id = categories[i].Id,
                    DisplayId = i + 1,
                    Name = categories[i].Name,
                });
            }
            return displayCategories;
        }

        public bool DeleteCategory(CategoryDto c)
        {
            try
            {
                var category = _db.Categories.SingleOrDefault(x => x.Id == c.Id);
                if (category == null) return false;
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem deleting this category: '{ex.Message}'");
                return false;
            }
        }

        public bool UpdateCategory(CategoryDto c)
        {
            try
            {
                var category = _db.Categories.SingleOrDefault(x => x.Id == c.Id);
                if (category == null) return false;
                category.Name = c.Name;
                _db.Update(category);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: There was a problem updating this category: '{ex.Message}'");
                return false;
            }
        }

        public bool CheckCategoryDuplicate(string name)
        {
            Category? category = _db.Categories.SingleOrDefault(x => x.Name.ToLower() == name.ToLower());
            return category == null ? false : true;
        }

        public bool CheckCategoryPopulated(int? id)
        {
            return _db.Contacts.Any(c => c.CategoryId == id);
        }

        public List<ContactDto>? GetContactsForCategory(int categoryId)
        {
            // Get the category.
            Category category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return null;
            // Get contacts.
            var contacts = _db.Contacts
                          .Where(c => c.CategoryId == category.Id)
                          .Select(c => new ContactDto
                          {
                              Id = c.Id,
                              Title = c.Title,
                              Name = c.Name,
                              PhoneNumber = c.PhoneNumber,
                              Email = c.Email,
                              CategoryName = category.Name,
                          })
                          .ToList();
            // Map to ContactDto and assign DisplayId
            var contactDtos = contacts.Select((contact, index) => new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Title = contact.Title,
                CategoryName = category.Name,
                DisplayId = index + 1
            }).ToList();
            return contactDtos;
        }

        public Category? GetCategoryFromId(int categoryId)
        {
            Category? category = _db.Categories.Find(categoryId);
            return category;
        }
    }
}
