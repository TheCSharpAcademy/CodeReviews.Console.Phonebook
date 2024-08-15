using Spectre.Console;
using PhoneBook.Arashi256.Controllers;
using PhoneBook.Arashi256.Classes;
using PhoneBook.Arashi256.Models;

namespace PhoneBook.Arashi256.Views
{
    internal class CategoryView
    {
        private const int RETURN_MAINVIEW_OPTION_NUM = 5;
        private Table _tblCategoryMenu;
        private CategoryController _categoryController;
        private string[] _menuOptions =
        {
            "Add a new category",
            "Update an existing category",
            "Delete an existing category",
            "List categories",
            "Return to main menu"
        };

        public CategoryView(CategoryController cc)
        {
            _categoryController = cc;
            if (cc == null) _categoryController = new CategoryController();
            _tblCategoryMenu = new Table();
            _tblCategoryMenu.AddColumn(new TableColumn("[steelblue]CHOICE[/]").Centered());
            _tblCategoryMenu.AddColumn(new TableColumn("[steelblue]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblCategoryMenu.AddRow($"[white]{i + 1}[/]", $"[aqua]{_menuOptions[i]}[/]");
            }
            _tblCategoryMenu.Alignment(Justify.Center);
        }

        public void DisplayViewMenu()
        {
            int selectedValue = 0;
            do
            {
                AnsiConsole.Write(new Text("\nCONTACT CATEGORIES").Centered());
                AnsiConsole.Write(_tblCategoryMenu);
                selectedValue = CommonUI.SelectNumberInRangeInput($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessCategoryMenu(selectedValue);
            } while (selectedValue != RETURN_MAINVIEW_OPTION_NUM);
        }

        private void ProcessCategoryMenu(int option)
        {
            AnsiConsole.Markup($"[lightslategrey]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    // Add new category.
                    AddNewCategory();
                    break;
                case 2:
                    // Update an existing category.
                    UpdateExistingCategory();
                    break;
                case 3:
                    // Delete existing category.
                    DeleteExistingCategory();
                    break;
                case 4:
                    // List categories.
                    ListCategories();
                    break;
            }
        }

        private void AddNewCategory()
        {
            bool isOkay = false;
            do
            {
                string? name = CommonUI.GetStringInput("Enter the name for the category: ");
                if (name == null) return;
                CategoryDto category = new()
                {
                    Id = null,
                    DisplayId = 1,
                    Name = name,
                };
                DisplayCategory(category);
                if (AnsiConsole.Confirm($"Are you happy to add this category?"))
                {
                    if (!_categoryController.CheckCategoryDuplicate(category.Name))
                    {
                        if (_categoryController.AddCategory(category))
                            AnsiConsole.MarkupLine("[green]New category added.[/]");
                        else
                            AnsiConsole.MarkupLine("[red]Error adding new category.[/]");
                        isOkay = true;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[yellow]This category '{category.Name}' is a duplicate. Try a different name.[/]");
                        isOkay = false;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]Re-enter the category name.[/]");
                    isOkay = false;
                }
            } while (!isOkay);
            CommonUI.Pause("grey53");
        }

        public void DisplayCategory(CategoryDto c)
        {
            if (c == null) return;
            Table tblCategory = new Table();
            tblCategory.AddColumn(new TableColumn($"[cyan]Id[/]").LeftAligned());
            tblCategory.AddColumn(new TableColumn($"[white]{c.DisplayId}[/]").LeftAligned());
            tblCategory.AddRow($"[cyan]Name[/]", $"[white]{c.Name}[/]");
            tblCategory.Alignment(Justify.Center);
            AnsiConsole.Write(tblCategory);
        }

        private void ListCategories()
        {
            List<CategoryDto> categories = _categoryController.GetCategories();
            DisplayCategories(categories);
            CommonUI.Pause("grey53");
        }

        public void DisplayCategories(List<CategoryDto> c)
        {
            if (c == null) return;
            if (c.Count == 0)
                AnsiConsole.MarkupLine("[red]\nThere are no categories to display!\n[/]");
            else
            {
                Table tblCategories = new Table();
                tblCategories.AddColumn(new TableColumn($"[cyan]Number[/]").LeftAligned());
                tblCategories.AddColumn(new TableColumn($"[cyan]Name[/]").LeftAligned());
                foreach (CategoryDto category in c)
                {
                    tblCategories.AddRow($"[white]{category.DisplayId}[/]", $"[white]{category.Name}[/]");
                }
                tblCategories.Alignment(Justify.Center);
                AnsiConsole.Write(tblCategories);
            }
        }

        private void UpdateExistingCategory()
        {
            bool isOkay = false;
            List<CategoryDto> categories = _categoryController.GetCategories();
            if (categories.Count > 0)
            {
                do
                {
                    DisplayCategories(categories);
                    int selection = CommonUI.SelectNumberInRangeInput("(Enter '0' to cancel)\nPlease select a category to edit: ", 0, categories.Count);
                    if (selection > 0)
                    {
                        CategoryDto category = categories[selection - 1];
                        DisplayCategory(category);
                        if (AnsiConsole.Confirm($"Are you sure you want edit this category?"))
                        {
                            string? name = CommonUI.GetStringInput("Enter the name for the contact: ");
                            if (name == null) return;
                            if (category.Name != name) category.Name = name;
                            if (!_categoryController.CheckCategoryDuplicate(category.Name))
                            {
                                if (_categoryController.UpdateCategory(category))
                                {
                                    AnsiConsole.MarkupLine("[green]Category updated.[/]");
                                }
                                else
                                    AnsiConsole.MarkupLine("[red]Error updating category.[/]");
                                isOkay = true;
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"[yellow]This category '{category.Name}' is a duplicate. Try a different name.[/]");
                                isOkay = false;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                        isOkay = true;
                    }
                } while (!isOkay);
            }
            CommonUI.Pause("grey53");
        }

        private void DeleteExistingCategory()
        {
            List<CategoryDto> categories = _categoryController.GetCategories();
            DisplayCategories(categories);
            if (categories.Count > 0)
            {
                int selection = CommonUI.SelectNumberInRangeInput("(Enter '0' to cancel)\nPlease select a category to delete: ", 0, categories.Count);
                if (selection > 0)
                {
                    CategoryDto category = categories[selection - 1];
                    DisplayCategory(category);
                    if (AnsiConsole.Confirm($"Are you sure you want delete this category?"))
                    {
                        // Check for empty category.
                        if (!_categoryController.CheckCategoryPopulated(category.Id))
                        {
                            if (_categoryController.DeleteCategory(category))
                                AnsiConsole.MarkupLine("[green]Category deleted.[/]");
                            else
                                AnsiConsole.MarkupLine("[red]Error deleting category. Please try again.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]This category still has contacts associated with it. Update your contacts to use a different category before trying again.[/]");
                        }
                    }
                    else
                        return;
                }
                else
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
            }
            CommonUI.Pause("grey53");
        }
    }
}
