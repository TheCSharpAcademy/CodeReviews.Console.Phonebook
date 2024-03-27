# PhoneBook

## EF
```bash
dotnet ef migrations add <Name>
dotnet ef database update
```

## SQL Server Setup on Mac
First download Docker and Azure Data Studio.

Pull docker image:
```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Run SQL Server
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=123456aA" \
   -p 1433:1433 --name sql1 --hostname sql1 -d \
   mcr.microsoft.com/mssql/server:2022-latest
```
