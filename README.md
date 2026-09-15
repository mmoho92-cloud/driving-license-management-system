# DVLD - Driving & Vehicle License Department

[![C#](https://img.shields.io/badge/C%23-.NET-blue?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-purple?logo=.net&logoColor=white)](https://dotnet.microsoft.com/)

[![Windows Forms](https://img.shields.io/badge/Windows%20Forms-Desktop%20Application-0078D4?logo=windows&logoColor=white)](https://learn.microsoft.com/dotnet/desktop/winforms/)

[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)

[![ADO.NET](https://img.shields.io/badge/ADO.NET-Data%20Access-512BD4?logo=.net&logoColor=white)](https://learn.microsoft.com/dotnet/framework/data/adonet/)

[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-IDE-5C2D91?logo=visualstudio&logoColor=white)](https://visualstudio.microsoft.com/)

[![Git](https://img.shields.io/badge/Git-Version%20Control-F05032?logo=git&logoColor=white)](https://git-scm.com/)

[![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?logo=git&logoColor=white)](https://github.com/)

A desktop management application for the **Driving & Vehicle License Department (DVLD)**, developed using C# and Windows Forms.

The application provides a centralized system for managing people, users, drivers, driving license applications, tests, licenses, and related operations.

![DVLD Main Screen](Screenshots/MainScreen.png)

---

## Overview

DVLD is an educational desktop application that simulates the management of a Driving & Vehicle License Department.

The project was developed as a practical implementation of **Object-Oriented Programming**, **Three-Tier Architecture**, **ADO.NET**, and **SQL Server**.

The application separates the user interface, business logic, and database access into independent layers, keeping the responsibilities of each part of the system organized.

---

## Features

- People management

- User management and authentication

- Driver management

- Driving license applications

- Application type management

- Test management and test appointments

- Local and international license applications

- License management

- Detained license management

- Application and license tracking

- Remember Me functionality

- Password management

- Database-driven business operations

---

## Technologies

- C#

- .NET Framework

- Windows Forms

- ADO.NET

- Microsoft SQL Server

- T-SQL

- Visual Studio

- Git / GitHub

---

## Architecture

The application follows a **Three-Tier Architecture**:

```text
Presentation Layer
        ↓
Business Layer
        ↓
Data Access Layer
        ↓
SQL Server Database
```

### Presentation Layer

Contains the Windows Forms interface and handles user interaction, navigation, and UI-related validation.

### Business Layer

Contains the application's business objects and business logic.

### Data Access Layer

Handles communication with SQL Server using ADO.NET and performs database operations.

The Presentation Layer does not directly access the database. Database operations are handled through the Data Access Layer, while business rules are implemented in the Business Layer.

---

## Database

The application uses **Microsoft SQL Server** as its relational database.

The database contains the main entities required by the system, including:

- People

- Users

- Drivers

- Licenses

- License Classes

- Applications

- Application Types

- Tests

- Test Types

- Test Appointments

- Detained Licenses

- Countries

The `People` entity acts as the central entity for personal information, allowing the same person to be associated with different parts of the system without duplicating their personal data.

### Database Diagram

![DVLD Database Diagram](Database/DVLD-Database-Diagram.png)

---

## Project Structure

```text
DVLD+Project+Final
│
├── DVLD
│   └── Presentation Layer
│
├── DVLD_Buisness
│   └── Business Layer
│
├── DVLD_DataAccess
│   └── Data Access Layer
│
├── Database
│   ├── DrivingLicenseDepartment_Database.sql
│   └── DVLD-Database-Diagram.png
│
├── ScreenShots
│   ├── ApplicationsMenue.png
│   ├── DeteinLicenseScreen.png
│   ├── Login.png
│   ├── MainScreen.png
│   ├── MangeApplicationsScreen.png
│   ├── MangeApplicationTypesSeccren.png
│   ├── MangeDriversScreen.png
│   ├── MangeInternalionalLicenseApplicationScreen.png
│   ├── MangePepoleScreen.png
│   └── MangeUserScreen.png
│
├── Assets
│
├── .gitignore
│
└── DVLD+Project+Final.slnx
```

---

## Getting Started

### Requirements

Before running the application, make sure the following are installed:

- Windows

- Visual Studio

- The .NET Framework version required by the project

- Microsoft SQL Server

- SQL Server Management Studio (SSMS)

---

## Database Setup

The repository contains a SQL Server script that creates the complete database structure and inserts the required initial/reference data.

The script creates the database with the name:

```text
DrivingLicenseDepartment
```

### Create the Database

1. Open **SQL Server Management Studio (SSMS)**.

2. Connect to your SQL Server instance.

3. Open the following file from the repository:

```text
Database/DrivingLicenseDepartment_Database_Final.sql
```

4. Open the script in a new query window.

5. Execute the entire script.

6. After successful execution, refresh **Databases** in SSMS. You should see:

```text
DrivingLicenseDepartment
```

The script creates the database tables, relationships, views, and required reference data. It does not contain the old operational/development data from the original database.

The script also creates an initial application user for first login:

```text
Username: admin
Password: 1234
```

After logging in, the user can use the application's user-management functionality to create or manage users.

> **Note:** The database script is intended to be executed on a fresh installation where a database named `DrivingLicenseDepartment` does not already exist.

---

## Connection String Configuration

The application can connect to SQL Server using different authentication methods.

### 1. Windows Authentication

The current version of the project uses Windows Authentication:

```text
Server=.;Database=DrivingLicenseDepartment;Integrated Security=True;TrustServerCertificate=True;
```

With Windows Authentication, SQL Server uses the Windows account running the application to authenticate the connection. No SQL Server username or password is stored in the connection string.

If SQL Server is installed as a named instance, the server name can be changed accordingly. For example:

```text
Server=.\SQLEXPRESS;Database=DrivingLicenseDepartment;Integrated Security=True;TrustServerCertificate=True;
```

The exact server or instance name depends on the local SQL Server installation.

### 2. SQL Server Authentication

An alternative is SQL Server Authentication, where a SQL Server username and password are provided in the connection string.

For example:

```text
Server=.;Database=DrivingLicenseDepartment;User Id=YourUsername;Password=YourPassword;TrustServerCertificate=True;
```

In this configuration:

- `Server` specifies the SQL Server instance.

- `Database` specifies the database to connect to.

- `User Id` specifies the SQL Server login.

- `Password` specifies the password for that login.

The project previously used this type of connection string during development.

The current configuration uses **Windows Authentication**, so SQL Server credentials do not need to be stored directly in the application's connection string.

> When using SQL Server Authentication, replace the example credentials with the credentials configured on your own SQL Server instance.

> **Security:** Never commit real SQL Server usernames or passwords to the repository.

---

## Running the Application

After creating the database and configuring the connection string:

1. Open the solution in Visual Studio:

```text
DVLD+Project+Final.slnx
```

2. Build the solution.

3. Set the Presentation project as the startup project if necessary.

4. Run the application.

The application will connect to the `DrivingLicenseDepartment` database using the configured connection string.

---

## Authentication

The application includes an application-level login system based on users stored in the database.

The login system supports:

- Username and password

- Remember Me

- Logout

- Password change

- Account settings

### Password Storage

For this educational project, user passwords are currently stored in the database as plain text without encryption or hashing.

This was a deliberate limitation of the project because password hashing and secure password storage had not yet been covered in my learning at the time of development. The primary focus of this project was learning and implementing the application code, database design, and three-tier architecture.

**This approach is not recommended for a production application.** In a real-world system, passwords should be securely hashed using an appropriate password-hashing algorithm and should never be stored as plain text.

Detailed role-based authorization and user permissions are outside the current scope of the project.

---

## Screenshots

### Login

![DVLD Login](Screenshots/Login.png)

### Main Screen

![DVLD Main Screen](Screenshots/MainScreen.png)

### Applications Menu

![Applications Menu](Screenshots/ApplicationsMenue.png)

### Manage People

![Manage People](Screenshots/MangePepoleScreen.png)

### Manage Users

![Manage Users](Screenshots/MangeUserScreen.png)

### Manage Drivers

![Manage Drivers](Screenshots/MangeDriversScreen.png)

### Manage Applications

![Manage Applications](Screenshots/MangeApplicationsScreen.png)

### Manage Application Types

![Manage Application Types](Screenshots/MangeApplicationTypesSeccren.png)

### International License Applications

![International License Applications](Screenshots/MangeInterNationalLicenseApplicatiocScreen.png)

### Detained Licenses

![Detained Licenses](Screenshots/DeteinLicenseScreen.png)

---

## Scope

This project is an educational implementation focused on applying software development concepts to a relatively complex desktop application.

The main focus is the implementation of:

- Object-Oriented Programming

- Three-Tier Architecture

- Relational Database Design

- ADO.NET database access

- Business logic

- Windows Forms development

- SQL Server database management

Advanced production-level concerns such as role-based authorization, automated testing, centralized configuration, and deployment infrastructure are outside the current scope of the project.

---

## Author

**Mohamad AlMaho**

GitHub:

[https://github.com/mmoho92-cloud](https://github.com/mmoho92-cloud)
