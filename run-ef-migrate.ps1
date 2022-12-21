$migrationName = Read-Host -Prompt "Migration Name"
dotnet ef migrations add --startup-project .\src\DesktopSetupConfigurator\ --project .\src\DesktopSetupConfigurator.DataAccess\ $migrationName