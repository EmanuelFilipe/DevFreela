# instalar na camada API o Microsoft.EntityFrameworkCore.Design

# caso necessário, instalar o dotnet ef
dotnet tool install --global dotnet-ef

# abrir console na pasta infrastructure

# add-migration
 Add-Migration InitialMigration -Context DevFreelaDbContext

# update
Update-Database -Context DevFreelaDbContext