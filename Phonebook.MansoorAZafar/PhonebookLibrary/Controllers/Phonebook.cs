using PhonebookLibrary.Models;
using PhonebookLibrary.Views;
using System.Runtime.CompilerServices;

namespace PhonebookLibrary.Controllers;

public class Phonebook
{
    public void Menu()
    {
        bool running = true;
        while(running)
        {
            System.Console.Clear();
            DataViewer.Figlet(text: "Home", alignment: "center");
            DataViewer.DisplayHeader("");

            var selection = Utility.GetSelectionInput<HomeMenuSelections>
            (
                enumValues: Enum.GetValues<HomeMenuSelections>(),
                alternateNames: item => item switch
                {
                    HomeMenuSelections.AddContact => "Add Contact",
                    HomeMenuSelections.ReadContact => "Get Contact(s)",
                    HomeMenuSelections.UpdateContact => "Update Contact",
                    HomeMenuSelections.DeleteContact => "Delete Contact",
                    _ => item.ToString() ?? string.Empty
                }
            );
            if(!this.HandleSelection(selection)) running = false;
        }

        System.Console.Clear();
        DataViewer.Figlet("Goodbye!");
        
    }

    private bool HandleSelection(HomeMenuSelections selection)
    {
        switch(selection)
        {
            case HomeMenuSelections.AddContact:
                System.Console.Clear();
                DataViewer.DisplayHeader("Add Contact");

                Database.AddContact();
                break;
            
            case HomeMenuSelections.ReadContact:
                System.Console.Clear();
                DataViewer.DisplayHeader("View Contacts");

                DataViewer.DisplayTable<Contact>(Database.GetContact(), Contact.Headers);
                break;
            
            case HomeMenuSelections.DeleteContact:
                System.Console.Clear();
                DataViewer.DisplayHeader("Delete Contact");

                DataViewer.DisplayTable<Contact>(Database.GetContact(), Contact.Headers);
                int id = Utility.GetValidID("Please enter the ID to be deleted or enter 0 to exit\n> ");
                if (id == -1) break;

                Database.DeleteContact(id);
                break;
            
            case HomeMenuSelections.UpdateContact:
                System.Console.Clear();
                DataViewer.DisplayHeader("Update Contact");

                DataViewer.DisplayTable<Contact>(Database.GetContact(), Contact.Headers);
                int Id = Utility.GetValidID("Please Enter the ID to be updated or enter 0 to exit\n> ");
                if (Id == -1) break;

                Contact c = Utility.CreateContact();
                c.Id = Id;


                Database.UpdateContact(c);
                break;
            
            default:
                Utility.PressToContinue();
                return false;
        }
        Utility.PressToContinue();
        return true;
    }
}
