//allow users to add an email and be able to send email
//add sms functionality
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Phonebook.kwm0304.Data;
using Phonebook.kwm0304.Interfaces;
using Phonebook.kwm0304.Repositories;
using Phonebook.kwm0304.Services;
using dotenv.net;
using Phonebook.kwm0304.Utils;

namespace Phonebook.kwm0304;
public class Program
{
  
  public static async Task Main(string[] args)
  {
    DotEnv.Load();
    var host = CreateHostBuilder(args).Build();

    using (var scope = host.Services.CreateScope())
    {
      var services = scope.ServiceProvider;
      var runApp = services.GetRequiredService<RunApplication>();
      await runApp.OnStart();
    }
    host.Run();
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
      services.AddDbContext<PhonebookContext>(options =>
      options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
      services.AddScoped<IContactRepository, ContactRepository>();
      services.AddScoped<ContactService>();
      services.AddScoped<GroupService>();
      services.AddScoped<IGroupRepository, GroupRepository>();
      services.AddScoped<RunApplication>();
      services.AddScoped<EmailService>();
      services.AddScoped<SMSHandler>();
    });
}