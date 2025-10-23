# API Usuarios (.NET 8)

## 1) Arquitectura utilizada
**Clean Architecture**
- **Domain**: Entidades (`Usuario`) e interfaces (`IUserRepository`). *Sin dependencias externas*.
- **Application**: Casos de uso/servicios (`IUserService`, `UserService`) y DTOs/validaciones. Orquesta la lógica y mapea Entidad ⇄ DTO.
- **Infrastructure**: Acceso a datos con **Dapper** y **Stored Procedures** en SQL Server. Implementa `IUserRepository`, `SqlConnectionFactory`.
- **API (Presentation)**: ASP.NET Core **.NET 8** (Controllers). Endpoints **solo POST**: `create`, `list`, `get`, `update`, `delete`. **Swagger** habilitado.

**Beneficios**: separación de responsabilidades, alta testabilidad, posibilidad de reemplazar infraestructura sin tocar reglas de negocio, código mantenible y escalable.

---

## 2) Conexión a Base de Datos
- **Proveedor**: `Microsoft.Data.SqlClient`.
- **Patrón**: `ISqlConnectionFactory` (crea `IDbConnection` por request) + `Options` para enlazar `DbOptions:ConnectionString`.
- **Seguridad**: La cadena de conexión se obtiene desde appsettings.
- **Inicialización**: Script `db/init.sql` crea la tabla y los SP del CRUD. 

---

## 3) Librerías adicionales
- **Dapper**: micro-ORM para ejecutar SP y mapear a POCOs.
- **Swashbuckle.AspNetCore**: Swagger UI / OpenAPI para probar la API.
- **FluentValidation** *(opcional)*: Validaciones de DTOs en `create`/`update`.
- **Microsoft.Extensions.Options.ConfigurationExtensions** y **Microsoft.Extensions.Configuration.Binder**: binding/validación de `DbOptions`.

---

## 4) Decisiones técnicas relevantes
- **Dapper + SP obligatorios**: todas las operaciones (`create`/`list`/`get`/`update`/`delete`) invocan **Stored Procedures** (`CommandType.StoredProcedure`).
- **Borrado lógico** (`Activo = 0`) en `delete`; el `list` trae solo activos por defecto para auditoría y reversión.
- **Endpoints solo POST** para cumplir instructivo y facilitar pruebas con Swagger/Postman.
- **DTOs inmutables** (`record`) y mapeo explícito para desacoplar API/Domain.
- **Manejo global de errores** con `UseExceptionHandler` + logging por defecto de ASP.NET.
- **CORS básico** (ajustable) y **HTTPS** activo.
 **Escalabilidad/Mantenibilidad**: DI nativa, repositorio cohesivo, servicios delgados; fácil de extender con **caché**, **paginación**, **health checks** o **JWT** sin romper capas.
