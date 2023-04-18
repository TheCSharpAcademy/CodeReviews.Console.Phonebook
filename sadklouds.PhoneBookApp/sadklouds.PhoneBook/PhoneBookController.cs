using sadklouds.PhoneBook.DataAccess;
using sadklouds.PhoneBook.Models;

namespace sadklouds.PhoneBook
{
    public class PhoneBookController
    {
        private ContactContext db = new ContactContext();

        public void CreateContact()
        {
            string name = UserInput.GetContactName("Enter contact name: ");
            string phoneNumber = UserInput.GetContactNumber();
            string email = UserInput.GetContactEmail();
            var c = new Contact
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email
            };
            using (var db = new ContactContext())
            {
                db.Contacts.Add(c);
                db.SaveChanges();
            }
        }

        public void DeleteContact()
        {
            string name = UserInput.GetContactName("Enter contact name: ");

            using (var db = new ContactContext())
            {
                var contact = db.Contacts.Where(c => c.Name == name).FirstOrDefault();
                var records = db.Contacts.ToList();
                if (contact != null)
                {
                    db.Remove(contact);
                    db.SaveChanges();
                }

                else
                {
                    Console.WriteLine($"Contact {name} could not be found");
                }

            }
        }

        public void GetContact(string name)
        {
            using (var db = new ContactContext())
            {
                var contact = db.Contacts.Where(c => c.Name == name).FirstOrDefault();
                var records = db.Contacts.ToList();
                if (contact != null)
                {
                    foreach (var c in records)
                    {
                        Console.WriteLine($"");
                        Console.WriteLine($"_____{c.Name}_____");
                        Console.WriteLine($"Number: {c.PhoneNumber}");
                        Console.WriteLine($"Email: {c.Email}");
                        Console.WriteLine("__________________");
                    }
                }
                else
                {
                    Console.WriteLine($"Contact {name} could not be found");
                }

            }
        }

        public void ReadAll()
        {
            using (var db = new ContactContext())
            {
                var records = db.Contacts.ToList();
                foreach (var c in records)
                {
                    Console.WriteLine($"Contact: {c.Name}");
                    Console.WriteLine("_____________");
                }
            }
        }

        public void UpdateContact()
        {
            string currentName = UserInput.GetContactName("Enter contact name: ");

            Console.Clear();
            GetContact(currentName);

            using (var db = new ContactContext())
            {
                var contact = db.Contacts.Where(c => c.Name == currentName).FirstOrDefault();
                if (contact != null)
                {
                    Console.WriteLine("Update Name(n), Phone number(p), Email(e), All(a) or any other key go back: ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "n")
                    {
                        contact.Name = UserInput.GetContactName("Enter new contact name: ");
                        db.SaveChanges();
                    }
                    else if (input.ToLower() == "p")
                    {
                        contact.PhoneNumber = UserInput.GetContactNumber();
                        db.SaveChanges();
                    }
                    else if (input.ToLower() == "e")
                    {
                        contact.Email = UserInput.GetContactEmail();
                        db.SaveChanges();
                    }
                    else if (input.ToLower() == "a")
                    {
                        contact.Name = UserInput.GetContactName("Enter new contact name: ");
                        contact.PhoneNumber = UserInput.GetContactNumber();
                        contact.Email = UserInput.GetContactEmail();
                        db.SaveChanges();
                    }
                    else return;
                }
                else
                {
                    Console.WriteLine("\nContact name does not exist\n");
                }

            }
        }

    }




}
