using Microsoft.EntityFrameworkCore;
using Phonebook.Models;


namespace Phonebook.Controllers;

internal class EmailHistoryController
{
    internal static EmailHistory AddEmailHistory(EmailHistory newEmailHistory)
    {
        using var db = new PhonebookContext();
        db.Add(newEmailHistory);
        db.SaveChanges();
        return newEmailHistory;
    }

    internal static List<EmailHistory> GetEmailHistory()
    {
        try
        {
            using var db = new PhonebookContext();

            var emailHistory = db.EmailHistory.ToList();

            return emailHistory;
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null; // Or throw the exception if appropriate for your application
        }
    }

    internal static EmailHistory GetSingleEmailById(int id)
    {
        using var db = new PhonebookContext();

        var singleEmail = db.EmailHistory
                            .FirstOrDefault(eh => eh.EmailHistoryId == id);

        return singleEmail;
    }
}
