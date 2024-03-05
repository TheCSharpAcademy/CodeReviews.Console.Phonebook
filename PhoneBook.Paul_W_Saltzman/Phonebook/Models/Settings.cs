using System.ComponentModel.DataAnnotations;
namespace Phonebook.Models;

internal class Settings
{
    [Key]
    public int SettingId { get; set; }
    [EmailAddress]
    public string? FromMail { get; set; }
    public string? Password { get; set; }
    [Url]
    public string? SmtpUrl { get; set; }
    public int Port { get; set; }
    public string? TwilioAccountSid { get; set; }
    public string? TwilioAuthToken { get; set; }
    [Phone]
    public string? TwilioNumber { get; set; }
}
