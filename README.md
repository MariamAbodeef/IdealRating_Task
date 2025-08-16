# Person Management System

A comprehensive person management application built with .NET 8 using Clean Architecture principles and modern design patterns. The system aggregates person data from multiple sources (SQL Server database and CSV files) and provides RESTful API for filtering person information with a HTML frontend.

## 🏗️ Architecture Overview

This project follows **Clean Architecture** principles with clear separation of concerns across multiple layers. The system is designed to aggregate person data from multiple sources (Database and CSV files):

```
┌─────────────────────┐
│   Presentation      │  ← API Controllers
├─────────────────────┤
│   Application       │  ← Services, DTOs, Mapping
├─────────────────────┤
│   Infrastructure    │  ← Data Access, CSV Reader, External Services
├─────────────────────┤
│   Domain            │  ← Entities, Abstractions, Models
└─────────────────────┘

Data Sources:
┌─────────────┐    ┌─────────────────┐
│  SQL Server │    │   CSV Files     │
│  Database   │    │  (Excel Reader) │
└─────────────┘    └─────────────────┘
       ↓                    ↓
┌─────────────────────────────────────┐
│    CompositePersonService          │ ← Combines both sources
└─────────────────────────────────────┘
```

## 🎯 Design Patterns Used

### 1. **Clean Architecture**
- Clear separation between Domain, Application, Infrastructure, and Presentation layers
- Dependency inversion principle ensuring the domain layer has no external dependencies

### 2. **Repository Pattern**
- `IPersonRepo.cs` - Interface defining data access contracts
- `PersonRepo.cs` - Concrete implementation handling data persistence
- Abstracts data access logic from business logic

### 3. **Service Layer Pattern**
- `IPersonService.cs` - Service interface defining business operations
- `PersonService.cs` - Business logic implementation
- `ICompositePersonService.cs` - Interface for multi-source data aggregation
- `CompositePersonService.cs` - Orchestrates data retrieval from database and CSV files

### 4. **Composite Pattern**
- `CompositePersonService.cs` - Aggregates person data from multiple sources (Database + CSV files)
- Combines data from repository (SQL Server) and Excel reader (CSV files) into unified response
- `CompositePersonFilter.cs` - Handles filtering across multiple data sources

### 5. **Data Transfer Object (DTO) Pattern**
- `PersonFilterDTO.cs` - Input data transfer for filtering
- `PersonListResponseDTO.cs` - Output data transfer for API responses
- Separates internal models from external contracts

### 6. **Mapping Pattern**
- `PersonMapping.cs` - Handles object-to-object mapping
- `PersonFilterMapping.cs` - Maps filter DTOs to domain models
- Ensures clean data transformation

### 7. **Data Source Abstraction Pattern**
- `IPersonExcelReader.cs` - Interface for reading person data from CSV/Excel files
- `PersonExcelReader.cs` - Concrete implementation for CSV file processing
- Enables reading from multiple file formats

## 🛠️ Technology Stack

- **.NET 8** - Framework
- **ASP.NET Core Web API** - REST API
- **Entity Framework Core** - ORM
- **SQL Server** - Database
- **HTML5/CSS3/JavaScript** - Frontend
- **Clean Architecture** - Project structure
- **Claude.ai** - AI Assistant for support and documentation 


## 📋 Prerequisites

Before running this application, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## 🚀 Setup Instructions

### 1. Clone the Repository
```bash
git clone "https://github.com/MariamAbodeef/IdealRating_Task.git"
cd IdealRating_Task/src/IdealRatingTechnicalTask.API
```

### 2. Database Configuration
1. Open `appsettings.json` in the API project
2. Update the connection string to match your SQL Server setup:
```json
{
    "ConnectionStrings": { 
        "DBConnectionString": "Server=.;Initial Catalog=IdealRating;Integrated Security=True;TrustServerCertificate=True;" 
    },

}
```

### 3. Database Migration
```bash
# Install dotnet ef (if not exists)
dotnet tool install --global dotnet-ef

# Add migration (if not already exists)
dotnet ef migrations add InitialCreate --project ..\IdealRatingTechnicalTask.Infrastructure

# Update database
dotnet ef database update
```

### 4. Restore Dependencies
```bash
dotnet restore
```

### 5. Build the Solution
```bash
dotnet build
```

## 🎬 How to Run

### Running the API

1. **Command Line:**
```bash
dotnet run
```

2. **Visual Studio:**
   - Set `IdealRatingTechnicalTask.API` as startup project
   - Press `F5` or click "Start Debugging"

3. **Specific URL (recommended):**
```bash
dotnet run 
```

The API will be available at:
- HTTP: `http://localhost:5001`
- Swagger UI: `http://localhost:5001/swagger`

### Running the Frontend

1. Open the `IdealRating_Task/person_details_list.html` file in your web browser
2. The HTML page is configured to connect to: `http://localhost:5001/person`
3. Use the interface to interact with the API

## 📡 API Documentation

### Base URL


### Endpoints

#### 1. Get Persons with Filtering
**POST** `/Person/GetAllPersons`

Retrieves a list of persons with optional filtering by name.

**Request Body:**
```json
{
  "name": "Ahmed"  // Optional: Filter by name (searches firstName, lastName, or full name)
}
```

**Response:**
```json
{
  "isSuccess": true,
  "error": null,
  "result": [
    {
      "firstName": "Ahmed",
      "lastName": "Amr",
      "telephoneCode": "20",
      "telephoneNumber": "122002020",
      "address": "10 Road Street",
      "country": "Egypt"
    }
  ]
}
```


## 🖥️ Frontend Usage

### Features
- **Load All Data**: Fetches all persons from the API
- **Name Filtering**: Filter persons by name (searches first name, last name, or full name)
- **Real-time Search**: Enter key support for quick searching

### How to Use
1. Open `IdealRating_Task/person_details_list.html` in your web browser
2. Click "Load Data" to fetch all records
3. Enter a name in the search field

## 🔧 Configuration

### API Configuration (`appsettings.json`)
```json
{
  "ConnectionStrings": {
    "DBConnectionString": "Your SQL Server Connection String"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Urls": "http://localhost:5001"
}
```

### CORS Configuration
The API is configured to allow all origins for development. For production, update the CORS policy in `Program.cs`:


## 🧪 Testing

### Manual Testing
1. Use the included `IdealRatingTechnicalTask.API.http` file for API testing
2. Test with the HTML frontend interface
3. Use Swagger UI at `https://localhost:7001/swagger`



## 🔄 Development Workflow

1. **Database Changes**: Add migrations using Entity Framework
2. **API Changes**: Update controllers and services
3. **Frontend Changes**: Modify the HTML/CSS/JavaScript
4. **Testing**: Use Swagger UI or the frontend interface
