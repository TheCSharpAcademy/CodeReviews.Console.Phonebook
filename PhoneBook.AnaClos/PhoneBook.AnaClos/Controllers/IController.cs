using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.AnaClos.Controllers;

public interface IController
{
    public void Add();

    public void Delete();

    public void Update();

    public void View();

    public void ViewAll();
}
