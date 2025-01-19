using Spectre.Console;
using Phonebook.yemiodetola.Models;

namespace Phonebook.yemiOdetola;

public class CategoryController
{

  private readonly ContactsRepository _contactsRepository;
  public CategoryController(ContactsRepository contactsRepository)
  {
    _contactsRepository = contactsRepository;
  }

  public async Task CreateCategory()
  {
    Category category = new Category();
    category.Name = AnsiConsole.Ask<string>("Enter category title");
    try
    {
      await _contactsRepository.CreateCategory(category);
      AnsiConsole.MarkupLine("[bold green]Category created successfully![/]");
    }
    catch (Exception e)
    {
      AnsiConsole.MarkupLine($"[red]Error creating category: {e.Message}[/]");
    }

  }

  public async Task GetAllCategories()
  {
    try
    {
      List<Category>? categories = await _contactsRepository.GetCategories();
      if (categories.Count == 0)
      {
        AnsiConsole.MarkupLine("[bold red]No Categories found![/]");
        return;
      }
      Table table = new Table()
        .Title("[bold red]Categories list[/]")
        .AddColumn("[bold yellow]Id[/]")
        .AddColumn("[bold darkorange]Name[/]");
      foreach (Category category in categories)
      {
        table.AddRow(
          $"[bold yellow]{categories.IndexOf(category) + 1}[/]",
          $"[bold darkorange]{category.Name}[/]"
        );
      }
      AnsiConsole.Write(table);
    }
    catch (Exception ex)
    {
      AnsiConsole.MarkupLine($"[red]Error getting categories: {ex.Message}[/]");
    }
  }

}
