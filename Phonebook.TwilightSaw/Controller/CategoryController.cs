using Microsoft.EntityFrameworkCore;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.Controller;

public class CategoryController(AppDbContext context)
{
    public void Create(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
    }

    public List<Category> Read()
    {
        return context.Categories.Select(t => t).ToList();
    }

    public void Delete(Category category)
    {
        context.Categories.Where(c => c.Id.Equals(category.Id)).AsNoTracking().ExecuteDelete();
        context.SaveChanges();
    }

    public void Update(Category category, string name)
    {
        context.Categories.Where(c => c.Id.Equals(category.Id)).AsNoTracking().
            ExecuteUpdate(s => s.SetProperty(c => c.Name, c => name));
        context.SaveChanges();
        context.Entry(category).Reload();
    }
}