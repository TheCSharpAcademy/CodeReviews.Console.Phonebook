using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models;

internal class EmailHistory
{
    [Key]
    public int EmailHistoryId { get; set; }
    public string ContactName { get; set; }
    public string ToEmail { get; set; }
    public DateTime SentTS { get; set; }
    public string Subject { get; set; }
    public string EmailBody { get; set; }
}
