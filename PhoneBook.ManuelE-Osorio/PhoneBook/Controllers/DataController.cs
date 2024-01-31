using System.Net.Mail;
using PhoneValidation = PhoneNumbers;
using Microsoft.Extensions.Configuration;

namespace PhoneBookProgram;

public class DataController
{
    public static readonly int PageSize = 10;
    public static readonly int EmailBodySize = 1000;
    public static readonly int SMSBodySize = 160;
    public bool RunViewContacts;
    public bool RunContactDetail;
    public DBController DBInstance;
    public List<Contact> Contacts;
    public List<Email> Emails;
    public List<PhoneNumber> PhoneNumbers;
    public string? UserEmail;
    public string? UserPhoneNumber;
    public DataController()
    {
        try
        {
            IConfiguration jsonConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            PhoneBookContext.PhoneBookConnectionString = jsonConfig.GetConnectionString("DefaultConnection");
            UserEmail = jsonConfig.GetSection("Settings")["UserEmail"] ?? "";
            UserPhoneNumber = jsonConfig.GetSection("Settings")["UserPhoneNumber"] ?? "";
        }
        catch
        {
            Console.WriteLine("Please configure your appsettings.json");
        }
        DBInstance = new();
        Contacts = [];
        Emails = [];
        PhoneNumbers = [];
        RunViewContacts = true;
        RunContactDetail = false;
    }

    public void Start()
    {  
        try
        {
            DBInstance.DBInit();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Thread.Sleep(2000);
            return;
        }
        MainMenuController();
    }

    private void MainMenuController()
    {
        UI.WelcomeMessage();

        var contactsToUI = new List<ContactDto>();    
        var getContacts = true;
        var selection = 0;
        var prevSelection = 0;
        var currentPage = 0;
        var pressedKey = new ConsoleKey();
        string? errorMessage = null;
        char? filterKey = null;
        string filterCategory = "";

        while(RunViewContacts)
        {
            if(getContacts)
            {
                if(filterKey == null && filterCategory == "")
                    Contacts = DBInstance.GetContacts();
                else if (filterKey != null && filterCategory == "")
                    Contacts = DBInstance.GetContacts(filterKey ?? ' ');
                else if (filterCategory != "")
                {
                    Contacts = DBInstance.GetContacts(filterCategory);
                    filterCategory = "";
                }
                contactsToUI = Contacts
                    .Select(contact => new ContactDto(contact)).ToList();
                getContacts = false;
                selection = 0;
                prevSelection = 0;
                filterKey = null;
            }

            UI.DisplayContacts(contactsToUI, 
                selection, prevSelection, currentPage);
            pressedKey = Console.ReadKey(true).Key;
            switch(pressedKey)
            {
                case(ConsoleKey.UpArrow):
                {
                    prevSelection = selection;
                    selection--;
                    if(selection < 0)
                        selection = 0;
                    currentPage = selection/PageSize;
                    break;
                }
                case(ConsoleKey.DownArrow):
                {
                    prevSelection = selection;
                    selection++;
                    if(selection > contactsToUI.Count - 1)
                        selection = contactsToUI.Count - 1;
                    currentPage = selection/PageSize;
                    break;
                }
                case(ConsoleKey.RightArrow):
                {
                    currentPage++;
                    if(currentPage*PageSize > contactsToUI.Count - 1)
                        currentPage--;
                    prevSelection = selection;
                    selection = currentPage*PageSize;
                    break;
                }
                case(ConsoleKey.LeftArrow):
                {
                    currentPage--;
                    if(currentPage < 0)
                        currentPage++;
                    prevSelection = selection;
                    selection = currentPage*PageSize;
                    break;
                }
                case(ConsoleKey.I):
                    getContacts = Insert();
                    break;

                case(ConsoleKey.M):
                    if(Contacts.Count > 0)
                        getContacts = Modify(Contacts[selection].ContactId);
                    break;

                case(ConsoleKey.D):
                    if(Contacts.Count > 0)
                        getContacts = Delete(Contacts[selection].ContactId);
                    break;

                case(ConsoleKey.C):
                {
                    getContacts = true;
                    currentPage = 0;
                    UI.FilterByCategory();
                    filterCategory = Console.ReadLine() ?? "";
                    break;
                }
                case(ConsoleKey.Q):
                {
                    getContacts = true;
                    currentPage = 0;
                    filterCategory = "";
                    break;
                }
                case(ConsoleKey.P):
                {
                    ImportController.ImportContacts();
                    ImportController.ImportEmails();                    
                    ImportController.ImportPhones();
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey(true);
                    getContacts = true;
                    break;
                }
                case(ConsoleKey.Enter):
                {
                    RunContactDetail = true;
                    ViewContactDetail(Contacts[selection].ContactId, 
                        Contacts[selection].ContactName);
                    break;
                }
                case(ConsoleKey.F):
                {
                    UI.FilterByFirstLetter();
                    filterKey = Console.ReadKey(false).KeyChar;
                    getContacts = true;
                    currentPage = 0;
                    break;
                }
                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunViewContacts = false;
                    break;
            }
        }
        UI.ExitMessage(errorMessage);
    }

    public void ViewContactDetail(int contactId, string contactName)
    {
        var emailsDto = new List<EmailDto>();
        var phonesDto = new List<PhoneNumberDto>();
        var getContactDetails = true;
        var selection = 0;
        var prevSelection = 0;
        var pressedKey = new ConsoleKey();
        
        while(RunContactDetail)
        {
            if(getContactDetails)
            {
                Emails = DBInstance.GetEmails(contactId);
                PhoneNumbers = DBInstance.GetPhones(contactId);
                emailsDto = Emails
                    .Select(p => new EmailDto(p)).ToList();
                phonesDto = PhoneNumbers
                    .Select(p => new PhoneNumberDto(p)).ToList();
                getContactDetails = false;
                selection = 0;
                prevSelection = 0;
            } 

            UI.DisplayContactData(phonesDto, emailsDto, selection, prevSelection, contactName);
            pressedKey = Console.ReadKey().Key;
            switch(pressedKey)
            {
                case(ConsoleKey.UpArrow):
                {
                    prevSelection = selection;
                    selection--;
                    if(selection < 0)
                        selection = 0;
                    break;
                }
                case(ConsoleKey.DownArrow):
                {
                    prevSelection = selection;
                    selection++;
                    if(selection > phonesDto.Count + emailsDto.Count - 1)
                        selection = phonesDto.Count + emailsDto.Count - 1;
                    break;
                }
                case(ConsoleKey.E):
                    getContactDetails = Insert(new Email {ContactId = contactId});
                    break;

                case(ConsoleKey.P):
                    getContactDetails = Insert(new PhoneNumber {ContactId = contactId});
                    break;
                
                case(ConsoleKey.D):
                {
                    if(selection < PhoneNumbers.Count)
                        getContactDetails = Delete(PhoneNumbers[selection]);
                    else if (Emails.Count > 0)
                        getContactDetails = Delete(Emails[selection - phonesDto.Count]);
                    break;
                }
                case(ConsoleKey.M):
                {
                    if(selection < PhoneNumbers.Count)
                        getContactDetails = Modify(PhoneNumbers[selection]);
                    else if (Emails.Count > 0)
                        getContactDetails = Modify(Emails[selection - phonesDto.Count]);
                    break;
                }
                case(ConsoleKey.Enter):
                {
                    if(selection < PhoneNumbers.Count)
                        SendSms(phonesDto[selection]);
                    else if (Emails.Count > 0)
                        SendEmail(emailsDto[selection - phonesDto.Count]);
                    break;
                }
                case(ConsoleKey.Backspace):
                case(ConsoleKey.Escape):
                    RunContactDetail = false;
                    break;
            }
        }
    }

    public bool Insert(string objectName = "contact name")
    {
        string newContactName;
        string newCategoryName = "";
        string? contactErrorMessage = null;
        string? categoryErrorMessage = null;

        while(true)
        {
            UI.DisplayInsert(objectName, contactErrorMessage);
            newContactName = Console.ReadLine() ?? ""; 
            contactErrorMessage = InputValidation.ContactNameValidation(newContactName, Contacts);
            
            if( contactErrorMessage == null)
            {
                UI.DisplayCategoryInsert("contact category", categoryErrorMessage);
                newCategoryName = Console.ReadLine() ?? "";
                categoryErrorMessage = InputValidation.CategoryNameValidation(newCategoryName);
            }

            if(newContactName.Equals("0"))
                return false;
            else if(categoryErrorMessage == null && contactErrorMessage == null)
            {
                DBInstance.Insert(newContactName, newCategoryName);
                return true;
            }
        }  
    }

    public bool Insert(Email emailToInsert, string objectName = "email")
    {
        string emailInput;
        string? errorMessage = null;
       
        while(true)
        {
            UI.DisplayInsert(objectName, errorMessage);
            emailInput = Console.ReadLine() ?? "";
            errorMessage = InputValidation.EmailValidation(emailInput);
            
            if(emailInput.Equals("0"))
                return false;
            else if(errorMessage == null)
            {
                var validEmail = new MailAddress(emailInput);
                emailToInsert.LocalName = validEmail.User;
                emailToInsert.DomainName = validEmail.Host;
                DBInstance.Insert(emailToInsert);
                return true;
            }
        }
    }

    public bool Insert(PhoneNumber phoneToInsert, string objectType = "phone number" ) 
    {
        string phoneNumber;
        string? errorMessage = null;
        
        while(true)
        {
            UI.DisplayInsert(objectType, errorMessage);
            phoneNumber = Console.ReadLine() ?? "";  
            errorMessage = InputValidation.PhoneNumberValidation(phoneNumber);

            if(phoneNumber.Equals("0"))
                return false;
            else if(errorMessage == null)
            {
                var phoneNumberUtil = PhoneValidation.PhoneNumberUtil.GetInstance();
                var phoneNumberToModify = phoneNumberUtil.Parse(phoneNumber, null);
                phoneToInsert.CountryCode = phoneNumberToModify.CountryCode.ToString();
                phoneToInsert.LocalNumber = phoneNumberToModify.NationalNumber.ToString();
                DBInstance.Insert(phoneToInsert);
                return true;
            }
        }        
    }

    public bool Modify(int contactId, string objectName = "contact name")
    {
        string modifyContactName;
        string modifyCategoryName = "";
        string? contactErrorMessage = null;
        string? categoryErrorMessage = null;

        while(true)
        {
            UI.DisplayInsert(objectName, contactErrorMessage); //Pending UI
            modifyContactName = Console.ReadLine() ?? "";
            contactErrorMessage = InputValidation.ContactNameValidation(modifyContactName, Contacts);
            
            if( contactErrorMessage == null && modifyContactName != "0")
            {
                UI.DisplayCategoryInsert("contact category", categoryErrorMessage);
                modifyCategoryName = Console.ReadLine() ?? "";
                categoryErrorMessage = InputValidation.CategoryNameValidation(modifyCategoryName);
            }

            if(modifyContactName.Equals("0"))
                return false;            
            else if(contactErrorMessage == null && categoryErrorMessage == null)
            {
                DBInstance.Modify(new Contact {ContactId = contactId, ContactName = modifyContactName, 
                    Category = modifyCategoryName});
                return true;
            }
        }        
    }

    public bool Modify(Email emailToModify, string objectName = "email")
    {
        string? errorMessage = null;
        string emailInput;

        while(true)
        {
            UI.DisplayInsert(objectName, errorMessage);
            emailInput = Console.ReadLine() ?? ""; 
            errorMessage = InputValidation.EmailValidation(emailInput);

            if(emailInput.Equals("0"))
                return false;            
            else if(errorMessage == null)
            {
                var validEmail = new MailAddress(emailInput);
                emailToModify.LocalName = validEmail.User;
                emailToModify.DomainName = validEmail.Host;
                DBInstance.Modify(emailToModify);
                return true;
            }
        }        
    }

    public bool Modify(PhoneNumber phoneToModify, string objectName = "phone number")
    {
        string? errorMessage = null;
        string phoneNumber;

        while(true)
        {
            UI.DisplayInsert(objectName, errorMessage);
            phoneNumber = Console.ReadLine() ?? "";
            errorMessage = InputValidation.PhoneNumberValidation(phoneNumber);

            if(phoneNumber.Equals("0"))
                return false;
            else if(errorMessage == null)
            {
                var phoneNumberUtil = PhoneValidation.PhoneNumberUtil.GetInstance();
                var phoneNumberToModify = phoneNumberUtil.Parse(phoneNumber, null);
                phoneToModify.CountryCode = phoneNumberToModify.CountryCode.ToString();
                phoneToModify.LocalNumber = phoneNumberToModify.NationalNumber.ToString();
                DBInstance.Modify(phoneToModify);
                return true;
            }
        }        
    }

    public bool Delete(int contactId, string objectType = "contact")
    {
        UI.DisplayConfirmationPromt(objectType);
        var selection = Console.ReadLine() ?? "";
        
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            DBInstance.Delete(contactId);
            return true;
        }
        return false;
    }

    public bool Delete(Email objectToDelete, string objectType = "email")
    {
        UI.DisplayConfirmationPromt(objectType);
        var selection = Console.ReadLine() ?? "";
        
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            DBInstance.Delete(objectToDelete);
            return true;
        }
        return false;
    }

    public bool Delete(PhoneNumber objectToDelete, string objectType = "phone number")
    {
        UI.DisplayConfirmationPromt(objectType);
        var selection = Console.ReadLine() ?? "";
        
        if(selection.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            DBInstance.Delete(objectToDelete);
            return true;
        }
        return false;
    }

    public void SendEmail(EmailDto emailToSend)
    {
        string? errorMessage;
        string emailSubject;

        errorMessage = InputValidation.EmailValidation(UserEmail ?? "");
        
        if(errorMessage != null)
        {
            UI.ConfigureAppSettings("email");
            Thread.Sleep(3000);
            return;
        }

        while(true)
        {
            UI.DisplayEmailSubject(errorMessage);
            emailSubject = Console.ReadLine() ?? "";
            errorMessage = InputValidation.ValidateSubject(emailSubject);
            if(errorMessage == null)
            {
                EnterEmailBody(emailSubject, emailToSend);
                return;
            }
        }
    }

    public void EnterEmailBody(string emailSubject, EmailDto emailToSend)
    {
        string emailBody = "";
        string? errorMessage = null;
        ConsoleKeyInfo pressedKey;

        while(true)
        {
            UI.DisplayEmail(UserEmail, emailToSend.Email, emailSubject, errorMessage);
            Console.Write(emailBody);
            pressedKey = Console.ReadKey();
                    
            switch(pressedKey.Key)
            {
                case(ConsoleKey.Enter):
                    emailBody += "\n";
                    break;

                case(ConsoleKey.Backspace):
                {
                    if(emailBody.Length > 0)
                        emailBody = emailBody.Remove(emailBody.Length-1);
                    if(emailBody.Length < EmailBodySize)
                        errorMessage = null;
                    break;
                }
                default:
                {
                    emailBody += pressedKey.KeyChar.ToString();
                    if(emailBody.Length > EmailBodySize)
                        errorMessage = $"Email body has more than {EmailBodySize} characters";
                    break;
                }
            }

            if((pressedKey.Modifiers & ConsoleModifiers.Control) != 0 && pressedKey.Key == ConsoleKey.S
                && errorMessage == null)
            {
                UI.DisplaySendEmail();
                return;
            }
            if((pressedKey.Modifiers & ConsoleModifiers.Control) != 0 && pressedKey.Key == ConsoleKey.Backspace)
                return;
        }
    }

    public void SendSms(PhoneNumberDto phoneToSend)
    {
        string? errorMessage;

        errorMessage = InputValidation.PhoneNumberValidation(UserPhoneNumber ?? "");
        if(errorMessage != null)
        {
            UI.ConfigureAppSettings("phone number");
            Thread.Sleep(3000);
            return;
        }

        string smsBody = "";
        errorMessage = null;
        ConsoleKeyInfo pressedKey;
        while(true)
        {
            UI.DisplaySms(UserPhoneNumber, phoneToSend.PhoneNumber, errorMessage);
            Console.Write(smsBody);
            pressedKey = Console.ReadKey();
                    
            switch(pressedKey.Key)
            {
                case(ConsoleKey.Enter):
                    smsBody += "\n";
                    break;

                case(ConsoleKey.Backspace):
                {
                    if(smsBody.Length > 0)
                        smsBody = smsBody.Remove(smsBody.Length-1);
                    if(smsBody.Length < SMSBodySize)
                        errorMessage = null;
                    break;
                }
                default:
                {
                    smsBody += pressedKey.KeyChar.ToString();
                    if(smsBody.Length > SMSBodySize)
                        errorMessage = $"SMS has more than {SMSBodySize} characters";
                    break;
                }
            }

            if((pressedKey.Modifiers & ConsoleModifiers.Control) != 0 && pressedKey.Key == ConsoleKey.S
                && errorMessage == null)
            {
                UI.DisplaySendSms();
                return;
            }
            if((pressedKey.Modifiers & ConsoleModifiers.Control) != 0 && pressedKey.Key == ConsoleKey.Backspace)
                return;
        }
    }
}