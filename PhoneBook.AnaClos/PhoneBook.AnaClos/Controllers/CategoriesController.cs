using Microsoft.EntityFrameworkCore;
using PhoneBook.AnaClos.Dtos;
using PhoneBook.AnaClos.Models;

namespace PhoneBook.AnaClos.Controllers;

public class CategoriesController : IController
{
    ConsoleController _consoleController;
    DataBaseController _dataBaseController;
    public CategoriesController(ConsoleController consoleController, DataBaseController dataBaseController)
    {
        _consoleController = consoleController;
        _dataBaseController = dataBaseController;
    }

    public void Add()
    {
        string name = _consoleController.GetString("Category's name");
        Category category = new Category { Name = name };
        try
        {
            _dataBaseController.Add(category);
            _dataBaseController.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _consoleController.MessageAndPressKey("Name is duplicated.", "red");
            _dataBaseController.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
            _dataBaseController.ChangeTracker.Clear();
        }
    }

    public void Delete()
    {
        Category category = GetCategoryFromMenu("Select a category to delete");
        if (category == null)
        {
            //_consoleController.MessageAndPressKey("There is no Category to delete.", "Red");
            return;
        }
        try
        {
            _dataBaseController.Remove(category);
            _dataBaseController.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _consoleController.MessageAndPressKey("Cannot be deleted. There are contacts with this category.", "red");
            _dataBaseController.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
        }
    }

    public void Update()
    {
        Category category = GetCategoryFromMenu("Select a category to update");
        if (category == null)
        {
            //_consoleController.MessageAndPressKey("There is no Category to update.", "Red");
            return;
        }
        string newName = _consoleController.GetString("New category's name");
        category.Name = newName;
        try
        {
            _dataBaseController.Update(category);
            _dataBaseController.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            _consoleController.MessageAndPressKey("Name is duplicated.", "red");
            _dataBaseController.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _consoleController.MessageAndPressKey(ex.Message.ToString(), "red");
            _dataBaseController.ChangeTracker.Clear();
        }
    }
        
    public void View()
    {
        Category category = GetCategoryFromMenu("Select a category to view details");
        if (category == null)
        {
            //_consoleController.MessageAndPressKey("There is no Category to view details.", "Red");
            return;
        }

        string[] columns = { "Property", "Value" };

        var recordCategories = CategoryToProperties(category);
        _consoleController.ShowTable("Category", columns, recordCategories);
        _consoleController.PressKey("Press a key to continue.");

    }

    public void ViewAll()
    {
        string[] columns = { "Id", "Name" };
        var categories = _dataBaseController.Categories.ToList<Category>();
        if (categories.Count == 0)
        {
            _consoleController.MessageAndPressKey("There is no Category to select.", "Red");
            return;
        }
        var recordCategories = CategoryToRecord(categories);
        _consoleController.ShowTable("Categories", columns, recordCategories);
        _consoleController.PressKey("Press a key to continue.");
    }

    public List<RecordDto> CategoryToRecord(List<Category> categories)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var category in categories)
        {
            var record = new RecordDto { Column1 = category.IdCategory.ToString(), Column2 = category.Name };
            tableRecord.Add(record);
        }
        return tableRecord;
    }

    public List<RecordDto> CategoryToProperties(Category category)
    {
        var tableRecord = new List<RecordDto>();
        foreach (var property in category.GetType().GetProperties())
        {
            if (property.GetValue(category) != null && property.GetValue(category)!="Contacts")
            {
                var record = new RecordDto { Column1 = property.Name, Column2 = property.GetValue(category).ToString() };
                tableRecord.Add(record);
            }
        }
        return tableRecord;
    }

    public List<string> CategoryToString(List<Category> categories)
    {
        var tableRecord = new List<string>();
        var records = categories.Select(c => c.Name).ToList();
        foreach (var category in categories)
        {
            var record = category.Name;
            tableRecord.Add(category.Name);
        }
        return tableRecord;
    }


    public string GetNameFromMenu(string title)
    {
        var categories = _dataBaseController.Categories.ToList<Category>();
        List<string> stringCategories = CategoryToString(categories);
        string name = _consoleController.Menu(title, "blue", stringCategories);
        return name;
    }

    public Category GetCategoryFromMenu(string title)
    {
        var categories = _dataBaseController.Categories.ToList<Category>();
        if(categories.Count==0)
        {
            _consoleController.MessageAndPressKey("There is no Category to select.", "Red");
            return null;
        }  
        List<string> stringCategories = CategoryToString(categories);
        stringCategories.Add("Exit Menu");

        string name = _consoleController.Menu(title, "blue", stringCategories);
        if (name == "Exit Menu")
        {
            return null;
        }
        var category = _dataBaseController.Categories.SingleOrDefault(x => x.Name == name);
        return category;
    }
}