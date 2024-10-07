
# Phone_Book.Lawang
This phone book application saves the Email and Phone number in the SqlServer database which is connected via Entity FrameworkCore ORM. User of this app can send Email and Message to the valid contact saved in the database.


## Features
- This is an application where you can record contacts with their phone numbers and Email.
-  Users are able to Add, Delete, Update and Read from a database, using the console.
- Email and Phone numbers are validated before storing in the database.
- Code first approach is obtained using Entity FrameworkCore.
- User can group the Contacts into Category.
- User can send Email to Contact stored in database and Also SMS using the Phone number stored.





## Deployment

To deploy this project run

- This app uses users-secrets and to initialize it run the following command.
- ```powershell 
    dotnet user-secrets init 
    ```
- This will generate UserSecretId in .csproj file like given below. (UserSecretId may vary from given below)
`<UserSecretsId>79a3edd0-2092-40a2-a04d-dcb46d5ca9ed</UserSecretsId>`

- This app stores connection string in the User secrets, so to enable the connection string to database copy the code below in .NET CLI and change the 'YOUR_PASSWORD' to your database password.
```powershell
dotnet user-secrets set "ConnectionString" "Server=localhost;Database=PhoneBook_db;User=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true"
```
- To send the Email user need to add the sender Email, to add Sender Email in the users-secrets replace 'YOUR EMAIL' with your desired sender Email.
```powershell
dotnet user-secrets set "Sender-Email" "YOUR EMAIL"
``` 
- Sender Name is also required to send mail, again to set the Sender name replace 'YOUR NAME' with your desired name:
```powershell
dotnet user-secrets set "Name" "YOUR NAME"
```
- This app utilises the google SMTP server and requires the app password for the Sender Email, again replace the "YOUR APP-PASSWORD" for your app password.
```powershell
dotnet user-secrets set "App-Password" "YOUR APP-PASSWORD"
```

- This app also utilises the Twilio for sending SMS and the configuration of Twilio is also in user-secrets, So it needs Twilio account sid, Twilio Auth token and Twilio generated Phone Number, this configuration is obtained when creating an account in twilio, so to cofigure it in this app Just replace, "YOUR SID" with your twilio account sid, "YOUR AUTH" with your twilio auth token and "YOUR PHONE" with your twilio generated phone number.

```powershell
dotnet user-secrets set "TWILIO_SID" "YOUR SID"
```
```powershell
dotnet user-secrets set "TWILIO_AUTH" "YOUR AUTH"
```
```powershell
dotnet user-secrets set "TWILIO_PHONE_NUMBER" "YOUR PHONE"
```
 ## Screen shots:



* ![App Screenshot]

- Data is presented to user in Table format, using the external library Spectre.Console.
- This app is beautified using Spectre.Console.



## Project Summary
#### What challenges did you face and how did you overcome them?

* I had no idea on how to send Email and SMS, so I browse the tutorial on youtube and got my answers there




## 🛠 Skills Learned
#### ENTITY-FRAMEWORK
* I had previous experiences with entity framework but doing this project was like a revision for me and helped me retain some crucial information

#### Spectre.Console
* I honed my Spectre.Console skill in this project which i previously learned.

#### TWILIO
* Working with Twilio to send SMS helped me learn how the sms alert and notification is sent via application.

#### SMTP
* Learned a lot about SMTP and how it the sends email, and how SMTP it is different from HTTP.
* SMTP is just one of the internet protocol that is used to send the email.


## FAQ

#### How to beautify the table in the project?

Answer I used the Microsoft.Spectre.Console package, which you can get for Nuget package manager. Install it and add Reference to your project. 

For more information u can visit the docs https://spectreconsole.net




## Feedback

If you have any feedback, please reach out to us at depeshgurung44@gmail.com

