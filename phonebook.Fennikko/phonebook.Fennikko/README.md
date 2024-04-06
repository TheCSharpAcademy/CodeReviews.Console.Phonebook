# Phonebook
- Console based CRUD application to create and store contacts
- Developed using C# and MSSQL

# Given Requirements:
- [x] When application starts, it creates a MSSQL database if one is not present
- [x] Creates a database where contacts are made and stored
- [x] Shows a menu of options
- [x] Allows user to Add, delete, update, view a contact and category, and view a list of contacts and categories
- [x] Handles all errors so application doesn't crash
- [x] Only exits when user selects Exit

# Features

* MSSQL database connection
		
- The program uses a MSSQL db conneciton to store and read information.
- If no database exists, or the correct table does not exist, they will be created when the program starts.

* A console based UI using Spectre Console where users can navigate with selecting with their keyboard


# DB functions

## Main Menu

- Users can Manage Categories or Contacts, or quit the application.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookMainMenu.png)


### Category Menu

- Can Add, delete, update, and view a category, or view all categories.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookCategoryMenu.png)


### AddCategory

- Enter the category name.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookAddCategory.png)


### DeleteCategory

- Users select a category to delete from the available categories.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookDeleteCategory.png)


### UpdateCategory

- Select a category to update.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewCategorySelect.png)

- Users will be prompted to confirm if they would like to change the category name. If yes is chosen, they will be prompted to enter a new name.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookUpdateCategory.png)


### ViewCategory

- Users will be prompted to select a category from list of categories.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewCategorySelect.png)

- Users will be shown the category details and all the contacts in the category.
	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewCategory.png)


### ViewCategories

- Users will be shown all the categories.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewCategories.png)


### Contact Menu

- Can Add, delete, update, and view a category, or view all categories.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookContactMenu.png)


### AddContact

- Enter the contact name, phone number, email address, and category to put them in (Not required).
- Phone numbers are converted to the E164 format for storage in the database.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookAddContact.png)
 
- If no is selected for category, or if no categories are available. The user will be notified and the category will be set to Null for the user.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookAddContactNoCategory.png)


### DeleteContact

- Users select a contact to delete from the available contacts.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookDeleteContact.png)


### UpdateContact

- Select a contact to update.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookDeleteContact.png)

- Users will be prompted to confirm if they would like to change the contact's Name, phone number, email, or category.
- If yes is chosen for any field, they will be prompted to enter a new value.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookUpdateContact.png)


### ViewContact

- Users will be prompted to select a contact from list of contacts.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookDeleteContact.png)

- Users will be shown the contact details.
	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewContact.png)


### ViewCategories

- Users will be shown all the contacts.

	![image](https://github.com/Fennikko/Images/blob/main/Phonebook/PhonebookViewContacts.png)