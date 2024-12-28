# Console PhoneBook
## Description
This program allows you to store contacts, with their phone numbers and email.
## Categories
Categories were added to group contacts.
## Database
For the program to work, a database must be created in SQL Server. In this case, Windows credentials were used.
## Configuration
I was unable to perform the migrations by reading the ConnectionString from the appsettings.json file. I had to add it to the DataBaseController class constructor.
The person who wants to test or use this program must modify the ConnectionString in both places and adapt it to their SQLServer configuration.
## Problems
The checker doesn't like the Readme.md file. I had to rename it to Readme.txt.
The checker doesn't like using System.Net; I had to prepend the library name to the methods that use it.

