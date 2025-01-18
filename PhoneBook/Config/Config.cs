using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Spectre.Console;

internal class Config
{
    public static string ConnectionString { get; set; }
    private const string AppsettingsFile = "appsettings.json";

    static Config()
    {
        CheckConfigFile();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppsettingsFile)
            .Build();

        ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    private static void CheckConfigFile()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), AppsettingsFile);

        if (!File.Exists(filePath))
        {
            var config = new
            {
                ConnectionStrings = new
                {
                    DefaultConnection = @"Server=(localdb)\MSSQLLocalDB;Database=Contacts;Integrated Security=True;TrustServerCertificate=True;"
                }
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (UnauthorizedAccessException ex)
            {
                AnsiConsole.MarkupLine($"[red]Failed to write to \"{AppsettingsFile}\" file.[/]");
                AnsiConsole.MarkupLine($"Details: [yellow]{ex.Message}[/]");
                DisplayInfoHelpers.PressAnyKeyToContinue();
            }
        }
    }
}
