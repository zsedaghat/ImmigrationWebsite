# Global Bridge Support Services

A production-oriented ASP.NET Core MVC web application developed for **Global Bridge Support Services**, a Canadian support-services business.

The platform provides a professional public-facing website together with an administrative dashboard for managing services, countries, blog content, consultation requests, and customer reviews.

## 🌐 Live Website

https://globalbridgesupport.com/

---

## 📋 Project Overview

Global Bridge Support Services provides practical administrative and support services for individuals, newcomers, students, families, and businesses in Canada and internationally.

This project was developed as a full-stack web application with a responsive public website and a secure administrative area for managing website content and customer requests.

The application was designed with maintainability, usability, security, and responsive design in mind.

---

## ✨ Key Features

### Public Website

* Responsive and mobile-friendly design
* Home page with featured services and countries
* Services listing and service details
* Countries and destination information
* Blog / resources section
* Customer reviews
* Consultation request form
* Contact functionality
* About and support information
* Privacy Policy and Terms & Disclaimer pages
* SEO-friendly page metadata
* Social media integration

### Administration

* Secure administrator authentication
* Role-based authorization
* Admin dashboard
* Services management
* Countries management
* Blog post management
* Consultation request management
* Customer review management and moderation
* Image upload and management
* Pagination for administrative lists
* Active/inactive content management
* Display ordering for website content

---

## 🛠️ Technology Stack

### Backend

* C#
* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Core Identity
* Dependency Injection
* LINQ
* RESTful application endpoints

### Database

* SQL Server
* Entity Framework Core Migrations

### Frontend

* HTML5
* CSS3
* JavaScript
* Bootstrap
* Font Awesome

### Development & Deployment

* Visual Studio
* Git
* GitHub
* ASP.NET Core Hosting
* Production deployment

---

## 🏗️ Application Architecture

The application follows the ASP.NET Core MVC architecture with separation between presentation, business logic, and data access responsibilities.


┌──────────────────────────────┐
│          Web Layer           │
│     ASP.NET Core MVC         │
│ Controllers / Views / Models │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│       Service Layer          │
│   Business Logic / Services  │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│      Data Access Layer       │
│ Entity Framework Core / DB   │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│         SQL Server           │
└──────────────────────────────┘


The project also uses dependency injection to keep application components loosely coupled and easier to maintain and test.

---

## 🔐 Authentication & Authorization

The administrative area is protected using ASP.NET Core Identity and role-based authorization.

Administrative functionality is restricted to authorized users with the appropriate role.

Examples include:

* Admin authentication
* Role-based access control
* Protected administration routes
* Secure content management

---

## 🗂️ Main Data Entities

The application includes data management for:

* Countries
* Services
* Blog Posts
* Consultation Requests
* Reviews
* Users / Roles

---

## 🖼️ Content & Image Management

The administration panel allows authorized users to manage website images and content.

Uploaded images are organized under dedicated upload directories and referenced by the application when rendering public content.

---

## 📄 Pagination

Administrative listing pages use reusable pagination functionality to efficiently display larger datasets.

The pagination implementation provides:

* Current page
* Page size
* Total items
* Total pages
* Current page items

---

## 🔎 SEO

The public website includes SEO-related metadata such as:

* Page titles
* Meta descriptions
* Open Graph metadata
* Site-wide branding metadata
* Search-engine-friendly page structure

---

## 📱 Responsive Design

The website is designed to work across:

* Desktop
* Laptop
* Tablet
* Mobile devices

The frontend uses Bootstrap together with custom CSS to maintain a consistent responsive layout.

---

## 🚀 Deployment

The application has been deployed to a production hosting environment and is publicly accessible through:

**https://globalbridgesupport.com/**

---



## 📁 Project Structure

```text
ImmigrationWebsite
│
├── ImmigrationWebsite.Web
│   │
│   ├── Areas
│   │   └── Admin
│   │       ├── Controllers
│   │       ├── Views
│   │       └── ...
│   │
│   ├── Controllers
│   ├── Models
│   ├── Services
│   ├── Data
│   ├── Views
│   ├── wwwroot
│   │   ├── css
│   │   ├── js
│   │   ├── img
│   │   └── uploads
│   │
│   └── Program.cs
│
├── .gitignore
└── README.md
```

---

## 💻 Running the Project Locally

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio or another compatible IDE

### Setup

Clone the repository:

```bash
git clone https://github.com/zsedaghat/ImmigrationWebsite.git
```

Navigate to the web project:

```bash
cd ImmigrationWebsite/ImmigrationWebsite.Web
```

Configure the database connection in the application's configuration.

Apply Entity Framework Core migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

The application can then be accessed through the local URL provided by ASP.NET Core.

---

## 🔒 Security

Sensitive information should never be committed to the repository.

This includes:

* Database passwords
* API keys
* Authentication secrets
* Production credentials
* Private configuration values

Production configuration should be supplied through appropriate environment-specific configuration or hosting settings.

---

## 👩‍💻 Developer

**Zeinab Sedaghat**

.NET Developer | C# | ASP.NET Core | Backend Development

GitHub:
https://github.com/zsedaghat

---

## 📌 Project Note

This project was developed for a real-world client requirement and demonstrates practical experience with ASP.NET Core MVC, Entity Framework Core, authentication and authorization, content management, responsive web development, and production deployment.
