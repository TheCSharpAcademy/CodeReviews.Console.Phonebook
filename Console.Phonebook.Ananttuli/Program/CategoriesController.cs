using Microsoft.EntityFrameworkCore;
using Program.Categories;
using Program.Contacts;
using Program.ContactsCategories;
using Spectre.Console;

namespace Program;

public class CategoriesController
{
    public static async Task ListCategories(PhonebookContext db)
    {
        var categories = await db.Categories
            .Include(c => c.ContactCategories)
            .ThenInclude(c => c.Contact)
            .ToListAsync();

        var table = new Spectre.Console.Table();

        table.AddColumns(["Id", "Name", "Contacts"]);

        foreach (var category in categories)
        {
            var id = category.CategoryId.ToString();
            var name = category.Name;
            string contacts = string.Join(
                ", ",
                category.ContactCategories
                    .Select(contactCategory => contactCategory.Contact.Name) ?? []
            );

            table.AddRow([id, name, contacts]);
        }

        table.ShowRowSeparators = true;
        AnsiConsole.Write(table);
    }

    public static async Task<List<Category>> MultiSelectCategories(PhonebookContext db, List<Category>? existingCategories = null)
    {
        var categories = await db.Categories
            .Include(c => c.ContactCategories)
            .ThenInclude(c => c.Contact)
            .ToListAsync();

        var multiSelectPrompt = new MultiSelectionPrompt<Category>()
                .Title("Choose categories")
                .NotRequired()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a category, " +
                    "[green]<enter>[/] to accept)[/]"
                )
                .AddChoices(categories);


        foreach (var cat in existingCategories ?? [])
        {
            multiSelectPrompt.Select(cat);
        }

        return AnsiConsole.Prompt(
            multiSelectPrompt
        );
    }


    public static int? PromptForCategoryId()
    {
        string categoryIdInput = AnsiConsole.Prompt(
            new TextPrompt<string>(
                "Category ID? [grey](Leave empty to cancel)[/]"
            )
            .Validate(input => int.TryParse(input, out int res))
            .AllowEmpty()
            .DefaultValue("")
        );

        if (categoryIdInput.Trim().Equals("") || categoryIdInput == null)
        {
            return null;
        }

        return int.Parse(categoryIdInput);
    }

    public static async Task<Category?> FindCategoryById(PhonebookContext db, int categoryId)
    {
        var existingCategory = await db.Categories.FindAsync(categoryId);

        if (existingCategory == null)
        {
            AnsiConsole.MarkupLine($"[red]Could not find ID {categoryId}[/]");
            return null;
        }

        return existingCategory;
    }

    private static Category PromptCategoryInput(Category? existingCategory = null)
    {
        string name = AnsiConsole.Ask<string>("Name", existingCategory?.Name ?? "");

        List<ContactCategory> contactCategories = [];

        return new Category
        {
            Name = name,
            ContactCategories = contactCategories
        };
    }

    public static async Task CreateCategory(PhonebookContext db)
    {
        Category categoryInput = PromptCategoryInput();

        db.Add(categoryInput);
        await db.SaveChangesAsync();
    }

    public static async Task UpdateCategory(PhonebookContext db, int contactId)
    {
        Category? existingCategory = await FindCategoryById(db, contactId);

        if (existingCategory == null)
        {
            return;
        }

        Category categoryInput = PromptCategoryInput(existingCategory);


        existingCategory.Name = categoryInput.Name ?? existingCategory.Name;
        existingCategory.ContactCategories.AddRange(categoryInput.ContactCategories);

        db.Update(existingCategory);
        await db.SaveChangesAsync();
    }

    public async static Task EditCategory(PhonebookContext db)
    {
        await ListCategories(db);

        AnsiConsole.MarkupLine("Edit category");
        int? categoryId = PromptForCategoryId();

        if (!categoryId.HasValue || categoryId == null)
        {
            return;
        }

        await UpdateCategory(db, categoryId.Value);
    }

    public async static Task DeleteCategory(PhonebookContext db)
    {
        await ListCategories(db);

        AnsiConsole.MarkupLine("Delete category");
        int? categoryId = PromptForCategoryId();

        if (!categoryId.HasValue || categoryId == null)
        {
            return;
        }

        var existingCategory = await FindCategoryById(db, categoryId.Value);

        if (existingCategory == null)
        {
            return;
        }

        db.Categories.Remove(existingCategory);
        await db.SaveChangesAsync();

    }
}