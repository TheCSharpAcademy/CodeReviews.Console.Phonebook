using Phonebook.tonyissa.Context;
using Phonebook.tonyissa.Repositories;
using Phonebook.tonyissa.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.tonyissa.Services;
public static class PhonebookService
{
    public static async Task GetAllContactsAsync()
    {
        using var context = new PhonebookContext();
        var list = await PhonebookRepository.GetAllEntriesAsync(context);
        MenuController.PrintContactList(list);
    }
}