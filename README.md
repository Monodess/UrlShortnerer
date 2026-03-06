# UrlShortener ASP.NET Core backend 
Project for creating re-reference url and storing it in DB

## Stack: 
- C# ASP.NET Core (9.0.0)
- EF .NET (9.0.0)
- MySQL (8.0.0)

## Requirements
- .NET 9.0 SDK
- MySQL 8.0+
- Entity Framework Core Tools (for migrations)

## Quick Start
1. Clone the repository.
2. Update `ConnectionString` in `appsettings.json` with your MySQL path.
3. Run migrations: `dotnet ef database update`
4. Start the application: `dotnet run`

## Usage
1. Open the URL provided in the console (e.g., `http://localhost:5000`).
2. Navigate to `/swagger` to access the interactive API documentation.
3. Use the **POST** endpoint to submit a long URL.
4. Copy the shortened identifier and use the **GET** endpoint or browser address bar to redirect.
