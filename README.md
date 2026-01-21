# 🛡️ Sistema de Gestión de Seguros - Rimac

Sistema integral de gestión de seguros vehiculares desarrollado con **ASP.NET Core MVC** que permite administrar pólizas, siniestros, reclamaciones, talleres y presupuestos de manera eficiente y profesional.

![Página Principal](https://github.com/user-attachments/assets/191d0f29-648d-40ac-ba86-1b03efb043bc)

---

## 📋 Descripción del Proyecto

**RimacSegurosApp** es una aplicación web completa para la gestión de seguros vehiculares que implementa un flujo integral desde la contratación de pólizas hasta la gestión de siniestros y reclamaciones. El sistema facilita la interacción entre beneficiarios, administradores, talleres y proveedores de servicios.

### Características Principales

- ✅ **Gestión de Usuarios y Autenticación**: Sistema de login/registro con autenticación basada en cookies y roles diferenciados
- ✅ **Gestión de Pólizas**: Creación, consulta y administración de pólizas de seguro vehicular
- ✅ **Registro de Siniestros**: Captura detallada de siniestros con ubicación geográfica (departamento, provincia, distrito)
- ✅ **Sistema de Reclamaciones**: Proceso completo de reclamaciones con seguimiento de estado
- ✅ **Gestión de Talleres**: Asignación y administración de talleres para reparación de vehículos
- ✅ **Presupuestos y Validación**: Creación, validación y aprobación de presupuestos de reparación
- ✅ **Administración de Proveedores**: Gestión de proveedores de servicios asociados
- ✅ **Carga de Documentos**: Integración con **Cloudinary** para almacenamiento de documentos y evidencias
- ✅ **Generación de Reportes**: Reportes PDF con estadísticas y detalles de siniestros utilizando **iText7**
- ✅ **Gestión de Indemnizaciones**: Control de pagos y liquidaciones

---

## 🏗️ Arquitectura del Proyecto

El proyecto implementa una **arquitectura en capas de 3 niveles** organizadas mediante **Visual Studio Solution**:

```
ProyectoRimacSeguros.sln
│
├── 📁 Frontend
│   └── AplicaciónWeb (Capa de Presentación)
│       ├── Controllers/
│       ├── Views/
│       ├── Models/
│       ├── wwwroot/
│       └── Program.cs
│
├── 📁 Backend
│   ├── AccesoDatos (Capa de Acceso a Datos)
│   │   ├── *DA.cs (Data Access)
│   │   └── Consultas SQL
│   │
│   ├── LogicaNegocio (Capa de Lógica de Negocio)
│   │   └── *LN.cs (Business Logic)
│   │
│   └── DbContext (Contexto de Base de Datos)
│       ├── MyDbContext
│       └── Entity Framework Configuration
│
└── 📁 Transversal
    └── EntidadesProyecto (Entidades de Dominio)
        └── Modelos del Dominio
```

### Separación de Responsabilidades

| Capa | Responsabilidad |
|------|-----------------|
| **AplicaciónWeb** | Presentación, vistas Razor, controladores MVC, manejo de peticiones HTTP |
| **LogicaNegocio** | Reglas de negocio, validaciones, orquestación de operaciones |
| **AccesoDatos** | Interacción con base de datos, consultas SQL, operaciones CRUD |
| **DbContext** | Configuración de Entity Framework, DbContext, migraciones |
| **EntidadesProyecto** | Modelos de dominio, DTOs, ViewModels, entidades compartidas |

---

## 🛠️ Tecnologías Utilizadas

### **Backend & Framework**
- **ASP.NET Core 8.0** - Framework principal
- **Entity Framework Core 8.0** - ORM para acceso a datos
- **C# (.NET 8)** - Lenguaje de programación

### **Base de Datos**
- **Microsoft SQL Server** - Sistema de gestión de base de datos
- **SQL Server Management Studio (SSMS)** - Administración de BD

### **Frontend**
- **Razor Pages / Views** - Motor de vistas
- **Bootstrap 5.3** - Framework CSS para UI responsiva
- **JavaScript / jQuery** - Interactividad del cliente

### **Librerías y Paquetes NuGet**
- **CloudinaryDotNet (1.26.2)** - Almacenamiento de archivos en la nube
- **BCrypt.Net-Next (4.0.3)** - Hashing seguro de contraseñas
- **iText7 (9.0.0)** - Generación de documentos PDF
- **Rotativa.AspNetCore (1.4.0)** - Conversión de vistas a PDF
- **Microsoft.AspNetCore.Authentication.Cookies** - Autenticación con cookies

### **Herramientas de Desarrollo**
- **Visual Studio 2022** - IDE principal
- **Git / GitHub** - Control de versiones

---

## 📊 Modelo de Datos (Entidades Principales)

### Entidades del Dominio

| Entidad | Descripción |
|---------|-------------|
| `Usuario` | Información de usuarios del sistema con roles |
| `Administrador` | Datos específicos del rol administrador |
| `Beneficiario` | Clientes/asegurados del sistema |
| `Póliza` | Contratos de seguro vehicular |
| `Vehículo` | Información de vehículos asegurados |
| `Siniestro` | Registro de incidentes y accidentes |
| `Reclamación` | Solicitudes de indemnización |
| `DocumentosReclamacion` | Evidencias y documentación asociada |
| `Taller` | Talleres autorizados para reparaciones |
| `Proveedor` | Proveedores de servicios |
| `Presupuesto` | Cotizaciones de reparación |

### Relaciones Principales

```
Beneficiario (1) ──── (N) Póliza (1) ──── (N) Siniestro
                                              │
                                              ├──── (N) Reclamación
                                              │
                                              └──── (1) Presupuesto
                                                    │
                                                    └──── Taller
```

---

## 🚀 Instalación y Configuración

Hay dos formas de ejecutar la aplicación: usando Docker (recomendado y más rápido) o instalación manual con Visual Studio.

---

### 🐳 Opción 1: Ejecución con Docker (Recomendado)

La forma más rápida y sencilla de ejecutar la aplicación **sin necesidad de instalar SQL Server ni Visual Studio**.

#### Prerrequisitos Docker

1. **Docker Desktop** - [Descargar aquí](https://www.docker.com/products/docker-desktop)
2. **Git** para clonar el repositorio

#### Pasos Rápidos

**1. Clonar el repositorio**

```bash
git clone https://github.com/Johnkl725/RimacSegurosApp.git
cd RimacSegurosApp
```

**2. Configurar variables de entorno (opcional)**

```bash
# Windows
copy .env.example .env

# Linux/Mac
cp .env.example .env

# Editar .env con tus credenciales de Cloudinary (opcional)
# Si no usas Cloudinary, puedes omitir este paso
```

**3. Iniciar la aplicación**

```bash
docker-compose up -d
```

**4. Esperar a que los contenedores inicien**

```bash
# Ver logs en tiempo real
docker-compose logs -f webapp
```

Espera hasta ver el mensaje: **"Now listening on: http://+:8080"**

**5. Acceder a la aplicación**

Abrir el navegador en: **http://localhost:8080**

#### Comandos Útiles Docker

| Comando | Descripción |
|---------|-------------|
| `docker-compose up -d` | Iniciar aplicación en segundo plano |
| `docker-compose down` | Detener y eliminar contenedores |
| `docker-compose logs -f` | Ver logs en tiempo real |
| `docker-compose ps` | Ver estado de contenedores |
| `docker-compose restart webapp` | Reiniciar aplicación web |
| `docker-compose down -v` | Detener y eliminar datos (reset completo) |
| `docker-compose up --build` | Reconstruir y iniciar |

#### Acceder a SQL Server desde Docker

```bash
# Conectar directamente al contenedor
docker-compose exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd"
```

O conectar con herramientas externas (SSMS, Azure Data Studio, etc.):
- **Host**: `localhost:1433`
- **Usuario**: `sa`
- **Password**: El configurado en `.env` (por defecto: `YourStrong!Passw0rd`)
- **Base de datos**: `BDSeguros`

#### Troubleshooting Docker

**Puerto ya en uso**
```bash
# En docker-compose.yml, cambiar "8080:8080" por "8081:8080"
docker-compose down
docker-compose up -d
```

**Reconstruir desde cero**
```bash
docker-compose down -v
docker-compose build --no-cache
docker-compose up -d
```

---

### 💻 Opción 2: Instalación Manual (Visual Studio)

Para desarrollo con Visual Studio o sin Docker.

#### Prerrequisitos

1. **.NET 8 SDK** - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **SQL Server 2019+** o **SQL Server Express**
3. **Visual Studio 2022** (recomendado) o Visual Studio Code
4. **Git** para clonar el repositorio

#### Pasos de Instalación

**1. Clonar el Repositorio**

```bash
git clone https://github.com/Johnkl725/RimacSegurosApp.git
cd RimacSegurosApp
```

**2. Configurar la Base de Datos**

1. Abrir **SQL Server Management Studio (SSMS)**
2. Ejecutar el script de base de datos:
   - Ubicación: `database/init.sql`
   - Este script creará la base de datos `BDSeguros` con todas las tablas y datos iniciales

**3. Configurar Cadena de Conexión**

Editar el archivo `AplicaciónWeb/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=BDSeguros;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

> **Nota**: Reemplaza `TU_SERVIDOR` con tu instancia de SQL Server (ej: `localhost\\SQLEXPRESS` o `DESKTOP-XXX\\MSSQLSERVER01`)

**4. Configurar Cloudinary (Opcional)**

Para habilitar la carga de documentos:

1. Crear cuenta gratuita en [Cloudinary](https://cloudinary.com/)
2. Obtener credenciales del dashboard
3. Actualizar la sección `Cloudinary` en `appsettings.json` o `appsettings.Development.json`

**5. Restaurar Dependencias**

```bash
dotnet restore
```

**6. Compilar la Solución**

```bash
dotnet build
```

**7. Ejecutar la Aplicación**

**Opción A**: Desde línea de comandos
```bash
cd AplicaciónWeb
dotnet run
```

**Opción B**: Desde Visual Studio
- Abrir `ProyectoRimacSeguros.sln`
- Establecer `AplicaciónWeb` como proyecto de inicio
- Presionar `F5` o seleccionar "Iniciar depuración"

**8. Acceder a la Aplicación**

Abrir el navegador en: `https://localhost:5001` o `http://localhost:5000`

---

---

## 👥 Roles de Usuario

El sistema maneja diferentes roles con permisos específicos:

### 🔵 Administrador
- Gestión completa de pólizas y siniestros
- Validación y aprobación de presupuestos
- Generación de reportes estadísticos
- Asignación de talleres
- Control de pagos e indemnizaciones

### 🟢 Beneficiario (Asegurado)
- Registro de siniestros
- Creación de reclamaciones
- Carga de documentación
- Seguimiento de estado de reclamaciones
- Consulta de pólizas

### 🟡 Taller
- Recepción de asignaciones de siniestros
- Creación de presupuestos de reparación
- Actualización de estado de reparaciones

### 🟠 Proveedor
- Gestión de servicios asociados
- Consultas de presupuestos

---

## 📁 Estructura de Archivos SQL

El proyecto incluye múltiples scripts SQL para diferentes versiones de la base de datos:

- `7diciembre.sql` - Script actualizado de base de datos
- `Database16noviembre.sql` - Versión anterior
- `ultimo.sql`, `ultimolunes.sql` - Versiones de respaldo
- `WISNERIASALVADOELDIA.sql` - Base de datos completa con datos de prueba

> **Recomendación**: Usar el script `7diciembre.sql` para la configuración inicial.

---

## 🌟 Características Técnicas Destacadas

### Seguridad
- ✅ Autenticación con **Cookie Authentication**
- ✅ Hashing de contraseñas con **BCrypt**
- ✅ Validación del lado del servidor y cliente
- ✅ Protección CSRF integrada en formularios
- ✅ Autorización basada en roles

### Funcionalidades Avanzadas
- 📄 Generación dinámica de PDFs con **iText7**
- ☁️ Almacenamiento cloud con **Cloudinary**
- 🗺️ Registro de ubicación geográfica jerárquica (Departamento → Provincia → Distrito)
- 📊 Sistema de reportes con métricas y estadísticas
- 🔄 Flujo completo de estados (siniestro → reclamación → presupuesto → aprobación → pago)

### Patrón de Diseño Repository
- Separación clara entre lógica de negocio y acceso a datos
- Inyección de dependencias con `Scoped` lifetime
- Facilita testing y mantenibilidad

---

## 🔄 Flujo de Trabajo Principal

1. **Registro de Usuario** → El beneficiario se registra en el sistema
2. **Contratación de Póliza** → Administrador asigna una póliza al beneficiario
3. **Registro de Siniestro** → Beneficiario reporta un incidente con documentación
4. **Creación de Reclamación** → Se genera una reclamación asociada al siniestro
5. **Asignación de Taller** → Administrador asigna un taller autorizado
6. **Generación de Presupuesto** → Taller elabora cotización de reparación
7. **Validación** → Administrador revisa y aprueba el presupuesto
8. **Aprobación Final** → Se autoriza el trabajo de reparación
9. **Pago de Indemnización** → Se procesa el pago al beneficiario o taller
10. **Cierre** → Finalización del proceso de reclamación

---

## 📝 Comandos Útiles

### Restaurar paquetes NuGet
```bash
dotnet restore
```

### Compilar proyecto
```bash
dotnet build
```

### Ejecutar aplicación
```bash
dotnet run --project AplicaciónWeb
```

### Ejecutar migraciones (si se usa Code First)
```bash
dotnet ef migrations add NombreMigracion
dotnet ef database update
```

### Limpiar solución
```bash
dotnet clean
```

---

## 🐛 Solución de Problemas Comunes

### Error de conexión a la base de datos
- Verificar que SQL Server esté corriendo
- Confirmar la cadena de conexión en `appsettings.json`
- Asegurar que la base de datos `BDSeguros` existe

### Error al cargar archivos (Cloudinary)
- Verificar credenciales de Cloudinary en `appsettings.json`
- Confirmar configuración de firewall/proxy

### Errores de compilación
- Ejecutar `dotnet restore` para restaurar dependencias
- Verificar que .NET 8 SDK esté instalado

### Puerto en uso
- Cambiar puertos en `Properties/launchSettings.json`
- O detener el proceso que usa el puerto 5000/5001

---

## 📄 Licencia

Este proyecto es de uso académico/demostrativo.

---

## 👨‍💻 Autor

**John Klever**  
GitHub: [@Johnkl725](https://github.com/Johnkl725)

---

## 🙏 Agradecimientos

- Framework ASP.NET Core por Microsoft
- Comunidad de desarrolladores .NET
- Cloudinary por servicios de almacenamiento
- iText por generación de PDFs

---

## 📞 Soporte y Contacto

Para reportar problemas o sugerencias:
- Crear un [Issue en GitHub](https://github.com/Johnkl725/RimacSegurosApp/issues)
- Contactar al desarrollador

---

**Desarrollado con ❤️ usando ASP.NET Core MVC**
