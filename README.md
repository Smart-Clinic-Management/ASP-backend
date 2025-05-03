# Smart Clinic Management System - Back-End

Smart Clinic is a comprehensive medical appointment and diagnosis system designed to enhance the experience of patients, doctors, and clinic administrators. The system provides integrated modules for full control and efficient management of clinic operations, including electronic payments and an intelligent preliminary diagnosis assistant.

## 🎯 Project Goals

- Streamline appointment booking and management.
- Improve communication between patients and doctors.
- Provide a complete admin dashboard for user and clinic management.
- Enable smart symptom analysis and electronic payment integration.

## 🧩 Functional Requirements

### 1. Admin Module
- Secure login with admin privileges.
- Full user management (Doctors & Patients): Add, Update, Delete.
- View all appointments and medical reports.

### 2. Patient Module
- Sign Up with email and password.
- Secure Sign In with JWT authentication.
- View and edit profile.
- Book appointments and view medical history.

### 3. Doctor Module
- Login with doctor privileges.
- Update personal information.
- Add diagnosis results and medical reports.

### 4. Appointment Management
- Patients can view available times and book appointments based on specialty and date.

## 🛠️ Tech Stack

- **ASP.NET Core 9**
- **Entity Framework Core**
- **SQL Server**
- **Clean Architecture**
- **Onion Architecture**
- **JWT Authentication**
- **Scalar (for API documentation)**
- **Visual Studio 2022**

## 🚀 Getting Started

### Requirements

- .NET 9 SDK (Preview)
- SQL Server
- Visual Studio 2022 or later

### Installation Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Smart-Clinic-Management/ASP-backend.git
   cd ASP-backend
   ### Project Structure (Clean Architecture)
   /SmartClinic
│
├── SmartClinic.API             → Web API (Controllers, Middleware, Authentication)
│
├── SmartClinic.Application     → Application logic (Services, DTOs, CQRS, Validators)
│
├── SmartClinic.Domain          → Core entities and interfaces
│
└── SmartClinic.Infrastructure  → Data access layer (EF Core, Repositories, Configs)
###Update the database connection
Open appsettings.json and update your SQL Server connection string.
dotnet ef database update
### Run the project
dotnet run
### Access API documentation
Open your browser and navigate to:
https://localhost:7047/scalar/v1



