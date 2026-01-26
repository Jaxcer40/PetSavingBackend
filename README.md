# Pet Saving Backend

Proyecto de .net webapi para Pet Saving.

## Inicializar base de datos

1. AgregaR migraciones

   ```bash
   dotnet ef migrations add Init
   ```

2. Crear database.db

   ```bash
   dotnet ef database update
   ```

## Ejecutar

   ```bash
   dotnet run
   ```