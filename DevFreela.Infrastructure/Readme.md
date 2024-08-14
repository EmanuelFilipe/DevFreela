# This API project is designed to manage freelance projects, developed in C# 8, and incorporates the following concepts and patterns:

Clean Architecture
Repository Pattern
MediatR Pattern
CQRS
Entity Framework Core

# instalar na camada API o Microsoft.EntityFrameworkCore.Design

# caso necessário, instalar o dotnet ef
dotnet tool install --global dotnet-ef

# abrir console na pasta infrastructure

# add-migration
 Add-Migration InitialMigration -Context DevFreelaDbContext

# update
Update-Database -Context 