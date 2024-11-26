# Phonebook Application

## Overview
The Phonebook application is a simple C# project designed to manage contact information. It allows users to perform CRUD (Create, Read, Update, Delete) operations on a list of contacts, making it easy to store and retrieve important contact details. The application leverages Entity Framework Core, an Object-Relational Mapper (ORM), which simplifies the process of interacting with the database.

### NOTE - User must set up the connection string in App.Config to point to your local SQL Server database (LocalDB) for the application to connect properly.

### NOTE - User must set up a mail server (hmailserver recommended free and open source) and add to emailService class the following:

- Line 16: new SmtpClient - add host
- Line 19: new NetworkCredentail - add username and password as two strings
- Line 25: new MailAddress - add from email address

## Prerequisites

- **.NET Core SDK**: To build and run the application.
- **Entity Framework**: Used for managing the database.

## Setting Up the Application for Local Development

To run the application locally, you need to complete the following steps:

1. **Set Up the Connection String**: Update the connection string in **App.Config** to point to your LocalDB instance. This is necessary for the application to connect to the database properly. Ensure that the connection string matches your local database configuration.

2. **Install Dependencies**: Ensure that the necessary dependencies are installed, including the .NET Core SDK and Entity Framework tools.

3. **Run Database Migrations**: Use the `dotnet ef database update` command to apply migrations and set up the local database schema.

1. **Clone the repository**:

   ```sh
   git clone <repository-url>
   ```

2. **Navigate to the project directory**:

   ```sh
   cd Phonebook
   ```

3. **Restore dependencies**:

   ```sh
   dotnet restore
   ```

4. **Update the database** (if applicable):

   ```sh
   dotnet ef database update
   ```

## Running the Application

To run the Phonebook application, use the following command in the root project directory:

```sh
   dotnet run
```

This will start the application, and you can interact with it via the console or a graphical user interface, depending on the implemented view.

## Features

- **View Contacts**: View all saved contacts.
- **Create Contact**: Add a new contact to the phonebook.
- **Update Contact**: Update details of an existing contact.
- **Delete Contact**: Remove a contact from the phonebook.
- **Send Email**: From the application the user is able to send email
- **View Contacts with Filters**: Filter the contact list based on category of contact either Friends/Family/Work.

## Technologies Used

- **C# and .NET Core**: For the core application logic.
- **Entity Framework**: For data access and database management.
- **System.Net.Mail**: Used to implement email sending functionality within the application.


## Lessons Learned
- **Importance of Database Design**: Structuring the database properly from the beginning is crucial for scaling and maintaining the application efficiently. Entity Framework Core provided helpful tools, but understanding the underlying SQL was also essential.
- **Modular Code Structure**: Organizing the code into separate services, models, and views helped to maintain a clean and readable codebase. This modular structure made it easier to test and extend different components.
- **Working with ORMs**: Using Entity Framework Core helped to streamline database interactions, but it also introduced challenges, such as managing migrations effectively.


## Challenges
- **Working with Entity**: For the first time presented challenges, particularly in understanding how to correctly set up the models. It took some time to realize that the primary key annotation needed to be placed directly above the property definition to function properly.
- **Email Functionality**: Integrating email functionality using System.Net.Mail had its own set of challenges, including handling SMTP server configurations.
- **Error Handling**: Ensuring robust error handling throughout the application, especially for database operations, was more complex than initially anticipated.




