//record contacts with their phone numbers
//users should be able to add, delete, update and readon from database using the console
//Contact class with Id Int, Name string, Email string, phonenumber string
// validate emails and phone numbers with regex
//reverse scaffold
//allow users to add an email and be able to send email
//create categories of contacts (Family, friends, work)
//add sms functionality
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phonebook.kwm0304.Data;
using Phonebook.kwm0304.Repositories;

public class Program
{
  public static void Main(string[] args)
  {
    CreateHostBuilder(args).Build().Run();
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
      services.AddDbContext<PhonebookContext>(options =>
      options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
      services.AddScoped<ContactRepository>();
    });
}