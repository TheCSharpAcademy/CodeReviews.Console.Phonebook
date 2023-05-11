using ConsoleTableExt;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;

namespace PhoneBook;

internal class UserInputs
{
    // static int stackID;
    static bool deleteContact;
    static bool editContact;
    internal static void MainMenu()
    {
        Console.Clear();
        bool closeApp = false;

        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 See Phone Book Records.");
            Console.WriteLine("Type 2 to Enter a New Rocord.");
            Console.WriteLine("Type 3 to Edit a Record");
            Console.WriteLine("Type 4 to Delete a Record");
            Console.WriteLine("Type 5 to Send an Email to a Rocord");
            Console.WriteLine("------------------------------------------\n");
            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    ShowPhoneBook();
                    break;
                case "2":
                    EnterNewContact();
                    // GetAllRecords();
                    break;
                case "3":
                    editContact = true;
                    ShowPhoneBook();
                    break;
                case "4":
                    deleteContact = true;
                    ShowPhoneBook();
                    //ChooseStack();
                    break;
                case "5":
                    ChooseContactToSendEmail();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                    break;
            }
        }
    }

    static void ShowPhoneBook()
    {
        DataContext dataContext = new DataContext(Data.SQLData);
        var users = dataContext.Users.ToList();
        var userPermissions = dataContext.UserPermissions.ToList();

        if (users.Any())
        {
            var tableData = users.Select((u, index) => new
            {
                Id = index + 1,
                u.FirstName,
                u.LastName,
                u.PhoneNumber,
                u.Email,
                u.DateCreated
            }).ToList();

            ConsoleTableBuilder.
               From(tableData).
               WithFormat(ConsoleTableBuilderFormat.Alternative).
               ExportAndWriteLine(TableAligntment.Center);

            if (deleteContact)
            {
                ChooseDeleteContact();
            }

            if (editContact)
            {
                ChooseEditContact();
            }
        }
    }

    static void EnterNewContact()
    {
        Console.WriteLine("Enter new contact details:");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        if (!ConfirmInput("First Name", firstName)) return;

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        if (!ConfirmInput("Last Name", lastName)) return;

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();
        if (!ConfirmInput("Phone Number", phoneNumber)) return;

        Console.Write("Email: ");
        string email = Console.ReadLine();
        if (!ConfirmInput("Email", email)) return;

        DataContext dataContext = new DataContext(Data.SQLData);

        User user = new User()
        {
            LastName = lastName,
            FirstName = firstName,
            PhoneNumber = phoneNumber,
            Email = email,
            DateCreated = DateTime.Now,
        };

        dataContext.Users.Add(user);
        dataContext.SaveChanges();

        UserPermission userPermission = new UserPermission()
        {
            Name = "Admin",
            UserId = 1
        };

        dataContext.UserPermissions.Add(userPermission);
        dataContext.SaveChanges();

        Console.WriteLine("New contact added successfully!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();

        MainMenu();
    }

    static bool ConfirmInput(string fieldName, string value)
    {
        while (true)
        {
            Console.Write($"Please confirm {fieldName}: {value} (y to confirm, n to re-enter, or c to cancel): ");

            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                return true;
            }
            else if (confirmation.ToLower() == "n")
            {
                Console.Write($"Please re-enter {fieldName}: ");
                value = Console.ReadLine();
            }
            else if (confirmation.ToLower() == "c")
            {
                Console.WriteLine("Operation cancelled.");
                return false;
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter y, n, or c.");
            }
        }
    }

    static void ChooseDeleteContact()
    {
        deleteContact = false;
        using (var dataContext = new DataContext(Data.SQLData))
        {
            var contacts = dataContext.Users.Select(u => new { u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.DateCreated }).ToList();

            Console.WriteLine("Available contacts:");
            int counter = 1;
            var contactNumberToIdMap = new Dictionary<int, int>();
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{counter}. {contact.FirstName} {contact.LastName} ({contact.Email})");
                contactNumberToIdMap.Add(counter, contact.Id);
                counter++;
            }

            Console.WriteLine("\nEnter the number of the contact to delete (or enter 'b' to go back to the main menu): \n\n");

            string input = Console.ReadLine();

            if (input.ToLower() == "b")
            {
                MainMenu();
            }
            else if (!int.TryParse(input, out int selectedContactNumber) || !contactNumberToIdMap.ContainsKey(selectedContactNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            else
            {
                int selectedContactId = contactNumberToIdMap[selectedContactNumber];
                var contactToDelete = dataContext.Users.FirstOrDefault(u => u.Id == selectedContactId);

                if (contactToDelete != null)
                {
                    Console.Write($"Are you sure you want to delete {contactToDelete.FirstName} {contactToDelete.LastName}? (y/n)");
                    string confirmation = Console.ReadLine();
                    if (confirmation.ToLower() == "y")
                    {
                        dataContext.Users.Remove(contactToDelete);
                        dataContext.SaveChanges();
                        Console.WriteLine($"Contact {contactToDelete.FirstName} { contactToDelete.LastName} has been deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine($"Contact with ID {selectedContactId} does not exist.");
                }

                // Return to the main menu
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                MainMenu();
            }
        }
    }

    static void ChooseEditContact()
    {
        editContact = false;

        using (var dataContext = new DataContext(Data.SQLData))
        {
            var contacts = dataContext.Users.Select(u => new { u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.DateCreated }).ToList();

            Console.WriteLine("Available contacts:");
            int counter = 1;
            var contactNumberToIdMap = new Dictionary<int, int>();
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{counter}. {contact.FirstName} {contact.LastName} ({contact.Email})");
                contactNumberToIdMap.Add(counter, contact.Id);
                counter++;
            }

            Console.WriteLine("\nEnter the number of the contact to edit (or enter 'b' to go back to the main menu): \n\n");

            string input = Console.ReadLine();

            if (input.ToLower() == "b")
            {
                MainMenu();
            }
            else if (!int.TryParse(input, out int selectedContactNumber) || !contactNumberToIdMap.ContainsKey(selectedContactNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            else
            {
                int selectedContactId = contactNumberToIdMap[selectedContactNumber];
                var contactToEdit = dataContext.Users.FirstOrDefault(u => u.Id == selectedContactId);

                if (contactToEdit != null)
                {
                    Console.WriteLine($"Editing contact: {contactToEdit.FirstName} {contactToEdit.LastName}");

                    Console.Write("Enter new first name (leave blank to keep existing value): ");
                    string newFirstName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newFirstName))
                    {
                        contactToEdit.FirstName = newFirstName;
                    }

                    Console.Write("Enter new last name (leave blank to keep existing value): ");
                    string newLastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newLastName))
                    {
                        contactToEdit.LastName = newLastName;
                    }

                    Console.Write("Enter new phone number (leave blank to keep existing value): ");
                    string newPhoneNumber = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newPhoneNumber))
                    {
                        contactToEdit.PhoneNumber = newPhoneNumber;
                    }

                    Console.Write("Enter new email (leave blank to keep existing value): ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEmail))
                    {
                        contactToEdit.Email = newEmail;
                    }

                    dataContext.SaveChanges();
                    Console.WriteLine($"Contact {contactToEdit.FirstName} {contactToEdit.LastName} has been updated.");
                }
                else
                {
                    Console.WriteLine($"Contact with ID {selectedContactId} does not exist.");
                }

                // Return to the main menu
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                MainMenu();
            }
        }
    }

    static void ChooseContactToSendEmail()
    {
        using (var dataContext = new DataContext(Data.SQLData))
        {
            var contacts = dataContext.Users.Select(u => new { u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.DateCreated }).ToList();

            Console.WriteLine("Available contacts:");
            int counter = 1;
            var contactNumberToIdMap = new Dictionary<int, int>();

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{counter}. {contact.FirstName} {contact.LastName} ({contact.Email})");
                contactNumberToIdMap.Add(counter, contact.Id);
                counter++;
            }

            Console.WriteLine("\nEnter the number of the contact to send an email to (or enter 'b' to go back to the main menu): \n\n");

            string input = Console.ReadLine();

            if (input.ToLower() == "b")
            {
                MainMenu();
            }
            else if (!int.TryParse(input, out int selectedContactNumber) || !contactNumberToIdMap.ContainsKey(selectedContactNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            else
            {
                int selectedContactId = contactNumberToIdMap[selectedContactNumber];
                var contactToSendEmail = dataContext.Users.FirstOrDefault(u => u.Id == selectedContactId);

                if (contactToSendEmail != null)
                {
                    Console.Write($"Are you sure you want to send an email to {contactToSendEmail.FirstName} {contactToSendEmail.LastName}? (y/n)");
                    string confirmation = Console.ReadLine();
                    if (confirmation.ToLower() == "y")
                    {

                        Console.Write("Enter the email subject: ");
                        string subject = Console.ReadLine();
                        Console.Write("Enter the email body: ");
                        string body = Console.ReadLine();

                        var senderEmail = Data.email;
                        var senderPassword = Data.password;


                        Console.WriteLine($"Sending email from: {senderEmail}");
                        Console.WriteLine($"Sending email to: {contactToSendEmail.Email}");
                        Console.Read();

                        var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient(host: "smtp.gmail.com")
                        {
                            EnableSsl = true,
                            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                            Port = 587,
                            Credentials = new NetworkCredential(senderEmail, senderPassword),
                        });

                        Email.DefaultSender = sender;
                        var email = Email
                            .From(emailAddress: senderEmail)
                            .To(emailAddress: contactToSendEmail.Email, name: $"{contactToSendEmail.FirstName} {contactToSendEmail.LastName}")
                            .Subject(subject: subject)
                            .Body(body: body)
                            .SendAsync().GetAwaiter().GetResult();
                        Console.WriteLine($"Email has been sent to {contactToSendEmail.FirstName} {contactToSendEmail.LastName} ({contactToSendEmail.Email})");
                    }
                }
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            UserInputs.MainMenu();
        }
    }
}