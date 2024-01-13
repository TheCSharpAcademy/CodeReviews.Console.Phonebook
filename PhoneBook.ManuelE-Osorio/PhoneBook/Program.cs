using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookProgram;

public class PhoneBook
{
    public static void Main()
    {
        // PopulateDB();

        DataController controller = new ();
        controller.MainMenuController();
        // EmailValidator();
    }

    public static void PopulateDB()
    {
        var db = new PhoneBookContext();
        Console.WriteLine(db.Database.EnsureDeleted());
        Console.WriteLine(db.Database.EnsureCreated());
        Console.WriteLine(db.Database.GenerateCreateScript());
        db.Add(new Contact { ContactName = "John Doe" });
        db.Add(new Contact { ContactName = "John Doe2" });
        db.Add(new Contact { ContactName = "John Doe3" });
        db.Add(new Contact { ContactName = "John Doe4" });
        db.Add(new Contact { ContactName = "John Doe5" });
        db.Add(new Contact { ContactName = "John Doe6" });
        db.Add(new Contact { ContactName = "John Doe7" });
        db.Add(new Contact { ContactName = "John Doe8" });
        db.SaveChanges();
        var contact = db.Contacts 
                    .Where(c => c.ContactName == "John Doe" )
            .First();

        contact.Emails?.Add( new Email {LocalName = "22334455", DomainName = "csharpacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "test", DomainName = "csharacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "testing", DomainName = "csharacademy.com"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "123456789"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "12345678"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "1234567"});
        db.SaveChanges();
    
        contact = db.Contacts  
                    .Where(c => c.ContactName == "John Doe2" )
            .First();

        contact.Emails?.Add( new Email {LocalName = "22334455", DomainName = "csharpacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "test", DomainName = "csharacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "testing", DomainName = "csharacademy.com"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "123456789"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "12345678"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "1234567"});
        db.SaveChanges();
    
        contact = db.Contacts  
                    .Where(c => c.ContactName == "John Doe3" )
            .First();

        contact.Emails?.Add( new Email {LocalName = "22334455", DomainName = "csharpacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "test", DomainName = "csharacademy.com"});
        contact.Emails?.Add( new Email {LocalName = "testing", DomainName = "csharacademy.com"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "123456789"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "12345678"});
        contact.PhoneNumbers?.Add( new PhoneNumber {CountryCode = "1", LocalNumber = "1234567"});
        db.SaveChanges();
    }

    public static void EmailValidator()
    {
        string addresstest = "prettyandsimple@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out MailAddress? test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="very common@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="very.common@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest = "disposable.style.email.with+symbol@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="other.email-with-dash@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="x@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="\"much.more unusual\"@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="\"very.unusual.@.unusual.com\"@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="\"very.(),:;<>[]\".VERY.\"very@\\ \"very\".unusual\"@strange.example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="example-indeed@strange-example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="admin@mailserver1";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="#!$%&'*+-/=?^_`{}|~@example.org";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="\"()<>[]:,;@\\\"!#$%&'*+-/=?^_`{}| ~.a\"@example.org";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="\" \"@example.org";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="example@localhost";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="example@s.solutions";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="user@com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="user@localserver";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="user@[2001:DB8::1][";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="user@[IPv6:2001:db8::1]";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="©other.email-with-dash@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="?prettyandsimple@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="用户@例子.广告";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="अजअय@डाअटा.भारत";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="квіточка@пошта.укр";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="θσερ@εχαμπλε.ψομ";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="Dörte@Sörensen.example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="аджай@экзампл.рус";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));

        addresstest ="Abc.example.com";
        Console.WriteLine("\nInvalid\n");
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="A@b@c@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="a\"b(c)d,e:f;g<h>i[j\\k]l@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="just\"not\"right@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="this is\"not\allowed@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="this\\ still\"not\\allowed@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="john..doe@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="john.doe@example..com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
        
        addresstest ="1234567890123456789012345678901234567890123456789012345678901234+x@example.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
                
        addresstest ="alskjnakjanv1wadfqaebbsfdfagfbnbKJANDVlvn13dcljnvzlkjbnadlkgf3cjnj@winnipeg.com";
        Console.WriteLine(addresstest);
        Console.WriteLine(MailAddress.TryCreate(addresstest, out test));
        Console.WriteLine(InputValidation.EmailValidation(addresstest));
    }
}