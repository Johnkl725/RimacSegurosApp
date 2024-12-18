USE [master]
GO
/****** Object:  Database [BDSeguros]    Script Date: 16/11/2024 12:53:08 ******/
CREATE DATABASE [BDSeguros]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDSeguros', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BDSeguros.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BDSeguros_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BDSeguros_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BDSeguros] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDSeguros].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BDSeguros] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDSeguros] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDSeguros] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDSeguros] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDSeguros] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDSeguros] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BDSeguros] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDSeguros] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDSeguros] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDSeguros] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BDSeguros] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDSeguros] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDSeguros] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDSeguros] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDSeguros] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BDSeguros] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDSeguros] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BDSeguros] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BDSeguros] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BDSeguros] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDSeguros] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BDSeguros] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BDSeguros] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BDSeguros] SET  MULTI_USER 
GO
ALTER DATABASE [BDSeguros] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BDSeguros] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BDSeguros] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BDSeguros] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BDSeguros] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BDSeguros] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BDSeguros] SET QUERY_STORE = ON
GO
ALTER DATABASE [BDSeguros] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BDSeguros]
GO
/****** Object:  Table [dbo].[administrador]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[administrador](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[contraseña] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[beneficiario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[beneficiario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_vehiculo] [int] NULL,
	[contraseña] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distrito]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distrito](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [text] NULL,
	[id_provincia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documento]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[enlace] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListaBeneficiarios]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListaBeneficiarios](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[Fecha_union] [date] NULL,
	[Monto_aportado] [money] NULL,
	[Monto_a_pagar] [money] NULL,
	[Estado_siniestro] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personal]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[contraseña] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Poliza]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Poliza](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_beneficiario] [int] NOT NULL,
	[id_tipo] [int] NOT NULL,
	[fecha_inicio] [date] NULL,
	[fecha_fin] [date] NULL,
	[estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[presupuesto]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[presupuesto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_emision] [date] NULL,
	[monto_total] [money] NULL,
	[detalles] [text] NULL,
	[impuestos] [money] NULL,
	[costo_sin_impuestos] [money] NULL,
	[estado] [varchar](10) NULL,
	[descuento] [money] NULL,
	[enlace] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NULL,
	[ruc] [varchar](20) NULL,
	[telefono] [varchar](10) NULL,
	[correo] [varchar](30) NULL,
	[calificacion] [varchar](15) NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [text] NULL,
	[id_departamento] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reclamacion]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reclamacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_siniestro] [int] NOT NULL,
	[fecha_reclamacion] [date] NULL,
	[estado] [varchar](10) NULL,
	[tipo] [varchar](20) NULL,
	[descripcion] [text] NULL,
	[evidencia] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Siniestro]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Siniestro](
	[id_siniestro] [int] IDENTITY(1,1) NOT NULL,
	[id_departamento] [int] NULL,
	[id_provincia] [int] NULL,
	[id_distrito] [int] NULL,
	[id_documento] [int] NULL,
	[id_poliza] [int] NULL,
	[id_taller] [int] NULL,
	[id_presupuesto] [int] NULL,
	[tipo] [varchar](20) NULL,
	[fecha_siniestro] [date] NULL,
	[fecha_creacion] [date] NULL,
	[fecha_actualizacion] [date] NULL,
	[ubicacion] [varchar](30) NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_siniestro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Taller]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taller](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NULL,
	[nombre] [varchar](20) NULL,
	[direccion] [varchar](30) NULL,
	[telefono] [varchar](10) NULL,
	[correo] [varchar](30) NULL,
	[ciudad] [varchar](20) NULL,
	[tipo] [varchar](25) NULL,
	[capacidad] [int] NULL,
	[descripcion] [text] NULL,
	[calificacion] [varchar](50) NULL,
	[estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPoliza]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPoliza](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [text] NULL,
	[monto_mensual] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NOT NULL,
	[apellido1] [varchar](50) NULL,
	[apellido2] [varchar](50) NULL,
	[dni] [varchar](10) NOT NULL,
	[telefono] [varchar](9) NULL,
	[tipousuario] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[placa] [varchar](50) NOT NULL,
	[marca] [varchar](50) NOT NULL,
	[modelo] [varchar](50) NULL,
	[tipo] [varchar](20) NULL,
	[tarjeta_vehiculo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Siniestro] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Siniestro] ADD  DEFAULT (getdate()) FOR [fecha_actualizacion]
GO
ALTER TABLE [dbo].[administrador]  WITH CHECK ADD  CONSTRAINT [fk_id_usuario_admin] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[administrador] CHECK CONSTRAINT [fk_id_usuario_admin]
GO
ALTER TABLE [dbo].[beneficiario]  WITH CHECK ADD  CONSTRAINT [fk_id_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[beneficiario] CHECK CONSTRAINT [fk_id_usuario]
GO
ALTER TABLE [dbo].[beneficiario]  WITH CHECK ADD  CONSTRAINT [fk_id_vehiculo] FOREIGN KEY([id_vehiculo])
REFERENCES [dbo].[Vehiculo] ([id])
GO
ALTER TABLE [dbo].[beneficiario] CHECK CONSTRAINT [fk_id_vehiculo]
GO
ALTER TABLE [dbo].[Distrito]  WITH CHECK ADD  CONSTRAINT [fk_distrito_provincia] FOREIGN KEY([id_provincia])
REFERENCES [dbo].[Provincia] ([id])
GO
ALTER TABLE [dbo].[Distrito] CHECK CONSTRAINT [fk_distrito_provincia]
GO
ALTER TABLE [dbo].[ListaBeneficiarios]  WITH CHECK ADD  CONSTRAINT [fk_id_usuario_beneficiario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[beneficiario] ([id])
GO
ALTER TABLE [dbo].[ListaBeneficiarios] CHECK CONSTRAINT [fk_id_usuario_beneficiario]
GO
ALTER TABLE [dbo].[personal]  WITH CHECK ADD  CONSTRAINT [fk_id_usuario_personal] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[personal] CHECK CONSTRAINT [fk_id_usuario_personal]
GO
ALTER TABLE [dbo].[Poliza]  WITH CHECK ADD  CONSTRAINT [fk_id_beneficiario_poliza] FOREIGN KEY([id_beneficiario])
REFERENCES [dbo].[beneficiario] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Poliza] CHECK CONSTRAINT [fk_id_beneficiario_poliza]
GO
ALTER TABLE [dbo].[Poliza]  WITH CHECK ADD  CONSTRAINT [fk_id_tipo_poliza] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[TipoPoliza] ([id])
GO
ALTER TABLE [dbo].[Poliza] CHECK CONSTRAINT [fk_id_tipo_poliza]
GO
ALTER TABLE [dbo].[Provincia]  WITH CHECK ADD  CONSTRAINT [fk_provincia_departamento] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[Departamento] ([id])
GO
ALTER TABLE [dbo].[Provincia] CHECK CONSTRAINT [fk_provincia_departamento]
GO
ALTER TABLE [dbo].[reclamacion]  WITH CHECK ADD  CONSTRAINT [fk_id_siniestro_reclamacion] FOREIGN KEY([id_siniestro])
REFERENCES [dbo].[Siniestro] ([id_siniestro])
GO
ALTER TABLE [dbo].[reclamacion] CHECK CONSTRAINT [fk_id_siniestro_reclamacion]
GO
ALTER TABLE [dbo].[Siniestro]  WITH CHECK ADD  CONSTRAINT [fk_id_documento] FOREIGN KEY([id_documento])
REFERENCES [dbo].[documento] ([id])
GO
ALTER TABLE [dbo].[Siniestro] CHECK CONSTRAINT [fk_id_documento]
GO
ALTER TABLE [dbo].[Siniestro]  WITH CHECK ADD  CONSTRAINT [fk_id_poliza] FOREIGN KEY([id_poliza])
REFERENCES [dbo].[Poliza] ([id])
GO
ALTER TABLE [dbo].[Siniestro] CHECK CONSTRAINT [fk_id_poliza]
GO
ALTER TABLE [dbo].[Siniestro]  WITH CHECK ADD  CONSTRAINT [fk_id_presupuesto] FOREIGN KEY([id_presupuesto])
REFERENCES [dbo].[presupuesto] ([id])
GO
ALTER TABLE [dbo].[Siniestro] CHECK CONSTRAINT [fk_id_presupuesto]
GO
ALTER TABLE [dbo].[Siniestro]  WITH CHECK ADD  CONSTRAINT [fk_id_taller] FOREIGN KEY([id_taller])
REFERENCES [dbo].[Taller] ([id])
GO
ALTER TABLE [dbo].[Siniestro] CHECK CONSTRAINT [fk_id_taller]
GO
ALTER TABLE [dbo].[Taller]  WITH CHECK ADD  CONSTRAINT [fk_id_proveedor_taller] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[Proveedor] ([id])
GO
ALTER TABLE [dbo].[Taller] CHECK CONSTRAINT [fk_id_proveedor_taller]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorDni]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuarioPorDni]
    @Dni NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Dni, nombres, apellido1, apellido2  -- Incluye el campo 'Id' si es la clave primaria
    FROM Usuario
    WHERE Dni = @Dni;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarIdVehiculoBeneficiario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ActualizarIdVehiculoBeneficiario]
    @IdVehiculo INT
AS
BEGIN
    -- Actualizar solo el último beneficiario insertado con id_vehiculo = 3
    UPDATE Beneficiario
    SET id_vehiculo = @IdVehiculo
    WHERE id_vehiculo = 3
    AND id = (SELECT MAX(id) FROM Beneficiario);  -- Obtener el último insertado por id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarIDVehiculo]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AsignarIDVehiculo]
    @idBeneficiario INT,
    @IdVehiculo INT
AS
BEGIN
    -- Actualizar el id_vehiculo del beneficiario especificado
    UPDATE Beneficiario
    SET id_vehiculo = @IdVehiculo
    WHERE id_usuario = @idBeneficiario;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AsignarPolizaABeneficiario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_AsignarPolizaABeneficiario]
    @idPoliza INT,
    @idBeneficiario INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si la póliza existe
    IF NOT EXISTS (SELECT 1 FROM Poliza WHERE id = @idPoliza)
    BEGIN
        RAISERROR('No se encontró una póliza con el ID proporcionado.', 16, 1);
        RETURN;
    END

    -- Actualizar el id_beneficiario de la póliza especificada
    UPDATE Poliza
    SET id_beneficiario = @idBeneficiario
    WHERE id = @idPoliza;

    -- Verificar si la actualización fue exitosa
    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se pudo asignar la póliza al beneficiario.', 16, 1);
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AutenticarUsuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AutenticarUsuario]
    @IdUsuario INT,
    @TipoUsuario VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Comprobar en la tabla Beneficiario
    IF EXISTS (SELECT 1 FROM beneficiario WHERE id_usuario = @IdUsuario)
    BEGIN
        SET @TipoUsuario = 'Beneficiario';
        RETURN;
    END

    -- Comprobar en la tabla Personal
    IF EXISTS (SELECT 1 FROM personal WHERE id_usuario = @IdUsuario)
    BEGIN
        SET @TipoUsuario = 'Personal';
        RETURN;
    END

    -- Comprobar en la tabla Administrador
    IF EXISTS (SELECT 1 FROM administrador WHERE id_usuario = @IdUsuario)
    BEGIN
        SET @TipoUsuario = 'Administrador';
        RETURN;
    END

    -- Si no se encontró, establecer tipo como NULL
    SET @TipoUsuario = NULL;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_IdentificarTipoUsuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_IdentificarTipoUsuario]
    @DNI NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Buscar en la tabla Beneficiario
    IF EXISTS (SELECT 1 FROM beneficiario b
               JOIN Usuario u ON b.id_usuario = u.id
               WHERE u.dni = @DNI)
    BEGIN
        SELECT u.Id, 'Beneficiario' AS TipoUsuario
        FROM Usuario u
        JOIN beneficiario b ON u.id = b.id_usuario
        WHERE u.dni = @DNI;
        RETURN;
    END

    -- Buscar en la tabla Personal
    IF EXISTS (SELECT 1 FROM personal p
               JOIN Usuario u ON p.id_usuario = u.id
               WHERE u.dni = @DNI)
    BEGIN
        SELECT u.Id, 'Personal' AS TipoUsuario
        FROM Usuario u
        JOIN personal p ON u.id = p.id_usuario
        WHERE u.dni = @DNI;
        RETURN;
    END

    -- Buscar en la tabla Administrador
    IF EXISTS (SELECT 1 FROM administrador a
               JOIN Usuario u ON a.id_usuario = u.id
               WHERE u.dni = @DNI)
    BEGIN
        SELECT u.Id, 'Administrador' AS TipoUsuario
        FROM Usuario u
        JOIN administrador a ON u.id = a.id_usuario
        WHERE u.dni = @DNI;
        RETURN;
    END

    -- Si no se encontró el usuario en ninguna tabla, retornar NULL
    SELECT NULL AS TipoUsuario;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarVehiculo]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertarVehiculo]
    @Placa VARCHAR(50),
    @Marca VARCHAR(50),
    @Modelo VARCHAR(50),
    @Tipo VARCHAR(20),
    @TarjetaVehiculo INT,
    @IdVehiculo INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vehiculo (placa, marca, modelo, tipo, tarjeta_vehiculo)
    VALUES (@Placa, @Marca, @Modelo, @Tipo, @TarjetaVehiculo);

    -- Obtener el Id del vehículo recién insertado
    SET @IdVehiculo = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerContraseñaHasheadaUsuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerContraseñaHasheadaUsuario]
    @IdUsuario INT,
    @ContraseñaHash VARCHAR(60) OUTPUT  -- Este parámetro retornará la contraseña hasheada
AS
BEGIN
    SET NOCOUNT ON;

    -- Comprobar en la tabla Beneficiario
    IF EXISTS (SELECT 1 FROM beneficiario WHERE id_usuario = @IdUsuario)
    BEGIN
        -- Obtener la contraseña hasheada del beneficiario
        SELECT @ContraseñaHash = contraseña FROM beneficiario WHERE id_usuario = @IdUsuario;
        RETURN;
    END

    -- Comprobar en la tabla Personal
    IF EXISTS (SELECT 1 FROM personal WHERE id_usuario = @IdUsuario)
    BEGIN
        -- Obtener la contraseña hasheada del personal
        SELECT @ContraseñaHash = contraseña FROM personal WHERE id_usuario = @IdUsuario;
        RETURN;
    END

    -- Comprobar en la tabla Administrador
    IF EXISTS (SELECT 1 FROM administrador WHERE id_usuario = @IdUsuario)
    BEGIN
        -- Obtener la contraseña hasheada del administrador
        SELECT @ContraseñaHash = contraseña FROM administrador WHERE id_usuario = @IdUsuario;
        RETURN;
    END

    -- Si no se encontró, devolver NULL
    SET @ContraseñaHash = NULL;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerIdPorDNI]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerIdPorDNI]
    @DNI VARCHAR(10),
    @IdUsuario INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @IdUsuario = id
    FROM Usuario
    WHERE dni = @DNI;

    -- Si no se encuentra el usuario, se establece @IdUsuario en NULL
    IF @IdUsuario IS NULL
    BEGIN
        SET @IdUsuario = -1; -- Valor especial para indicar que no se encontró
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarAdministrador]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarAdministrador]
    @id_usuario INT,
    @Password VARCHAR(60)
AS
BEGIN
    INSERT INTO Administrador (id_usuario, contraseña)
    VALUES (@id_usuario, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarBeneficiario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarBeneficiario]
    @id_usuario INT,
    @IdVehiculo INT = NULL,  -- Permitir NULL para IdVehiculo
    @Password VARCHAR(60)
AS
BEGIN
    INSERT INTO Beneficiario (id_usuario, id_vehiculo, contraseña)
    VALUES (@id_usuario, @IdVehiculo, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPersonal]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarPersonal]
    @id_usuario INT,
    @Password VARCHAR(60)
AS
BEGIN
    INSERT INTO Personal (id_usuario, contraseña)
    VALUES (@id_usuario, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPoliza]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegistrarPoliza]
    @id_beneficiario INT,
    @id_tipo INT,
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @estado VARCHAR(10),  -- Actualizado a VARCHAR(10)
    @id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar nueva póliza
    INSERT INTO Poliza (id_beneficiario, id_tipo, fecha_inicio, fecha_fin, estado)
    VALUES (@id_beneficiario, @id_tipo, @fecha_inicio, @fecha_fin, @estado);

    -- Obtener el ID de la póliza recién insertada
    SET @id = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarSiniestro]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarSiniestro]
    @Tipo NVARCHAR(20),
    @FechaSiniestro DATE,
    @Departamento INT,
    @Provincia INT,
    @Distrito INT,
    @Ubicacion NVARCHAR(30),
    @Descripcion NVARCHAR(MAX),
    @IdDocumento INT = 1,  -- Valor predeterminado de 1
    @IdPoliza INT = 1,     -- Valor predeterminado de 1
    @IdTaller INT = 1,     -- Valor predeterminado de 1
    @IdPresupuesto INT = 1,-- Valor predeterminado de 1
    @IdSiniestro INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar el nuevo siniestro en la tabla Siniestro
    INSERT INTO Siniestro (
        tipo, fecha_siniestro, fecha_creacion, fecha_actualizacion, ubicacion, descripcion, 
        id_departamento, id_provincia, id_distrito, id_documento, id_poliza, id_taller, id_presupuesto
    )
    VALUES (
        @Tipo, @FechaSiniestro, GETDATE(), GETDATE(), @Ubicacion, @Descripcion, 
        @Departamento, @Provincia, @Distrito, @IdDocumento, @IdPoliza, @IdTaller, @IdPresupuesto
    );

    -- Obtener el ID del siniestro recién insertado
    SET @IdSiniestro = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarUsuario]
    @Nombres VARCHAR(50),
    @Apellido1 VARCHAR(50),
    @Apellido2 VARCHAR(50),
    @DNI VARCHAR(10),
    @Telefono VARCHAR(7),
    @UserType VARCHAR(20), -- Nuevo parámetro
    @IdUsuario INT OUTPUT
AS
BEGIN
    INSERT INTO Usuario (nombres, apellido1, apellido2, dni, telefono, tipousuario)
    VALUES (@Nombres, @Apellido1, @Apellido2, @DNI, @Telefono, @UserType);

    SET @IdUsuario = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spActualizarProveedor]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spActualizarProveedor]
    @Id INT,
    @Nombre VARCHAR(20),
    @Ruc VARCHAR(20),
    @Telefono VARCHAR(10),
    @Correo VARCHAR(30),
    @Calificacion VARCHAR(15),
    @Descripcion TEXT
AS
BEGIN
    UPDATE Proveedor
    SET nombre = @Nombre,
        ruc = @Ruc,
        telefono = @Telefono,
        correo = @Correo,
        calificacion = @Calificacion,
        descripcion = @Descripcion
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spActualizarTaller]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spActualizarTaller]
    @Id INT,
    @nombre VARCHAR(20),
    @direccion VARCHAR(30),
    @telefono VARCHAR(10),
    @correo VARCHAR(30),
    @ciudad VARCHAR(20),
    @tipo VARCHAR(25),
    @capacidad INT,
    @descripcion TEXT,
    @calificacion VARCHAR(15),
    @estado VARCHAR(10)
AS
BEGIN
    UPDATE Taller
    SET nombre = @nombre, direccion = @direccion, telefono = @telefono, correo = @correo,
        ciudad = @ciudad, tipo = @tipo, capacidad = @capacidad, descripcion = @descripcion,
        calificacion = @calificacion, estado = @estado
    WHERE id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[spAgregarProveedor]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAgregarProveedor]
    @Nombre VARCHAR(20),
    @Ruc VARCHAR(20),
    @Telefono VARCHAR(10),
    @Correo VARCHAR(30),
    @Calificacion VARCHAR,
    @Descripcion TEXT,
    @Id INT OUTPUT  -- Parámetro de salida opcional
AS
BEGIN
    INSERT INTO Proveedor (nombre, ruc, telefono, correo, calificacion, descripcion)
    VALUES (@Nombre, @Ruc, @Telefono, @Correo, @Calificacion, @Descripcion);

    -- Asigna el ID generado al parámetro de salida
    SET @Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spAgregarTaller]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAgregarTaller]
    @id_proveedor INT,
    @nombre VARCHAR(20),
    @direccion VARCHAR(30),
    @telefono VARCHAR(10),
    @correo VARCHAR(30),
    @ciudad VARCHAR(20),
    @tipo VARCHAR(25),
    @capacidad INT,
    @descripcion TEXT,
    @calificacion VARCHAR(15),
    @estado VARCHAR(10),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Taller (id_proveedor, nombre, direccion, telefono, correo, ciudad, tipo, capacidad, descripcion, calificacion, estado)
    VALUES (@id_proveedor, @nombre, @direccion, @telefono, @correo, @ciudad, @tipo, @capacidad, @descripcion, @calificacion, @estado);

    SET @Id = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[spEliminarProveedor]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarProveedor]
    @Id INT
AS
BEGIN
    DELETE FROM Proveedor WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminarTaller]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarTaller]
    @Id INT
AS
BEGIN
    DELETE FROM Taller WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[spObtenerProveedores]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerProveedores]
AS
BEGIN
    SELECT * FROM Proveedor
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerTalleres]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerTalleres]
AS
BEGIN
    SELECT *
    FROM Taller;
END;
GO
/****** Object:  StoredProcedure [dbo].[spObtenerTalleresConProveedor]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerTalleresConProveedor]
AS
BEGIN
    SELECT 
        T.id AS Id,
        T.nombre AS Nombre,
        T.direccion AS Direccion,
        T.ciudad AS Ciudad,
        T.capacidad AS Capacidad,
        T.id_proveedor AS IdProveedor,
        P.nombre AS NombreProveedor
    FROM Taller T
    INNER JOIN Proveedor P ON T.id_proveedor = P.id;
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerTallerPorId]    Script Date: 16/11/2024 12:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[spObtenerTallerPorId]
    @Id INT
AS
BEGIN
    SELECT 
        id AS Id,
        id_proveedor ,
        nombre AS Nombre,
        direccion AS Direccion,
        telefono AS Telefono,
        correo AS Correo,
        ciudad AS Ciudad,
        tipo AS Tipo,
        capacidad AS Capacidad,
        descripcion AS Descripcion,
        calificacion AS Calificacion,
        estado AS Estado
    FROM Taller
    WHERE id = @Id;
END
GO
USE [master]
GO
ALTER DATABASE [BDSeguros] SET  READ_WRITE 
GO
