
# PhoneBook Application

This is a console-based PhoneBook application built using C# 
and Entity Framework Core (EF Core).
The app allows users to perform CRUD (Create, Read, Update, Delete) 
operations on contacts, including their name, phone number, and email.

## Features

- **Add Contact**: Add a new contact with name, phone number, and email.
- **Remove Contact**: Delete an existing contact.
- **Show All Contacts**: Display all the contacts in the phonebook.
- **Show Contact**: Display a specific contact based on the name.
- **Update Contact**: Update an existing contact's details.

## Technologies Used

- C#
- .NET 6 or later
- Entity Framework Core
- AnsiConsole (for command-line interface)

## Requirements

Before running the application, ensure the following:

1. **.NET SDK** is installed. You can download it from [here](https://dotnet.microsoft.com/download).
2. **SQL Server**  should be set up.

## Installation

1. Clone the repository:

    ```bash

    git clone https://github.com/Bina28/CodeReviews.Console.Phonebook.git
    cd PhoneBook

    ```

2. Restore dependencies:

    ```bash

    dotnet restore

    ```

3. Update the connection string:
    - Open the `PhoneBookContext` class and modify the connection 
    string inside the `OnConfiguring` method:
    
    ```csharp

    optionsBuilder.UseSqlServer("Server=your_server_name;Database=your_database_name;Trusted_Connection=True;");

    ```

    Replace `your_server_name` with the appropriate server address and
    `your_database_name` with the database you want to use.
    Make the same in the appsettings.json.

4. Run migrations to create the database and schema:

    ```bash

    dotnet ef database update

    ```

## Running the Application

To run the application:

1. Open a terminal in the project folder.
2. Run the application:

    ```bash

    dotnet run

    ```

3. Follow the on-screen prompts to interact with the application 
(add, remove, update, view contacts).

## Commands

Here are the available options in the application:

1. **AddData**: Add a new contact to the phonebook.
2. **RemoveData**: Remove an existing contact.
3. **ShowAllData**: Show all contacts in the phonebook.
4. **Show Data**: Search for a specific contact by name.
5. **UpdateData**: Update an existing contact’s information.
6. **Exit**: Exit the application.

