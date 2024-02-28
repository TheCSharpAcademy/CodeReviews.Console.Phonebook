using Phonebook.Models;

namespace Phonebook.Controllers;

internal class SmsHistoryController
{
    internal static void AddSmsHistory(SmsHistory newSmsHistory)
    {
        using var db = new PhonebookContext();
        db.Add(newSmsHistory);
        db.SaveChanges();
    }

    internal static SmsHistory GetSingleSmsById(int id)
    {
        using var db = new PhonebookContext();

        var singleSms = db.SmsHistory
                           .FirstOrDefault(eh => eh.SMSHistoryId == id);

        return singleSms;
    }

    internal static List<SmsHistory> GetSmsHistory()
    {
        try 
        { 
            using var db = new PhonebookContext();

            var smsHistory = db.SmsHistory.ToList();
            return smsHistory;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            return null;
        }
    }
}
