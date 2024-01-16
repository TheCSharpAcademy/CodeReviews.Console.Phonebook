namespace PhoneBookProgram;

public class ImportController
{
    public static void ImportContacts()
    {
        Console.WriteLine("\nImporting Contacts ...\n");
        string contactsFile = "ContactsImport.csv";

        List<Contact> contacts = [];
        try
        {
            contacts = File.ReadAllLines(contactsFile)
                .Skip(1)
                .Select(Contact.FromCsv)
                .ToList();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    
        using DBController DBInstance = new();
        foreach (Contact contact in contacts)
        {
            try
            {
                DBInstance.Insert(contact.ContactName, contact.Category ?? "");
                Console.WriteLine($"Contact \"{contact.ContactName}\" added succesfully.");
            }
            catch
            {
                Console.WriteLine($"Error: Contact \"{contact.ContactName}\" already exists");
            }
        }
    }

    public static void ImportEmails()
    {
        Console.WriteLine("\nImporting Emails ...\n");
        var emailsFile = "EmailsImport.csv";
        IEnumerable<string> csvData;

        List<Email> emails = [];
        try
        {
            csvData = File.ReadAllLines(emailsFile)
                .Skip(1);
        }
        catch
        {
            Console.WriteLine($"File {emailsFile} not found");
            return;
        }
        
        using DBController DBInstance = new();
        foreach(string emailData in csvData)
        {
            try
            {
                string contactName = emailData.Split(',')[0];
                emails.Add( Email.FromCsv(emailData ,DBInstance.GetContactID(contactName)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        foreach(Email email in emails)
        {
            try
            {
                DBInstance.Insert(email);
                Console.WriteLine($"Phone Number \"{email.GetEmail()}\" added succesfully");
            }
            catch
            {
                Console.WriteLine($"Couldn't add email {email.GetEmail()}, contact not found");
            }
        }
    }

    public static void ImportPhones()
    {
        Console.WriteLine("\nImporting Phone Numbers ...\n");
        var phonesFile = "PhoneNumbersImport.csv";
        IEnumerable<string> csvData;

        List<PhoneNumber> phones = [];
        try
        {
            csvData = File.ReadAllLines(phonesFile)
                .Skip(1);
        }
        catch
        {
            Console.WriteLine($"File {phonesFile} not found");
            return;
        }
        
        using DBController DBInstance = new();
        foreach(string phoneData in csvData)
        {
            try
            {
                string contactName = phoneData.Split(',')[0];
                phones.Add( PhoneNumber.FromCsv(phoneData ,DBInstance.GetContactID(contactName)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(1000);
            }
        }
        
        foreach(PhoneNumber phone in phones)
        {
            try
            {
                DBInstance.Insert(phone);
                Console.WriteLine($"Phone Number \"{phone.GetFullPhoneNumber()}\" added succesfully");
            }
            catch
            {
                Console.WriteLine($"Couldn't add phone {phone.GetFullPhoneNumber()}, contact not found");
            }
        }
    }
}