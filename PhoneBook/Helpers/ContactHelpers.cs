internal class ContactHelpers
{
    internal static Contact? GetContact()
    {
        var contactsMap = MakeContactMap();
        if (DisplayInfoHelpers.NoRecordsAvailable(contactsMap.Keys)) return new Contact();

        var choice = DisplayInfoHelpers.GetChoiceFromSelectionPrompt(
            "Choose contact:", contactsMap.Keys);
        if (choice == DisplayInfoHelpers.Back) return new Contact();

        var success = contactsMap.TryGetValue(choice, out Contact? contact);
        if (!success) return new Contact();

        return contact;
    }

    private static Dictionary<string, Contact> MakeContactMap()
    {
        var contacts = new List<Contact>();
        var getContactsList = new Action(() =>
        {
            using var database = new ContactContext();
            contacts = database.Contacts.ToList();
        });

        if (!ErrorHandler.Success(getContactsList)) return [];
        var contactsList = MakeContactsList(contacts);

        return contactsList.Zip(contacts, (key, value) => new { key, value })
            .ToDictionary(x => x.key, x => x.value);
    }

    private static List<string> MakeContactsList(List<Contact> contacts)
    {
        var list = new List<string>();
        foreach (var contact in contacts)
        {
            list.Add(
                $"Name: [yellow]{contact.Name}[/] " +
                $"Phone: [yellow]{contact.Phone}[/] " +
                $"Email: [yellow]{contact.Email}[/] " +
                $"[{Console.BackgroundColor}]=>id:{contact.Id}[/]");
        }
        return list;
    }
}
