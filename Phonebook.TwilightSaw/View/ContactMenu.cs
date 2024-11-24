using Spectre.Console;
using TwilightSaw.Phonebook.Controller;
using TwilightSaw.Phonebook.Helpers;
using TwilightSaw.Phonebook.Model;

namespace TwilightSaw.Phonebook.View;

public class ContactMenu(ContactController contactController, EmailController emailController, MessageController messageController)
{
    public void Menu(Contact contact, bool isDeleted)
    {
        while (true)
        {
            Console.Clear();
            if (isDeleted) break;
            AnsiConsole.Write(new Panel(new Markup($"[seagreen1][bold]{contact.Name}\n{contact.Email}\n{contact.PhoneNumber}[/][/]").Centered()).Expand());
            var inputContact = UserInput.CreateChoosingList(["Send SMS", "Send email", "Change contact details", "Delete contact"], "Return");
            if (inputContact == "Return") break;
            switch (inputContact)
            {
                case "Send email":
                    SendEmail(contact);
                    break;
                case "Send SMS":
                    SendMessage(contact);
                    break;
                case "Delete contact":
                    DeleteContact(contact, out isDeleted);
                    break;
                case "Change contact details":
                    UpdateContact(contact);
                    break;
            }
        }
    }

    private void SendMessage(Contact contact)
    {
        var message = UserInput.Create("Write your text");
        if (message == "0") return;
        AnsiConsole.Write(new Markup(Validation.Validate(() => messageController.SendMessage(contact, message), true)));
        Validation.EndMessage("");
    }

    protected void SendEmail(Contact contact)
    {
        var head = UserInput.Create("Write your subject");
        if (head == "0") return;
        var message = UserInput.Create("Write your text");
        if (message == "0") return;
        emailController.SendEmail(contact, message, head);
        Validation.EndMessage("Sent successfully");
    }

    protected void DeleteContact(Contact contact, out bool isDeleted) 
    {
        contactController.Delete(contact);
        isDeleted = true;
        Validation.EndMessage("Contact successfully deleted.");
    }

    protected void UpdateContact(Contact contact)
    {
        var inputUpdate = UserInput.CreateChoosingList(["Name", "Phone Number", "Email"], "Return");
        if (inputUpdate == "Return") return;
        var updatedContactAttribute = UserInput.UpdateContact(inputUpdate);

        var validateUpdate = Validation.Validate(() => contactController.Update(contact, inputUpdate, updatedContactAttribute), false);
        if (validateUpdate == "") return;
        Validation.EndMessage("Executed successfully");
    }
}