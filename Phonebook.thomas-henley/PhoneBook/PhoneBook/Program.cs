using Microsoft.Extensions.Configuration;
using PhoneBook;

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var db = new PhonebookDbContext(config.GetConnectionString("LocalDb")!);

var validation = new ContactValidator();

var ui = new SpectreConsole(validation);

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

if (config["UseExampleData"] is "True")
{
    db.PopulateExampleData();
}

PhonebookController controller = new(config, db, ui, validation);
controller.Run();

db.Database.EnsureDeleted();

db.Dispose();