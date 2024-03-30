using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Controllers;

internal class SettingsController
{
    internal static Settings GetSettings()
    {
        using var db = new PhonebookContext();
        int id = 1;

        Settings settings = db.Settings.FirstOrDefault(eh => eh.SettingId == id);

        return settings;
    }

    internal static void AddSettings(Settings settings)
    {
        using var db = new PhonebookContext();
        try
        {
            db.Add(settings);
            db.SaveChanges();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }

    internal static void UpdateSettings (Settings settings) 
    {
        using var db = new PhonebookContext();
        db.Update(settings);
        db.SaveChanges();
    }

    internal static void DeleteSettings(Settings settings)
    {
        using var db = new PhonebookContext();
        db.Remove(settings);
        db.SaveChanges();


    }

}
