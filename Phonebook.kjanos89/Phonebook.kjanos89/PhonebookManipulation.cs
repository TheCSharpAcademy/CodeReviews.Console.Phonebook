namespace Phonebook.kjanos89;

public class PhonebookManipulation(Menu menu)
{
    Validation validation=new Validation();

    public void AddContact()
    {
        Console.Clear();
        Console.WriteLine("Please type in the name of the contact or press '0' to return to the menu");
        Console.WriteLine("Any name is accepted except the ones containing numbers or special characters. Example: John Doe");
        string tempName = Console.ReadLine();
        if (tempName == "0")
        {
            menu.DisplayMenu();
            return;
        }
        while (!validation.CheckName(tempName))
        {
            Console.WriteLine("There was an error with the name you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            tempName = Console.ReadLine();
            if (tempName == "0")
            {
                menu.DisplayMenu();
                return;
            }
        }
        string name = tempName;

        Console.WriteLine("Please type in the phone number of the contact or press '0' to return to the menu");
        Console.WriteLine("Example: +36201234567 or 06201234567");
        string tempPhone = Console.ReadLine();
        if (tempPhone == "0")
        {
            menu.DisplayMenu();
            return;
        }
        while (!validation.CheckPhoneNumber(tempPhone))
        {
            Console.WriteLine("There was an error with the phone number you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            tempPhone = Console.ReadLine();
            if (tempPhone == "0")
            {
                menu.DisplayMenu();
                return;
            }
        }
        string phoneNumber = tempPhone;

        Console.WriteLine("Please type in the e-mail address of the contact or press '0' to return to the menu");
        Console.WriteLine("Try to stick with the format of the example here: \"example@example.com\"");
        string tempMail = Console.ReadLine();
        if (tempMail == "0")
        {
            menu.DisplayMenu();
            return;
        }
        while (!validation.CheckEmail(tempMail))
        {
            Console.WriteLine("There was an error with the e-mail address you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            tempMail = Console.ReadLine();
            if (tempMail == "0")
            {
                menu.DisplayMenu();
                return;
            }
        }
        string email = tempMail;

        var contact = new Contact { Name = name, PhoneNumber = phoneNumber, Email = email };
        using (var context = new PhonebookContext())
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
        Console.WriteLine("Contact successfully added to the list. Pressing any key will return you to the main menu...");
        Console.ReadLine();
        menu.DisplayMenu();
    }


    public void ShowContacts()
    {
        GetContacts();
        Console.WriteLine("\nPressing any key will return you to the main menu...");
        Console.ReadLine();
        menu.DisplayMenu();
    }

    public void GetContacts()
    {
        Console.Clear();
        Console.WriteLine("\nHere's your list of contacts:\n");
        try
        {
            using (var context = new PhonebookContext())
            {
                List<Contact> list = context.Contacts.ToList();

                if (list.Any())
                {
                    foreach (var contact in list)
                    {
                        Console.WriteLine($"The contact's id is {contact.Id}, name: {contact.Name}, phone number: {contact.PhoneNumber}, e-mail address: {contact.Email}.");
                    }
                }
                else
                {
                    Console.WriteLine("Your contact list is currently empty. Try adding some information first with the Add contact option in the menu (option 2)!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void UpdateContact()
    {
        GetContacts();
        int id;
        Console.WriteLine("\nPlease type in the id of the contact you want to update or press '0' to return to the menu");
        string idString=Console.ReadLine();
        if(validation.CheckNumber(idString))
        {
            id = Int32.Parse(idString);
        }
        else
        {
            Console.WriteLine("The given data is not a number. Press any key to return to the menu...");
            Console.ReadLine();
            menu.DisplayMenu();
            return;
        }
        if(id==0)
        {
            menu.DisplayMenu();
        }
        using (var context = new PhonebookContext())
        {
            var searchedContact = context.Contacts.FirstOrDefault(c => c.Id == id);
            if (searchedContact != null)
            {
                Console.WriteLine("Please type in the name of the contact or leave empty if you don't want to modify it:");
                Console.WriteLine("Example: John Doe or John");
                string newName = Console.ReadLine();
                if (!validation.CheckName(newName))
                {
                    Console.WriteLine("Name not in acceptable format! Press any key to try again from the beginning...");
                    Console.ReadLine();
                    UpdateContact();
                    return;
                }
                Console.WriteLine("Please type in the phone number of the contact or leave empty if you don't want to modify it:");
                Console.WriteLine("Example: +36201234567 or 06201234567");
                string phoneNumber = Console.ReadLine();
                if (!validation.CheckPhoneNumber(phoneNumber))
                {
                    Console.WriteLine("Phone number not in acceptable format! Press any key to try again from the beginning...\"");
                    Console.ReadLine();
                    UpdateContact();
                    return;
                }
                Console.WriteLine("Please type in the e-mail address of the contact or leave empty if you don't want to modify it:");
                Console.WriteLine("Example: \"example@example.com\"");
                string email = Console.ReadLine();
                if (!validation.CheckEmail(email))
                {
                    Console.WriteLine("Email not in acceptable format! Press any key to try again from the beginning...\".");
                    Console.ReadLine();
                    UpdateContact();
                    return;
                }
                if (!String.IsNullOrEmpty(newName)&&validation.CheckName(newName))
                {
                    searchedContact.Name = newName;
                }
                if(!String.IsNullOrEmpty(phoneNumber)&&validation.CheckPhoneNumber(phoneNumber))
                {
                    searchedContact.PhoneNumber = phoneNumber;
                }
                if(!String.IsNullOrEmpty(email)&&validation.CheckEmail(email))
                {
                    searchedContact.Email = email;
                }
                context.Contacts.Update(searchedContact);
                context.SaveChanges();
                Console.WriteLine("Contact updated successfully! Pressing any key will return you to the main menu...");
                Console.ReadLine();
                menu.DisplayMenu();
                return;
            }
            else
            {
                Console.WriteLine("Can't find any contact with the given id. Pressing any key will return you to the main menu...");
                Console.ReadLine();
                menu.DisplayMenu();
                return;
            }
            
        }
    }
    public void DeleteContact()
    {
        GetContacts();
        Console.WriteLine("\nPlease enter the id of the contact you want to delete:\n");
        int id=Int32.Parse(Console.ReadLine());
        using(var context = new PhonebookContext())
        {
            var contact = context.Contacts.Find(id);
            if (contact != null)
            {
                Console.WriteLine("Are you sure you want to delete the contact? Y/N");
                string confirmation=Console.ReadLine();
                if (confirmation.ToLower()=="y")
                {
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                    Console.WriteLine("Contact deleted successfully! Pressing any key will return you to the main menu...");
                    Console.ReadLine();
                    menu.DisplayMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("Contact removal cancelled, press any key to return to the menu...");
                    Console.ReadLine();
                    menu.DisplayMenu();
                    return;
                }
            }
            else
            {
                Console.WriteLine("No contact found with the id. Try again:");
                DeleteContact();
            }
        }
    }
}