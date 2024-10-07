# Run project
1. cd to service
2. copy appsettings.josn to appsettings.Development.json and change config
3. Run database update
```
dotnet ef database update
```
---
VerifyService : localhost:5110

AuthenticationService : localhost:5112

CourseService : localhost:5114
