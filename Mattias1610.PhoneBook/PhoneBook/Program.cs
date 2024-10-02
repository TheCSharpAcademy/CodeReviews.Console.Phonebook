using PhoneBook;
using Spectre.Console;
using System;

Console.Clear();
bool isAppRunning = true;

while(isAppRunning){
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
        .Title("What would you like to do?")
        .AddChoices(MenuOptions.AddNumber,
        MenuOptions.DeleteNumber,
        MenuOptions.UpdateNumber,
        MenuOptions.ShowInfo,
        MenuOptions.SendEmail,
        MenuOptions.Categories,
        MenuOptions.Quit)
    );

    switch(option){
        case MenuOptions.AddNumber:
            NumberController.AddNumber();
            break;
        
        case MenuOptions.DeleteNumber:
            var numberToDelete = Service.GetNumber();  
            if (numberToDelete != null)
            {
                NumberController.DeleteNumber(numberToDelete);
                AnsiConsole.MarkupLine("[green]Number deleted successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No number selected or available to delete.[/]");
            }
            break;
        
        case MenuOptions.UpdateNumber:
            var numberToUpdate = Service.GetNumber();
            NumberController.UpdateNumber(numberToUpdate);
            break;
        
        case MenuOptions.ShowInfo:
            Service.GetNum();
            break;
        
        case MenuOptions.SendEmail:
            Service.SendEmail();
            break;
        
        case MenuOptions.Categories:
            Service.GetCategories();
            break;
        case MenuOptions.Quit:
            Environment.Exit(0);
            break;
        }
    
}

 enum MenuOptions{
        AddNumber,
        DeleteNumber,
        UpdateNumber,
        ShowInfo,
        SendEmail,
        Categories,
        Quit
    }
