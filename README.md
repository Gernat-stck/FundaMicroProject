## Características Principales

* **Autenticación Segura:** Control de acceso con hashing criptográfico SHA-256 en base de datos.
* **Mantenimiento de Clientes (CRUD):** Registro, edición, eliminación y listado en tiempo real con ventanas emergentes (modales).
* **Bitácora de Auditoría:** Registro transaccional automático (`SqlTransaction`) de cada acción realizada (`AGREGAR`, `EDITAR`, `ELIMINAR`) con detalle, usuario en sesión y marca de tiempo.
* **Gestión de Usuarios:** Administración de operadores del sistema (altas, bajas, modificación y restablecimiento de contraseña).
* **Prevención de Inyección SQL:** Todas las consultas a la base de datos están estrictamente parametrizadas mediante ADO.NET.
* **Arquitectura en Capas:** Desacoplamiento estructurado:
  * `Models/`: Entidades de dominio.
  * `DAL/`: Capa de Acceso a Datos pura (SQL y conexiones).
  * `Services/` o `BLL/`: Capa de Lógica de Negocio y orquestación transaccional.
  * `Security/`: Guardias de sesión (`PaginaBase`) y helpers de hashing.
  * `Controls/`: Componentes web reutilizables (Navbar).

---

## Requisitos Previos

* **IDE:** Visual Studio 2019 / 2022 con la carga de trabajo *Desarrollo web y ASP.NET*.
* **Base de Datos:** Microsoft SQL Server 2019 / 2022 o SQL Server Express.
* **Herramienta SQL:** SQL Server Management Studio (SSMS).
* **Framework:** .NET Framework 4.8 o compatible.

---

## Pasos de Instalación y Despliegue

### 1. Base de Datos
1. Abrir **SQL Server Management Studio (SSMS)** y conectarse al servidor local.
2. Abrir y ejecutar el script `Script_BaseDatos.sql` (incluido en la raíz de este repositorio).
3. Este script creará la base de datos `GestionClientesDB`, las tablas requeridas y el usuario inicial de prueba.

### 2. Cadena de Conexión
1. Abrir la solución `FundaMicroProject.slnx` en Visual Studio.
2. Abrir el archivo `Web.config` dentro del proyecto web.
3. Verificar la clave `ConexionDB`:
   ```xml
   <connectionStrings>
     <add name="ConexionDB" 
          connectionString="Server=localhost;Database=GestionClientesDB;Integrated Security=True;TrustServerCertificate=True;" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>

*(Ajustar el valor `Server` a `.\SQLEXPRESS` si se utiliza una instancia nombrada de SQL Express).*

### 3. Compilación y Ejecución

1. Compilar la solución en Visual Studio (`Ctrl + Shift + B`).
2. Establecer `Login.aspx` como página de inicio (o ejecutar directamente con `F5` / IIS Express).

---

## 🔑 Credenciales de Acceso Inicial

* **Usuario:** `admin`
* **Contraseña:** `Admin123*`
