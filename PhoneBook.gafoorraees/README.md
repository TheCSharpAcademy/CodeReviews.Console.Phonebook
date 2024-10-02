# Phone Book App

A small CRUD Console application that allows users to manage a phonebook of their contacts names, phone numbers, and email addresses. 

The purpose of this application was to learn how to use Entity Framework in conjunction with SQL server.

## Instructions

1. Install SQL Server and configure the connection string in `PhoneBookContext` to match your local SQL server instance: 
```c#
optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
```

2. If Entity Framework CLI is not already installed:
```c#
dotnet tool install --global dotnet-ef
```

3. Apply Migrations to create the database schema:
```c#
dotnet ef database update
```

3. Run the application
```c#
dotnet run
```
