# Phonebook Console App

## Project Repository
[j-nas/phonebook](https://github.com/j-nas/phonebook)

## Requirements
-   Dotnet 7.0
-   SQL Server
-   [EF Core CLI](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

## Instructions

1.  Clone the repo
2.  Set up DB connection string in [secrets.json](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=linux):
```json
{
  "ConnectionStrings": {
    "PhonebookDatabase": "[your connection string here]"
  }
}
```
3.  Run `dotnet ef database update` to create the database
4.  Run `dotnet run` to start the app
