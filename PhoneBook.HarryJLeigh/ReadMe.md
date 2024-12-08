# Phonebook Application

## Overview

The Phonebook application is a simple C# project designed to manage contact information.
It allows users to perform CRUD (Create, Read, Update, Delete) operations
on a list of contacts,
making it easy to store and retrieve important details.
The application leverages Entity Framework Core, an Object-Relational Mapper (ORM),
which simplifies database interactions.

### Important Setup Notes

- **Database Connection**: You must configure the connection string
in `App.Config` to point to your local SQL Server database (LocalDB).
- **Mail Server Configuration**: 

You need to set up a mail 

server (hMailServer is recommended, as it is free and open source). 

- Update the following in the `EmailService` class:
  - **Line 16**: `new SmtpClient` - Add the SMTP host.
  - **Line 19**: `new NetworkCredential` - Add your username and password as strings.
  - **Line 25**: `new MailAddress` - Add the sender's email address.

## Prerequisites

- **.NET Core SDK**: Required to build and run the application.
- **Entity Framework Core**: Required for managing the database.

## Setting Up the Application for Local Development

To run the application locally, follow these steps:

1. **Set Up the Connection String**: Update the connection string in `App.Config`
to point to your
LocalDB instance.Ensure that it matches your local database configuration.

2. **Install Dependencies**: Ensure that the necessary dependencies are installed,
including the .NET Core SDK and Entity Framework tools.

3. **Run Database Migrations**: Use the following command to
apply migrations and set up the local database schema:

   ```sh
   dotnet ef database update
   ```
   
### Steps to Run the Application Locally

1. **Clone the Repository**:

   ```sh
   git clone <repository-url>
   ```

2. **Navigate to the Project Directory**:

   ```sh
   cd Phonebook
   ```

3. **Restore Dependencies**:

   ```sh
   dotnet restore
   ```

4. **Update the Database**:

   ```sh
   dotnet ef database update
   ```

## Running the Application

To run the Phonebook application, use the following command in the root project directory:

```sh
   dotnet run
```

This will start the application, allowing you to interact with it via
the console or a graphical user interface, depending on the implementation.

## Features

- **View Contacts**: View all saved contacts.
- **Create Contact**: Add a new contact to the phonebook.
- **Update Contact**: Update an existing contact.
- **Delete Contact**: Remove a contact from the phonebook.
- **Send Email**: Send emails directly from the application.

- **Filter Contacts**: Filter contacts				
        based on category (e.g., Friends, Family, Work).

## Technologies Used

- **C# and .NET Core**: Core application logic.
- **Entity Framework Core**: Data access and database management.
- **System.Net.Mail**: Email functionality.

## Lessons Learned

- **Database Design**:
Proper database structuring is crucial for scalability and maintainability.
Entity Framework Core simplifies interactions, but understanding SQL is also essential.
- **Modular Code Structure**:
Organizing code into services, models,
and views results in a clean, readable codebase.
It also makes testing and extending components easier.
- **Working with ORMs**:
Entity Framework Core simplifies database management
but introduces challenges like managing migrations effectively.

## Challenges Faced

- **Entity Framework Setup**:

Setting up models correctly was challenging initially, 
especially understanding the importance of correctly annotating primary keys.
- **Email Functionality**:
Integrating email features with `System.Net.Mail`
involved handling SMTP server configuration challenges.
- **Error Handling**:
Implementing robust error handling,
especially for database operations, was more complex than expected.

