# 🎾 Tennis Club App

A full-featured tennis club management application built with Blazor Server (.NET 8), MudBlazor, Entity Framework Core, and Supabase (PostgreSQL).

## Features

- **Player Management**: Register and manage tennis club members
- **Match Recording**: Record match results with automatic point updates
- **Standings**: View live rankings and statistics
- **Authentication**: Secure login/register with ASP.NET Core Identity
- **Responsive UI**: Mobile-friendly design with MudBlazor components

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend + Backend | Blazor Server (.NET 8) |
| UI Components | MudBlazor |
| ORM | Entity Framework Core |
| Database Provider | Npgsql (PostgreSQL) |
| Database | Supabase (PostgreSQL) |
| Authentication | ASP.NET Core Identity |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A [Supabase](https://supabase.com) account (free tier works)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <your-repo-url>
cd tennis-club-app
```

### 2. Configure the Database Connection

Edit `TennisClub.App/appsettings.json` and replace the placeholder values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOUR_SUPABASE_HOST;Port=5432;Database=postgres;Username=postgres;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```

### 3. Supabase Setup

1. Go to [supabase.com](https://supabase.com) and create a new project
2. Go to **Settings → Database**
3. Find your connection string (Host, Password)
4. Replace `YOUR_SUPABASE_HOST` and `YOUR_PASSWORD` in `appsettings.json`

### 4. Run EF Core Migrations

```bash
cd TennisClub.App
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run --project TennisClub.App/TennisClub.App.csproj
```

Navigate to `https://localhost:5001` (or the URL shown in the terminal).

## Project Structure

```
TennisClub.App/
├── Components/
│   ├── Layout/          # MainLayout, NavMenu
│   └── RedirectToLogin.razor
├── Data/
│   └── TennisDbContext.cs
├── Models/
│   ├── Player.cs
│   ├── Match.cs
│   └── Tournament.cs
├── Pages/               # Blazor route pages
│   ├── Home.razor
│   ├── Players.razor
│   ├── Matches.razor
│   ├── MatchNew.razor
│   ├── Standings.razor
│   ├── Login.razor
│   └── Register.razor
├── Services/
│   ├── PlayerService.cs
│   └── MatchService.cs
└── Program.cs
```

## Point System

- **Win**: +10 ranking points
- **Loss**: +3 ranking points

## License

MIT

