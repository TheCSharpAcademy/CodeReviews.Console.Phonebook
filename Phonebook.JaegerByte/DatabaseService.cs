namespace Phonebook.JaegerByte
{
    internal class DatabaseService
    {
        public void InsertContact(DataModel contact)
        {
            using (PhonebookDataContext context = new PhonebookDataContext())
            {
                context.Add(contact);
                context.SaveChanges();
            }
        }
        public void DeleteContact(DataModel contact)
        {
            using (PhonebookDataContext context = new PhonebookDataContext())
            {
                context.Remove(contact);
                context.SaveChanges();
            }
        }
        public void UpdateContact(DataModel contact)
        {
            using (PhonebookDataContext context = new PhonebookDataContext())
            {
                context.Update(contact);
                context.SaveChanges();
            }
        }
        public List<DataModel> GetAllContacts()
        {
            using (PhonebookDataContext context = new PhonebookDataContext())
            {
                return context.TblPhonebook
                    .OrderBy(p => p.FirstName)
                    .ToList();
            }
        }
        public List<DataModel> SearchContacts(string searchKeyword)
        {
            using (PhonebookDataContext context = new PhonebookDataContext())
            {
                return context.TblPhonebook
                    .Where(p => p.FirstName == searchKeyword ||
                                p.LastName == searchKeyword ||
                                p.Phonenumber == searchKeyword ||
                                p.Email == searchKeyword)
                    .ToList();
            }
        }
    }
}
