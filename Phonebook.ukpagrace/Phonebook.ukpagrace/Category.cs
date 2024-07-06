using Phonebook.ukpagrace.Model;

namespace Phonebook.ukpagrace.Categories
{
    public class CategoryController
    {
        public void Create()
        {
            using var db = new ApplicationContext();
            if (!db.Category.Any(c => c.Name == "Friends"))
            {
                var friends = new Category()
                {
                    Name = "Friends"
                };
                db.Category.Add(friends);
            }

            if (!db.Category.Any(c => c.Name == "Family"))
            {
                var family = new Category()
                {
                    Name = "Family"
                };
                db.Category.Add(family);
            }

            if (!db.Category.Any(c => c.Name == "Work"))
            {
                var work = new Category()
                {
                    Name = "Work"
                };
                db.Category.Add(work);
            }
            db.SaveChanges();
        }

        
    }
}
