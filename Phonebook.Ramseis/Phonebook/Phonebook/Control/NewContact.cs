namespace Phonebook;
internal class NewContact
{
    public static void Menu()
    {
        Menu menu = new Menu
        {
            Titles = new List<string>
            {
                "New Contact"
            },
            Fields = new List<string>
            {
                "Name",
                "Phone",
                "Address",
                "City",
                "State",
                "Zip",
                "Email",
                "Group"
            }
        };

        bool run = true;

        menu.Draw();

        string name = menu.GetFieldString(0);
        while (run)
        {
            if (name.Length == 0)
            {
                menu.Message = "Name must contain at least 1 character.";
                menu.Draw();
                name = menu.GetFieldString(0);
            }
            else if (Validate.Name(name))
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Name already exists.";
                menu.Draw();
                name = menu.GetFieldString(0);
            }
        }

        run = true;
        string phone = menu.GetFieldString(1);
        while (run)
        {
            phone = Validate.Phone(phone);
            if (phone.Length > 0)
            {
                run = false;
                menu.Message = "";
                menu.FieldString[1] = phone;
                menu.Draw();
            } else
            {
                menu.Message = "Please enter 10-digit phone number. Omit country code.";
                menu.Draw();
                phone = menu.GetFieldString(1);
            }
        }
                
        string address = menu.GetFieldString(2);
        string city = menu.GetFieldString(3);
        string state = menu.GetFieldString(4);

        run = true;
        int zip = menu.GetFieldInt(5);
        while (run)
        {
            if (Validate.Zip(zip))
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Please enter only 5-digit zip code.";
                menu.Draw();
                zip = menu.GetFieldInt(5);
            }
        }

        run = true;
        string email = menu.GetFieldString(6);
        while (run)
        {
            if (Validate.Email(email))
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Please enter a valid email. (e.g. ____@____.__)";
                menu.Draw();
                email = menu.GetFieldString(6);
            }
        }

        string group = menu.GetFieldString(7);

        Controller.AddContact(new Contact
            {
                Name = name,
                Phone = phone,
                Address = address,
                City = city,
                State = state,
                ZipCode = zip,
                Email = email,
                LastAccess = DateTime.Now,
                Group = group
            }
        );
    }
}
