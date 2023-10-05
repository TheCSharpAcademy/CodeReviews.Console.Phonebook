using PhoneBook.Data;

namespace PhoneBook
{
    public class Manager
    {
        private DataService Service;
        private UI Ui;
        private SELECTOR Selector;

        public Manager()
        {
            Service = new DataService();
            Ui = new UI();
            Selector = Ui.MainMenu();

            while (true)
            {
                Action();
            }
        }
        private void Action()
        {
            switch (Selector)
            {
                case SELECTOR.CREATE:
                    CreateContact();
                    break;
                case SELECTOR.READ:
                    ReadContact();
                    break;
                case SELECTOR.UPDATE:
                    UpdateContact();
                    break;
                case SELECTOR.DELETE:
                    DeleteContact();
                    break;
                case SELECTOR.VIEWALL:
                    ViewAllContact();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    UI.Write("Invalid Input");
                    break;
            }
            Selector = Ui.GoToMainMenu("Type any keys to continue.");
        }

        private void ViewAllContact()
        {
            UI.Clear();

            try
            {
                var contact = Service.Contacts;
                var contactData = new List<List<object>>();
                foreach (var c in contact)
                {
                    contactData.Add(new List<object> { c.Id, c.Name, c.Email, c.PhoneNumber } );

                }
                UI.MakeTable(contactData, "All Contacts");
            }
            catch
            {
                UI.Write("Error.");
            }
        }

        private void CreateContact()
        {
            UI.Clear();
            try
            {
                var name = Ui.GetInput("Type a name.").str;
                var email = Validation.CheckEmail(Ui.GetInput("Type an email address. (myname@email.com)").str);
                var phoneNumber = Validation.CheckPhoneNumber(Ui.GetInput("Type a phone number. (000-0000-0000)").str);

                var contacts = Service.Contacts;
                contacts.Add(new Model.Contact() { Name = name, Email = email, PhoneNumber = phoneNumber });
                Service.SaveChanges();

                UI.Write("Contact added.");
            }
            catch (Exception ex)
            {
                UI.Write(ex.Message);
            }
        }
        private void ReadContact()
        {
            UI.Clear();
            var name = Ui.GetInput("Type a name to read.").str;

            try
            {
                var contact = Service.Contacts.Where(c => c.Name == name).First();
                var contactData = new List<List<object>>() { new List<object> { contact.Id, contact.Name, contact.Email, contact.PhoneNumber } };
                UI.MakeTable(contactData, "Contact");
            }
            catch
            {
                UI.Write("There is no such a name.");
            }

        }
        private void UpdateContact()
        {
            ViewAllContact();
            var name = Ui.GetInput("Type a name to update.").str;

            try
            {
                var contact = Service.Contacts.Where(c => c.Name == name).First();
                contact.Email = Validation.CheckEmail(Ui.GetInput("Type an email address. (myname@email.com)").str);
                contact.PhoneNumber = Validation.CheckPhoneNumber(Ui.GetInput("Type a phone number. (000-0000-0000)").str);
                Service.SaveChanges();
                var contactData = new List<List<object>>() { new List<object> { contact.Id, contact.Name, contact.Email, contact.PhoneNumber } };
                UI.MakeTable(contactData, "Contact");
            }
            catch (Exception ex)
            {
                UI.Write(ex.Message);
            }

        }
        private void DeleteContact()
        {
            ViewAllContact();
            var name = Ui.GetInput("Type a name to delete.").str;

            try
            {
                var contact = Service.Contacts.Where(c => c.Name == name).First();
                Service.Contacts.Remove(contact);
                Service.SaveChanges();
                UI.Write("Contact deleted.");
            }
            catch
            {
                UI.Write("There is no such a name.");
            }
        }
    }
}
