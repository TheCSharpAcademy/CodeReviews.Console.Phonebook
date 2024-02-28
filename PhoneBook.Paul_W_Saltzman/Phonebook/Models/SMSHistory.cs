using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models;

internal class SmsHistory
{
    [Key]
    public int SMSHistoryId { get; set; }
    public string MessageSid { get; set; }
    public string ContactName { get; set; }
    public string ToNumber { get; set; }
    public DateTime SentTS { get; set; }
    public string Body { get; set; }
}
