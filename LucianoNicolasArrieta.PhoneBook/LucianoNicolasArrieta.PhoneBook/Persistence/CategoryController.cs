using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucianoNicolasArrieta.PhoneBook.Persistence
{
    public class CategoryController
    {
        ContactContext db = new ContactContext();
        TableVisualization tableVisualization = new TableVisualization();
        UserInput userInput = new UserInput();

        public void Delete()
        {
            List<Category> categories = GetCategories();

            List<int> existingIds = new List<int>();
            foreach (Category aux_category in categories)
            {
                existingIds.Add(aux_category.CategoryID);
            }
            int id = userInput.ValidIdInput(existingIds, "category", "delete");

            var category = db.Categories.FirstOrDefault(x => x.CategoryID == id);
            db.Remove(category);
            db.SaveChanges();

            AnsiConsole.Markup("[green]Category deleted from the Phone Book succesfully![/] Prees any key to continue.");
            Console.ReadKey();
        }

        public void Insert(Category category)
        {
            db.Add(category);
            db.SaveChanges();

            AnsiConsole.Markup("[green]Category added to the Phone Book succesfully![/] Prees any key to continue.");
            Console.ReadKey();
        }

        public void Update()
        {
            List<Category> categories = GetCategories();

            List<int> existingIds = new List<int>();
            foreach (Category aux_category in categories)
            {
                existingIds.Add(aux_category.CategoryID);
            }
            int id = userInput.ValidIdInput(existingIds, "category", "update");

            var category = db.Categories.FirstOrDefault(x => x.CategoryID == id);

            Category updatedCategory = userInput.CategoryInput();
            category.Name = updatedCategory.Name;

            db.Update(category);
            db.SaveChanges();

            AnsiConsole.Markup("[green]Category updated succesfully![/] Prees any key to continue.");
            Console.ReadKey();
        }

        public void ViewCategories()
        {
            tableVisualization.PrintCategories(GetCategories());
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();

            return categories;
        }

        public Category GetCategoryByID()
        {
            List<Category> categories = GetCategories();

            List<int> existingIds = new List<int>();
            foreach (Category aux_category in categories)
            {
                existingIds.Add(aux_category.CategoryID);
            }
            int id = userInput.ValidIdInput(existingIds, "category", "select");

            Category category = db.Categories.FirstOrDefault(x => x.CategoryID == id);

            return category;
        }
    }
}
