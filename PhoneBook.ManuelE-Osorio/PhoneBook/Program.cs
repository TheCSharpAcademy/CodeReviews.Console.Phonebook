using Microsoft.EntityFrameworkCore;
// var folder = Environment.path;
// var path = System.IO.Path.GetDirectoryName(System.IO.Path.)
// var DbPath = System.IO.Path.Join(path, "blogging.db");

namespace PhoneBookProgram;

public class PhoneBook
{
    public static void Main()
    {
        DataController controller = new ();
        controller.MainMenuController();

        // using var db = new PhoneBookContext();

        // Console.WriteLine(db.Database.EnsureDeleted());
        // Console.WriteLine(db.Database.EnsureCreated());
        // Console.WriteLine(db.Database.GenerateCreateScript());

        // var phonenumberUtil = PhoneNumberExt.PhoneNumberUtil.GetInstance();
        // PhoneNumberExt.PhoneNumber hola = phonenumberUtil.Parse("47870807","GT");

        // Console.WriteLine(phonenumberUtil.IsValidNumber(hola));
        // Console.WriteLine(hola.CountryCode);

        // // Note: This sample requires the database to be created before running.
        // Console.WriteLine($"Database path: ");

        // // Create
        // Console.WriteLine("Inserting a new Contact");    //INSERT CONTACT
        // db.Add(new Contact { ContactName = "John Doe" });
        // db.Add(new Contact { ContactName = "John Doe2" });
        // db.Add(new Contact { ContactName = "John Doe3" });
        // db.Add(new Contact { ContactName = "John Doe4" });
        // db.Add(new Contact { ContactName = "John Doe5" });
        // db.Add(new Contact { ContactName = "John Doe6" });
        // db.Add(new Contact { ContactName = "John Doe7" });
        // db.Add(new Contact { ContactName = "John Doe8" });
        // db.SaveChanges();

        // var contact = db.Contacts   //INSERT WHERE CONTACTNAME = ""
        //     .Where(c => c.ContactName == "John Doe" )
        //     .First();

        // contact.Emails?.Add( new Email {LocalName = "22334455", DomainName = "csharpacademy.com"});
        // contact.Emails?.Add( new Email{ LocalName = "test", DomainName = "csharacademy.com"});
        // db.SaveChanges();

        
        // // Update
        // Console.WriteLine("Updating the blog and adding a post");
        // blog.Url = "https://devblogs.microsoft.com/dotnet";
        // blog.Posts.Add(
        //     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        // db.SaveChanges();

        // Delete
        // Console.WriteLine("Delete the blog");
        // db.Remove(blog);
        // db.SaveChanges();
    }
}