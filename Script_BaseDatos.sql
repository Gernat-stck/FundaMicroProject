-- 1. Crear la base de datos
CREATE DATABASE GestionClientesDB;
GO

USE GestionClientesDB;
GO

-- 2. Tabla Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Usuario VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(64) NOT NULL,
    NombreCompleto VARCHAR(100) NOT NULL,
    Estado BIT DEFAULT 1 NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- 3. Tabla Clientes
CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    DocumentoIdentidad VARCHAR(20) NOT NULL UNIQUE,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Telefono VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    Direccion VARCHAR(255) NULL,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

-- 4. Tabla Bitácora (para auditar cambios en Clientes)
CREATE TABLE Bitacora (
    IdBitacora INT IDENTITY(1,1) PRIMARY KEY,
    Accion VARCHAR(20) NOT NULL, -- 'AGREGAR', 'EDITAR', 'ELIMINAR'
    IdClienteAfectado INT NOT NULL,
    Detalle VARCHAR(MAX) NULL,
    UsuarioResponsable VARCHAR(50) NOT NULL,
    FechaHora DATETIME DEFAULT GETDATE()
);
GO

-- 5. Usuario de prueba inicial
-- Credenciales:
-- Usuario: admin
-- Contraseña plana: Admin123*
INSERT INTO Usuarios (Usuario, PasswordHash, NombreCompleto, Estado)
VALUES ('admin', '0a5bc3e342432f1bad92ffd51b785343ec72906cdba6a26131060b008e786656', 'Administrador General', 1);
GO