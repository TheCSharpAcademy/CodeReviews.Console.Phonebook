March 10 / 2024

Project Finish Target Date: March 24/2024

Requirements:

 This is an application where you should record contacts with their phone numbers.

 Users should be able to Add, Delete, Update and Read from a database, using the console.

 You need to use Entity Framework, raw SQL isn't allowed.

 Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}

 You should validate e-mails and phone numbers and let the user know what formats are expected
 You should use Code-First Approach, which means EF will create the database schema for you.

 You should use SQL Server, not SQLite

----TO DOs ---

Todos for Phonebook Project:

1. Define Data Model:
   - Define the Contact class with properties for Id, Name, Email, and PhoneNumber.
   - Implement validation for email and phone number formats.

2. Configure Entity Framework:
   - Configure Entity Framework Core to use SQL Server as the database provider.
   - Set up the DbContext class (PhoneBookContext) to interact with the database.
   - Define the database schema using the OnModelCreating method in PhoneBookContext.

3. CRUD Operations:
   - Implement methods for creating (AddContact), reading (GetContact), updating (UpdateContact), and deleting (DeleteContact) contacts in the PhoneBookContext.

4. Console User Interface:
   - Implement a console user interface to interact with the phone book.
   - Provide options for adding, deleting, updating, and viewing contacts.
   - Validate user input and provide feedback/error messages where necessary.

5. Testing:
   - Test each CRUD operation to ensure they work as expected.
   - Test input validation to ensure it rejects invalid email and phone number formats.

6. Error Handling:
   - Implement error handling to gracefully handle exceptions and unexpected scenarios.
   - Provide meaningful error messages to the user when errors occur.

7. Documentation:
   - Document the code with comments to explain its purpose and functionality.
   - Write a README.md file with instructions on how to run the application and any other relevant information.

8. Code Cleanup:
   - Refactor the code to improve readability, maintainability, and adherence to best practices.
   - Remove any unnecessary or redundant code.

9. Additional Features (Optional):
   - Implement search functionality to search for contacts by name or email.
   - Add pagination or sorting options for viewing contacts.
   - Implement import/export functionality to import contacts from or export contacts to a file.

10. Review and Feedback:
    - Have someone review your code for any potential issues or areas of improvement.
    - Gather feedback from users to identify any usability issues or desired features.


____ 
March 13 - 2024 Update:

Almost done - just have to add validation to the updateContact method and then clean code.
This is finished a lot sooner than I thought.
Progressing fast. Feels good.

____ 
March 18 - 2024 Update:

Project finished. Will submit 

