Phonebook
=========

Allows users to create, read, update and delete contacts. Contacts can
also be assigned to categories e.g. “Friends”, “Coworkers” etc. Contacts
can be part of multiple categories.

Run locally
-----------

Pre-requisites & Configuration
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

-  SQL server local DB must be running locally
-  Database details configurable via ``Program/appsettings.json``
-  Database & tables will automatically be created on app startup

Run
~~~

-  Clone this repo and ``cd`` into it
-  ``dotnet run``

Features
--------

-  View Contacts
-  Add New Contact

   -  Assign contact to category(s)

-  Edit Contact

   -  Assign contact to category(s)

-  Delete Contact
-  View Categories
-  Add New Category
-  Edit Category
-  Delete category

Tech stack
----------

-  C#
-  SQL Server
-  EF Core ORM