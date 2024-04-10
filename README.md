.NET Core 8 Web APi
- Basic Controller Model 
- Entity Framework
- Identity Framework 
- Swagger XML Comments
- Seed Initial User/Court Data (DataSeederStandAlone.cs)
- Endpoints using EF as well as Stored Procedures (FromSqlInterpolated, FromSqlRaw)

- Set your Connections string in Appsettings.json
- PM> update-database
- PM> dotnet run seeddata (seeds initial users and court data)
- Login 

- {
"email": "admin@example.com",
  "password": "Admin@123!*!",
  "twoFactorCode": "string",
  "twoFactorRecoveryCode": "string"
}

- Get Bearer Token