using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.kjanos89
{
    public class PhonebookManipulation(Menu menu)
    {
        Validation validation=new Validation();
        public void AddContact()
        {
            Console.Clear();
            Console.WriteLine("Please type in the name of the contact or press '0' to return to the menu");
            string tempName=Console.ReadLine();
            if(tempName=="0")
            {
                menu.DisplayMenu();
            }
            while(!validation.CheckString(tempName))
            {
                Console.WriteLine("There was an error with the name you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            }
            string name = tempName;
            Console.WriteLine("Please type in the phone number of the contact or press '0' to return to the menu");
            string tempPhone=Console.ReadLine();
            if (tempPhone == "0")
            {
                menu.DisplayMenu();
            }
            while(!validation.CheckString(tempPhone))
            {
                Console.WriteLine("There was an error with the phone number you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            }
            string phoneNumber = tempPhone;
            Console.WriteLine("Please type in the e-mail address of the contact or press '0' to return to the menu");
            string tempMail=Console.ReadLine();
            if (tempMail == "0")
            {
                menu.DisplayMenu();
            }
            while(!validation.CheckString(tempMail))
            {
                Console.WriteLine("There was an error with the e-mail address you've typed in. Try again, pressing '0' will abort adding a new contact to your list.");
            }
            string email = tempMail;

            var contact= new Contact { Name=name, PhoneNumber=phoneNumber, Email=email };

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
                        Console.WriteLine("Your contact list is currently empty. Try adding some information first!");
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
            Console.WriteLine("\nPlease type in the id of the contact you want to update or press '0' to return to the menu");
            int id= Int32.Parse(Console.ReadLine());
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
                    string newName = Console.ReadLine();
                    Console.WriteLine("Please type in the phone number of the contact or leave empty if you don't want to modify it:");
                    string phoneNumber = Console.ReadLine();
                    Console.WriteLine("Please type in the e-mail address of the contact or leave empty if you don't want to modify it:");
                    string email = Console.ReadLine();
                    if(!String.IsNullOrEmpty(newName))
                    {
                        searchedContact.Name = newName;
                    }
                    if(!String.IsNullOrEmpty(phoneNumber))
                    {
                        searchedContact.PhoneNumber = phoneNumber;
                    }
                    if(!String.IsNullOrEmpty(email))
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
                    Console.WriteLine("Can't find contact with the given information.");
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
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                    Console.WriteLine("Contact deleted successfully! Pressing any key will return you to the main menu...");
                    Console.ReadLine();
                    menu.DisplayMenu();
                }
                else
                {
                    Console.WriteLine("No contact found with the id. Try again:");
                    DeleteContact();
                }
            }
        }

    }
}
