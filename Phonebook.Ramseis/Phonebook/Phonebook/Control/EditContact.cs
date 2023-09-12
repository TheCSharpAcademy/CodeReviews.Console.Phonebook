namespace Phonebook.Control;

internal class EditContact
{
    public static Contact Menu(Contact contact)
    {
        Menu menu = new Menu
        {
            Titles = new List<string>
            {
                "Update Contact"
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
            },
            FieldString = {
                contact.Name,
                contact.Phone,
                contact.Address,
                contact.City,
                contact.State,
                contact.ZipCode.ToString(),
                contact.Email,
                contact.Group
            }
        };

        bool run = true;

        menu.Draw();

        string name = menu.UpdateFieldString(0);
        while (run)
        {
            if (name.Length == 0)
            {
                menu.Message = "";
                name = contact.Name;
                menu.FieldString[0] = name;
                run = false;
                menu.Draw();
            }
            else if (Validate.Name(name) | Controller.GetContactName(name) == contact)
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Name already exists.";
                menu.Draw();
                name = menu.UpdateFieldString(0);
            }
        }

        run = true;
        string phone = menu.UpdateFieldString(1);
        while (run)
        {
            phone = Validate.Phone(phone);
            if (phone.Length > 0)
            {
                run = false;
                menu.Message = "";
                menu.FieldString[1] = phone;
                menu.Draw();
            }
            else if (phone.Length == 0)
            {
                phone = contact.Phone;
                menu.FieldString[1] = phone;
                menu.Message = "";
                menu.Draw();
            }
            else
            {
                menu.Message = "Please enter 10-digit phone number. Omit country code.";
                menu.Draw();
                phone = menu.UpdateFieldString(1);
            }
        }

        string address = menu.UpdateFieldString(2);
        if (address.Length == 0)
        { 
            address = contact.Address;
            menu.FieldString[2] = address;
            menu.Draw();
        }
        
        string city = menu.UpdateFieldString(3);
        if (city.Length == 0)
        {
            city = contact.City;
            menu.FieldString[3] = city;
            menu.Draw();
        }
        
        string state = menu.UpdateFieldString(4);
        if (state.Length == 0) 
        {  
            state = contact.State;
            menu.FieldString[4] = state;
            menu.Draw();
        }

        run = true;
        int zip = menu.UpdateFieldInt(5);
        while (run)
        {
            if (zip == 0) {
                zip = contact.ZipCode;
                menu.FieldString[5] = zip.ToString();
                menu.Message = "";
                menu.Draw();
                run = false;
            }
            else if (Validate.Zip(zip))
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Please enter only 5-digit zip code.";
                menu.Draw();
                zip = menu.UpdateFieldInt(5);
            }
        }

        run = true;
        string email = menu.UpdateFieldString(6);
        while (run)
        {
            if (email.Length == 0)
            {
                email = contact.Email;
                menu.FieldString[6] = email;
                menu.Message = "";
                menu.Draw();
                run = false;
            }
            else if (Validate.Email(email))
            {
                run = false;
                menu.Message = "";
            }
            else
            {
                menu.Message = "Please enter a valid email.";
                menu.Draw();
                email = menu.UpdateFieldString(6);
            }
        }

        string group = menu.UpdateFieldString(7);
        if (group.Length == 0) 
        { 
            group = contact.Group; 
            menu.FieldString[7] = group;
            menu.Draw();
        }
        Contact update = new Contact
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
        };
        Controller.UpdateContact(contact, update);

        return update;
    }
}
