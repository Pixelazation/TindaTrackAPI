# TindaTrackAPI

TindaTrackAPI is the backend for **TindaTrack**, a sales tracking system built with ASP.NET Core and Entity Framework Core using MySQL.

## Technologies

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [MySQL](https://www.mysql.com/)
- [Swagger](https://swagger.io/)
- [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/TindaTrackAPI.git
cd TindaTrackAPI
```

### 2. Setup Secrets (Connection String)

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=3306;database=tinda_track;user=root;password=yourpassword;"
```

3. Apply Migrations & Seed Database

```bash
dotnet ef database update
```

4. Run the API
```bash
dotnet run
```