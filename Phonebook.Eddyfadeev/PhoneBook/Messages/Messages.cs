namespace PhoneBook.Messages;

/// <summary>
/// Class with different messages used in the codebase 
/// </summary>
internal static class Messages
{
    #region Problems with contact
    
    public const string ContactWasNotDeleted = "[red]Contact wasn't deleted.[/]";
    public const string ContactWasNotUpdated = "[red]Contact wasn't updated.[/]";
    public const string ContactWasNotAdded = "[red]Contact wasn't added.[/]";
    
    #endregion

    #region Success with contacts
    
    public const string ContactRetrieved = "[green]Contact retrieved.[/]";
    public const string ContactAdded = "[green]Contact added[/]";
    public const string ContactDeleted = "[green]Contact deleted[/]";
    public const string ContactUpdated = "[green]Contact updated[/]";
    
    #endregion

    #region Database and other problems
    
    public const string NoContactsInTheDatabase = "[red]No contacts found.[/]";
    public const string ProblemWithCommand = "[red]Problem executing command.[/]";
    
    #endregion
    
    #region General messages and options

    public const string UpdateCancelled = "[red]Update cancelled.[/]";
    public const string DeleteCancelled = "[red]Delete cancelled.[/]";
    public const string PressAnyKeyOption = "[white]Press any key to continue...[/]";
    
    public const string InvalidPhoneNumber = "[red]Invalid phone number.[/]\n" +
                                             "[green]Phone shoild be 10-12 digits long.\n" +
                                             "+123456789012 or 1234567890[/] ";
    public const string InvalidEmailAddress = "[red]Invalid email address.[/]" +
                                              "[green]example@example.com[/]";
    
    public const string NameIsRequired = "[red]Name is required.[/]";
    public const string MessageWasSuccessfullySent = "[green]Message sent.[/]";
    
    #endregion
    
    #region Prompts
    
    public const string AskFirstName = "[white]Enter a name:[/]";
    public const string AskLastName = "[white]Enter a last name (or leave it empty):[/]";
    public const string AskPhone = "[white]Enter a phone number (or leave it empty):[/]";
    public const string AskEmail = "[white]Enter an email (or leave it empty):[/]";
    public const string AskGroupName = "[white]Enter a group name (or leave it empty):[/]";
    public const string ContactsHandlerTitle = "[white]Select contact from the list:[/]";
    public const string DeletePrompt = "[red]Delete contact?[/]";
    public const string UpdatePrompt = "[red]Update contact?[/]";
    public const string EnterSubject = "[white]Enter a subject: [/]";
    public const string EnterMessage = "[white]Enter a message: [/]";
    public const string SendAnEmail = "[white]Whould you like to send an email?[/]";

    #endregion
}