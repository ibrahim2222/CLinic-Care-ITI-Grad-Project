# Clinic-System-MVC-ITI-GRADUTION-PROJECT

Clinic System Website
 
## About Clinic System Website

The Clinic System Website is a comprehensive online platform designed to streamline and enhance the management of medical appointments and patient information in a healthcare clinic setting. This web application offers a range of features and functionalities to improve the efficiency of clinic operations and enhance patient experiences.

Admin User Name = admin@roshetaclinic.org
Admin Password = Admin$123

Doctor User Name = clinicdoctor@roshetaclinic.org
Doctor Password = Admin$123

Nurse User Name = clinicnurse@roshetaclinic.org
Nurse Password = Admin$123

Examination User Name = clinicexamination@roshetaclinic.org
Examination Password = Admin$123

receptionist User Name = clinicreceptionist@roshetaclinic.org
receptionist Password = Admin$123

Patient User Name = testpatient@roshetaclinic.com
Patient Password = Patient$123

## Working Tools
- [Trello](https://trello.com/b/OkW0qij7/graduation-project)

## How to use it :

The Clinic System Website is designed to be user-friendly and intuitive, making it easy for both healthcare providers and patients to navigate. Here's a step-by-step guide on how to use the website effectively:

- You will need the latest Visual Studio 2022 and the latest .NET Core 6.
- You will need  MS SQL Server
-  Make sure from the configuration in the **AppSettings.json** file that meets the application features :
   (**Google** for External Login,**Facebook** for External Login, **Stripe** for online payment , **Twilio** & **Email** Configuration Service)

### Email Configuration Section

```json
"EmailConfiguration": {
    "From": "",
    "SmtpServer": "",
    "Port": "",
    "Username": "",
    "Password": ""
  }
```

### Google and Facebook Configuration Section

```json
"Authentication": {
    "Google": {
      "ClientId": "",
      "ClientSecret": ""
    },
     "Facebook": {
      "AppId": "",
      "AppSecret": ""
    }
  }
   
```

### Stripe Configuration Section

```json
 "Stripe": {
    "PublicKey": "",
    "SecretKey": ""
  }
```
### Twilio Configuration Section
```json
 "Twilio": {
    "AccountSid": "",
    "AuthToken": ""
  }
  ```

- in **Package Manager Console** make Db migration using command

```cmd
add-migration init
```

- Write another command to update the database

```cmd
update-database
```

- Change the connection string (SQL Server, username & password )

```json
 "ConnectionStrings": {
    "DefaultConnection": "Data Source=[Server Name];Initial Catalog=Clinical_DB;User ID=[Sql server Username]];Password=[Sql server Password];Integrated Security=True"
  }
```
the application will run on route 'https://localhost:7150'

## Features :

- User Registration and Login:
  Patients and healthcare providers can register and log in securely.
  External-Logins supported

- Patient Management:
  Patients can create and manage their profiles, including contact information and medical history.

- Appointment Booking:
  Patients can schedule appointments with their preferred healthcare providers.
  Healthcare providers can manage appointments efficiently.

- Queue Management:
  The system organizes patients in a queue based on appointment times.
  Supports reordering of the queue after cancellations or changes.

- Notifications:
  Automated notifications are sent to patients for appointment reminders.

- User Roles:
  Different user roles, including patients, healthcare providers, and administrators.

- Administrator Controls Panal:
   User management for adding and managing healthcare professionals.
   Customization options to tailor the system to the clinic's specific needs.

- Payment system using "Stripe"

- Send massages with Twilio


#### Refrences

- [ASP .NET CORE MVC](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0).

