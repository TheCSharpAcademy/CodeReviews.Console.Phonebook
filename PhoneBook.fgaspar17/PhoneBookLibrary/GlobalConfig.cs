namespace PhoneBookLibrary;

public static class GlobalConfig
{
    public static string FromAddress { get; private set; } = string.Empty;
    public static string GmailAppPassword { get; private set; } = string.Empty;
    public static string TwilioPhoneNumber { get; private set; } = string.Empty;
    public static string TwilioSid { get; private set; } = string.Empty;
    public static string TwilioAuthToken { get; private set; } = string.Empty;

    public static void ConfigureGmail(string fromAddress, string gmailAppPassword)
    {
        FromAddress = fromAddress;
        GmailAppPassword = gmailAppPassword;
    }

    public static void ConfigureTwilio(string twilioSid, string twilioAuthToken, string twilioPhoneNumber)
    {
        TwilioSid = twilioSid;
        TwilioAuthToken = twilioAuthToken;
        TwilioPhoneNumber = twilioPhoneNumber;
    }
}