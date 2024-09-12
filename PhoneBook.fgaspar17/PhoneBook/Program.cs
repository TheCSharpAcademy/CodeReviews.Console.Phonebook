using Microsoft.Extensions.Configuration;
using PhoneBook;
using PhoneBookLibrary;


var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>();

IConfiguration config = builder.Build();

CancelSetup.CancelString = config.GetValue<string>("CancelString") ?? "c";

string gmailAddress = config.GetValue<string>("GmailAddress") ?? string.Empty;
string gmailAppPassword = config.GetValue<string>("GmailAppPassword") ?? string.Empty;
GlobalConfig.ConfigureGmail(gmailAddress, gmailAppPassword);

string twilioSid = config.GetValue<string>("TwilioSid") ?? string.Empty;
string twilioAuthToken = config.GetValue<string>("TwilioAuthToken") ?? string.Empty;
string twilioPhoneNumber = config.GetValue<string>("TwilioPhoneNumber") ?? string.Empty;
GlobalConfig.ConfigureTwilio(twilioSid, twilioAuthToken, twilioPhoneNumber);

UserInterface userInterface = new();
userInterface.Run();