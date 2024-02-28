using Phonebook.Models;

namespace Phonebook.Controllers;

internal class SmsHistoryController
{
    internal static void AddSmsHistory(SMSHistory newSmsHistory)
    {
        using var db = new PhonebookContext();
        db.Add(newSmsHistory);
        db.SaveChanges();
    }

    internal static SMSHistory GetSingleSmsById(int id)
    {
        using var db = new PhonebookContext();

        var singleSms = db.SMSHistory
                           .FirstOrDefault(eh => eh.SMSHistoryId == id);

        return singleSms;
    }

    internal static List<SMSHistory> GetSmsHistory()
    {
        try 
        { 
            using var db = new PhonebookContext();

            var smsHistory = db.SMSHistory.ToList();
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
