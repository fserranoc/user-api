/* DB y esquema */
IF DB_ID('UsersDb') IS NULL
BEGIN
CREATE DATABASE UsersDb;
END
GO


USE UsersDb;
GO


IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL
DROP TABLE dbo.Usuario;
GO


CREATE TABLE dbo.Usuario
(
IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) NOT NULL,
Apellido VARCHAR(100) NOT NULL,
Correo VARCHAR(150) NOT NULL UNIQUE,
FechaCreacion DATETIME NOT NULL CONSTRAINT DF_Usuario_FechaCreacion DEFAULT (GETUTCDATE()),
Activo BIT NOT NULL CONSTRAINT DF_Usuario_Activo DEFAULT (1)
);
GO


-- sp crear 

IF OBJECT_ID('dbo.spUsuario_Crear', 'P') IS NOT NULL
DROP PROCEDURE dbo.spUsuario_Crear;
GO

CREATE PROCEDURE dbo.spUsuario_Crear
@Nombre VARCHAR(100),
@Apellido VARCHAR(100),
@Correo VARCHAR(150)
AS
BEGIN
SET NOCOUNT ON;


INSERT INTO dbo.Usuario (Nombre, Apellido, Correo)
VALUES (@Nombre, @Apellido, @Correo);


SELECT SCOPE_IDENTITY() AS IdUsuario;
END
GO

--sp listar

IF OBJECT_ID('dbo.spUsuario_Listar', 'P') IS NOT NULL
DROP PROCEDURE dbo.spUsuario_Listar;
GO

CREATE PROCEDURE dbo.spUsuario_Listar
@IncluirInactivos BIT = 0,
@BuscarTexto VARCHAR(150) = NULL
AS
BEGIN
SET NOCOUNT ON;


SELECT IdUsuario, Nombre, Apellido, Correo, FechaCreacion, Activo
FROM dbo.Usuario
WHERE (@IncluirInactivos = 1 OR Activo = 1)
AND (
@BuscarTexto IS NULL
OR Nombre LIKE '%' + @BuscarTexto + '%'
OR Apellido LIKE '%' + @BuscarTexto + '%'
OR Correo LIKE '%' + @BuscarTexto + '%'
)
ORDER BY IdUsuario DESC;
END


GO

--sp obtener

IF OBJECT_ID('dbo.spUsuario_Obtener', 'P') IS NOT NULL
DROP PROCEDURE dbo.spUsuario_Obtener;
GO

CREATE PROCEDURE dbo.spUsuario_Obtener
@IdUsuario INT
AS
BEGIN
SET NOCOUNT ON;


SELECT IdUsuario, Nombre, Apellido, Correo, FechaCreacion, Activo
FROM dbo.Usuario
WHERE IdUsuario = @IdUsuario;
END
GO


-- sp actualizar

IF OBJECT_ID('dbo.spUsuario_Actualizar', 'P') IS NOT NULL
DROP PROCEDURE dbo.spUsuario_Actualizar;
GO

CREATE PROCEDURE dbo.spUsuario_Actualizar
@IdUsuario INT,
@Nombre VARCHAR(100),
@Apellido VARCHAR(100),
@Correo VARCHAR(150),
@Activo BIT
AS
BEGIN
SET NOCOUNT ON;


UPDATE dbo.Usuario
SET Nombre = @Nombre,
Apellido = @Apellido,
Correo = @Correo,
Activo = @Activo
WHERE IdUsuario = @IdUsuario;


SELECT @@ROWCOUNT AS FilasAfectadas;
END

GO

-- sp eliminar

IF OBJECT_ID('dbo.spUsuario_Eliminar', 'P') IS NOT NULL
DROP PROCEDURE dbo.spUsuario_Eliminar;
GO

CREATE PROCEDURE dbo.spUsuario_Eliminar
@IdUsuario INT
AS
BEGIN
SET NOCOUNT ON;


UPDATE dbo.Usuario SET Activo = 0 WHERE IdUsuario = @IdUsuario;
SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO