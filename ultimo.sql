USE [master]
GO
/****** Object:  Database [BDSeguros]    Script Date: 26/11/2024 10:42:47 ******/
CREATE DATABASE [BDSeguros]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDSeguros', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BDSeguros.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
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
/****** Object:  User [saa]    Script Date: 26/11/2024 10:42:47 ******/
CREATE USER [saa] FOR LOGIN [saa] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[administrador]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[beneficiario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Departamento]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Distrito]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[documento]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[DocumentosReclamacion]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentosReclamacion](
	[IdDocumento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Extension] [varchar](50) NOT NULL,
	[IdReclamacion] [int] NOT NULL,
	[Url] [nvarchar](900) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListaBeneficiarios]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[personal]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Poliza]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[presupuesto]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Proveedor]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Provincia]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[reclamacion]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reclamacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_siniestro] [int] NOT NULL,
	[fecha_reclamacion] [date] NULL,
	[estado] [varchar](10) NULL,
	[descripcion] [text] NULL,
	[tipo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Siniestro]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[Taller]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[TipoPoliza]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 26/11/2024 10:42:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 26/11/2024 10:42:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 26/11/2024 10:42:47 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[administrador] ON 

INSERT [dbo].[administrador] ([id], [id_usuario], [contraseña]) VALUES (1, 12, N'$2a$11$nBW5UzHL4GniLDt7R.PCvuvHVWd5TGYiB/BFe68aniqbdozgtZHEC')
INSERT [dbo].[administrador] ([id], [id_usuario], [contraseña]) VALUES (2, 17, N'$2a$11$wUGFgnW/F9mv/SPWtuYu5OogcjiK1DAJpc38AvrDf7dTrs9/JhzRq')
INSERT [dbo].[administrador] ([id], [id_usuario], [contraseña]) VALUES (3, 18, N'$2a$11$Qt.05iIsU9AXPkDl3TQaT.z/C1ytNX3fxDls9519iuCer.67iOpCW')
INSERT [dbo].[administrador] ([id], [id_usuario], [contraseña]) VALUES (4, 53, N'$2a$11$OcvJijzpFd.RyYQR5QUZ6eUIx8D.BhSiZWIqtxis2vNb7CvbREivq')
SET IDENTITY_INSERT [dbo].[administrador] OFF
GO
SET IDENTITY_INSERT [dbo].[beneficiario] ON 

INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (2, 4, NULL, N'$2a$11$amj5QFzdscb8O9VZjRazqOsRlKjL1ZNdgF7cGYGZZcCIFkkIQR1O6')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (3, 5, NULL, N'$2a$11$apIZ4Q/UXFsWUqHbLDjhj.Y0CQkNb6W3h.6gddYIO1nn.f98fKbAe')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (4, 6, NULL, N'$2a$11$fg8WGyhR1bG6lRJkrw74kulql6Hbj.8uSrYLUoQGEYWNNVzv7sSSy')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (5, 7, NULL, N'$2a$11$/9Ph3jOnLt8Lbpsn9vsgzOKYAbsfOSU0JnLwaXvd0YAogDoEnmQl.')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (6, 8, NULL, N'$2a$11$uczBt1b5B2RDezAWfHHkze2E29XCHzlb8oYhEnxo6vD3Pz6Tge2wu')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (7, 9, NULL, N'$2a$11$ErC.9q9zHcRSYnFuAjno6u8hn.FSe0Ndp.ZsSjIZFHE8whf6dVA.G')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (8, 10, NULL, N'$2a$11$daQxnuXmhHzwvkOc9FEq6eLy5TF96NMHPKb7WsafRGAlPXUxA2Dsm')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (9, 11, 10, N'$2a$11$LsP0Zs1GAHXJjswhkjr5fO2ai8MdAl9XV85jawF8L202r.fEu11Pm')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (10, 15, 19, N'$2a$11$aupn9YT5fAmZ1bP3jnfgT.49q.x5XBwl/yVW8H7kGG/fHhtz8CJaC')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (11, 20, 13, N'$2a$11$xHGquTKGiYxtjowPnnQkLODNYX8fUjadAeDmeXiD8JjJrvkFzeokm')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (12, 21, 15, N'$2a$11$6Y6/VcIJGGTiC/e.Z.lugO6EF9gvXq44D6dhw06FgULbD29qWgvbK')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (13, 22, 18, N'$2a$11$LUlm579mMjpBFrqiAlUvk.G//UCJth5e0QEfZiL53XHSBsG6H4ATK')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (14, 23, NULL, N'$2a$11$Mu0sNT9tZS1Q.JFvVImMrOtHj4S1Cp97AjFv0dOnM8X68xQr4ssjK')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (15, 25, NULL, N'$2a$11$3SnTZwEMiXmSXta0tRly3.DbtpuEdOdMgSTZTtbrL6IqIiI/c1my.')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (16, 26, NULL, N'$2a$11$2D15oWIlefoRR1wHAmgEBetHGBIPfSAZjMSqC7j/FjTvukMnqd0oG')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (17, 27, NULL, N'$2a$11$xddhbFAUX/rT5FZqEhox6uiYfRW/2qcgzK3x.5aBhCA4iWRjFX39y')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (18, 29, 22, N'$2a$11$2pPvx7hf/TxzkyhlDM13HuFaCLKkGiyLz8ZIHdRm6YEoui1yJvogq')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (19, 30, 23, N'$2a$11$xp1xQ9diZjU00v1gAtPz0.aOAESiV.f31g1u3ucRZGDqSPK7h7qL6')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (20, 31, 24, N'$2a$11$YWG4oG12thBE6RbwvBYl.OwxwSu3jbfcmeOZmOo1CKTNH6ZvmCi9i')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (21, 32, 25, N'$2a$11$RVg8P9Qn5SyLNerhWbvXdeWR9HtL/syWqll0CCDqkQgDaGRUZNLQa')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (22, 33, 26, N'$2a$11$orEF/AFtAeFQcethNBu1YeKwYkfNh9oWkyI/3jze/bVXUo.FSlXp2')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (23, 34, 28, N'$2a$11$UiMxcgx0uMsw591FN1HQ.upemyyv263UNNe33W6lr7hIoAHhCXCb.')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (24, 35, 39, N'$2a$11$dffLMlE4bcEiZSTX903/5OxDY2Ofprg5KSvaTsamwQBmjzXGzM6Vq')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (25, 36, 29, N'$2a$11$r9iAJ50Ryn8LxIfRPY65E.JPo2M9OO4NRseVLJPzqsaFFThT/O7Cu')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (26, 37, 30, N'$2a$11$knk9ZEUHNHRwpJZyqhwvoeqd6BkcGzePPOjyFv0xLVmD/Wwov/gPO')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (27, 38, 32, N'$2a$11$dm55GF/qV1xfkee1zZIgRen3Ijtz0o00joqH2j0ZvonlAUboto6jm')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (28, 39, NULL, N'$2a$11$Z8gW0U5ezRj9K036orB7T.TADGTVz0zo/Jp6bDcIyjvukk8H7ZnGy')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (29, 40, 33, N'$2a$11$ZaSnl5go3kokzhoRKDIvqu/tn3xfos3DDfNOypXCY7qN67mFFHd82')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (30, 41, NULL, N'$2a$11$6K0Fd3P7ecvcPY0H2CJIzeDQQU/v2bePLP8BzJItpuDwb9Fc/tV8q')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (31, 42, 35, N'$2a$11$gFLIja6v4510QXwYRe8YPup/XB.2RTYDnLKQsh30wWjFOSrGEFUPO')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (32, 43, 36, N'$2a$11$pU0uAwvVseOPDE1x77.s6eEX8GSL1RbaOAaAFRAyCfFHxBoIrA.mW')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (33, 44, 37, N'$2a$11$.T51NLSuYHKKQItQdTGktOemlMkw9TkB/1EZXDX5UHkSPDZTElpB2')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (34, 45, 38, N'$2a$11$MZmqBhoZDcaI5hP7IhfaoOTSymIFrliu9nn1KCIKAoXWCeRaArr96')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (35, 46, NULL, N'$2a$11$xSrbbBXHzm7XXlyyqgkdw.qeGG/98eKf4i.d3Onj8WkYiCxuXEA7S')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (36, 47, 40, N'$2a$11$Tm4zvh.Buy.OyGk1NEVLdOEYptGLzioAQa8Xk/y6SsnkLMenL3Iey')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (37, 48, 42, N'$2a$11$9FdPoyLlxQzbfE80Bg7n3.o4DghpudsZlYAKHkNj.T5d9AVmhgE7.')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (38, 49, 43, N'$2a$11$pzHTUln8bOAspDQdE9lYD.fmaUVDK3QQ6p1yALLZxj6BZnqP9GpcW')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (39, 50, 44, N'$2a$11$3PgJW1Dw1Bw3W0O7KHamae5IzHFBgccTu5g0wq2aTRjXLsBYxfi6O')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (40, 52, 45, N'$2a$11$XQSFGMpbImWgjAA7VsGAIeuG3nqVUurBaCYYwYyRuLIFkbH2DtAtS')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (41, 54, NULL, N'$2a$11$vWkEOYmBhbn0csd.mgSuOOJdX6tJ9IuinmL0GqowH10MM63ZZZ2La')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (42, 55, 47, N'$2a$11$ORZftaNnhkRWbcOprVOrIe/5C/1Le8RVx/LtxfwEol/LF0YePLf4C')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (43, 60, NULL, N'$2a$11$R/afNc1YbPQJLCMWjVFtoOUXizCbc02xep/7ERtkhEtJKHtd95icG')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (44, 61, NULL, N'$2a$11$zjutlT0jIC2Qfdm6edyDBe9cdsI1m0FqJMjkeCshgYT1zNgz.Uxci')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (45, 62, 48, N'$2a$11$7U3W3G6y4YYPlIG3i/y9HuZr59vT./2hS46tli8QI8Skfnk4uPZVe')
SET IDENTITY_INSERT [dbo].[beneficiario] OFF
GO
SET IDENTITY_INSERT [dbo].[Departamento] ON 

INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (1, N'Amazonas')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (2, N'Áncash')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (3, N'Apurímac')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (4, N'Arequipa')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (5, N'Ayacucho')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (6, N'Cajamarca')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (7, N'Callao')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (8, N'Cusco')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (9, N'Huancavelica')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (10, N'Huánuco')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (11, N'Ica')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (12, N'Junín')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (13, N'La Libertad')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (14, N'Lambayeque')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (15, N'Lima')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (16, N'Loreto')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (17, N'Madre de Dios')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (18, N'Moquegua')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (19, N'Pasco')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (20, N'Piura')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (21, N'Puno')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (22, N'San Martín')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (23, N'Tacna')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (24, N'Tumbes')
INSERT [dbo].[Departamento] ([id], [descripcion]) VALUES (25, N'Ucayali')
SET IDENTITY_INSERT [dbo].[Departamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Distrito] ON 

INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (1, N'Miraflores', 1)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (2, N'San Isidro', 1)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (3, N'La Molina', 1)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (4, N'Bellavista', 2)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (5, N'Carmen de La Legua Reynoso', 2)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (6, N'Yanahuara', 3)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (7, N'Cayma', 3)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (8, N'Cerro Colorado', 3)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (9, N'Santiago', 4)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (10, N'San Jerónimo', 4)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (11, N'Wanchaq', 4)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (12, N'Trujillo', 5)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (13, N'La Esperanza', 5)
INSERT [dbo].[Distrito] ([id], [descripcion], [id_provincia]) VALUES (14, N'Huanchaco', 5)
SET IDENTITY_INSERT [dbo].[Distrito] OFF
GO
SET IDENTITY_INSERT [dbo].[documento] ON 

INSERT [dbo].[documento] ([id], [enlace]) VALUES (1, N'documento1.pdf')
INSERT [dbo].[documento] ([id], [enlace]) VALUES (2, N'documento2.pdf')
INSERT [dbo].[documento] ([id], [enlace]) VALUES (3, N'documento3.pdf')
INSERT [dbo].[documento] ([id], [enlace]) VALUES (4, N'ruta/del/documento/por/defecto')
SET IDENTITY_INSERT [dbo].[documento] OFF
GO
SET IDENTITY_INSERT [dbo].[DocumentosReclamacion] ON 

INSERT [dbo].[DocumentosReclamacion] ([IdDocumento], [Nombre], [Extension], [IdReclamacion], [Url]) VALUES (1, N'reclamacion_detallada.jpg', N'.jpg', 1022, N'https://res.cloudinary.com/dovonolyt/image/upload/v1732556868/RimacSeguros/Siniestro_57-Reclamacion_1022/Reclamacion_detallada.jpg')
INSERT [dbo].[DocumentosReclamacion] ([IdDocumento], [Nombre], [Extension], [IdReclamacion], [Url]) VALUES (2, N'barcelona.png', N'.png', 1023, N'https://res.cloudinary.com/dovonolyt/image/upload/v1732557711/RimacSeguros/Siniestro_57-Reclamacion_1023/barcelona.png')
INSERT [dbo].[DocumentosReclamacion] ([IdDocumento], [Nombre], [Extension], [IdReclamacion], [Url]) VALUES (3, N'Captura de pantalla 2024-08-21 080732.png', N'.png', 1024, N'https://res.cloudinary.com/dovonolyt/image/upload/v1732596430/RimacSeguros/Siniestro_58-Reclamacion_1024/Captura de pantalla 2024-08-21 080732.png')
INSERT [dbo].[DocumentosReclamacion] ([IdDocumento], [Nombre], [Extension], [IdReclamacion], [Url]) VALUES (4, N'Captura de pantalla 2024-08-21 093306.png', N'.png', 1025, N'https://res.cloudinary.com/dovonolyt/image/upload/v1732597685/RimacSeguros/Siniestro_1061-Reclamacion_1025/Captura de pantalla 2024-08-21 093306.png')
SET IDENTITY_INSERT [dbo].[DocumentosReclamacion] OFF
GO
SET IDENTITY_INSERT [dbo].[personal] ON 

INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (1, 2, N'$2a$11$77klPEoA0/.jXksjCS7yEOVRuQsOiwnx.QhE5R5ociIE8tK1n5qZe')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (2, 13, N'$2a$11$AaS3mxizX8txOh2pAXk7CO0EjTpMYFVVDu4NSx/RLk1v5WrgughKy')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (3, 16, N'$2a$11$I/DxJ7v5kFp76ikE0DxeYuicryohkDbR/pOulbQ8GR6tv5Y/1N.oW')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (4, 19, N'$2a$11$4VnI84/8FJAqIZt1n.DX1.aBIpfvaJgft/VvEtCxeGYlZrH6dgCKG')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (5, 51, N'$2a$11$m4JKKozpSZjtqmE581WKd.RunjO9cCtVpcf0sl8fmm.g.lbzTstbG')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (6, 56, N'$2a$11$BaazxE8ftxfZwyPyIF5/lOz6ppJ9VcBhild.DKbHkhx/n98Hnxc8S')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (7, 57, N'$2a$11$Pg05AMpk9GJZEUgyqLrF1usAmmFCY5GiojVJbBQnxsO41SLsEf0v.')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (8, 59, N'$2a$11$DRhSuLmFjgdxPtomXmIccu1dlUbVC2eJq4eM.HN.KrtyHd8GssqO2')
SET IDENTITY_INSERT [dbo].[personal] OFF
GO
SET IDENTITY_INSERT [dbo].[Poliza] ON 

INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (4, 2, 1, CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (5, 21, 2, CAST(N'2024-11-12' AS Date), CAST(N'2024-11-14' AS Date), N'Inactivo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (6, 3, 1, CAST(N'2024-11-13' AS Date), CAST(N'2025-11-13' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (7, 5, 1, CAST(N'2024-11-13' AS Date), CAST(N'2025-11-13' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (11, 35, 2, CAST(N'2025-12-20' AS Date), CAST(N'2026-12-20' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (12, 36, 3, CAST(N'2024-10-25' AS Date), CAST(N'2025-11-25' AS Date), N'Inactivo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (13, 38, 3, CAST(N'2024-10-25' AS Date), CAST(N'2500-10-26' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (14, 39, 3, CAST(N'2024-12-20' AS Date), CAST(N'2025-12-20' AS Date), N'inactivo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (15, 40, 1, CAST(N'2024-02-21' AS Date), CAST(N'2025-01-23' AS Date), N'Activo')
INSERT [dbo].[Poliza] ([id], [id_beneficiario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (16, 42, 3, CAST(N'2024-11-21' AS Date), CAST(N'2025-11-21' AS Date), N'Activo')
SET IDENTITY_INSERT [dbo].[Poliza] OFF
GO
SET IDENTITY_INSERT [dbo].[presupuesto] ON 

INSERT [dbo].[presupuesto] ([id], [fecha_emision], [monto_total], [detalles], [impuestos], [costo_sin_impuestos], [estado], [descuento], [enlace]) VALUES (1, CAST(N'2024-11-05' AS Date), 0.0000, N'Detalles de prueba', 0.0000, 0.0000, N'Pagado', 0.0000, N'enlace_prueba')
INSERT [dbo].[presupuesto] ([id], [fecha_emision], [monto_total], [detalles], [impuestos], [costo_sin_impuestos], [estado], [descuento], [enlace]) VALUES (3, CAST(N'2024-11-12' AS Date), 12000.0000, N'Para todo el año', 1500.0000, 18500.0000, N'Pagado', 0.0000, N'presupuesto.com.pe')
SET IDENTITY_INSERT [dbo].[presupuesto] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedor] ON 

INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (1, N'Proveedor Ejemplo1', N'12345678901', N'123456789', N'proveedor@ejemplo.com', N'A', N'Proveedor de prueba')
INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (4, N'hola', N'das', N'23', N'daniel', N'A', N'sa')
INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (5, N'proveedornuevo', N'45132431', N'93994', N'm@hotmail.com', N'B', N'nuevo')
INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (6, N'SegurosPerro', N'1231245642', N'2134351', N'olis@gmail.com', N'B', N'Nada mal')
INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (7, N'ProveedorCus', N'123415345', N'231231', N'cus@gmail.com', N'C', N'Nuevo cus 4 y 5')
SET IDENTITY_INSERT [dbo].[Proveedor] OFF
GO
SET IDENTITY_INSERT [dbo].[Provincia] ON 

INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (1, N'Lima', 15)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (2, N'Barranca', 15)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (3, N'Cañete', 15)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (4, N'Huaral', 15)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (5, N'Huarochirí', 15)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (6, N'Cusco', 8)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (7, N'Arequipa', 4)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (8, N'Camaná', 4)
INSERT [dbo].[Provincia] ([id], [descripcion], [id_departamento]) VALUES (9, N'Islay', 4)
SET IDENTITY_INSERT [dbo].[Provincia] OFF
GO
SET IDENTITY_INSERT [dbo].[reclamacion] ON 

INSERT [dbo].[reclamacion] ([id], [id_siniestro], [fecha_reclamacion], [estado], [descripcion], [tipo]) VALUES (1022, 57, CAST(N'2024-11-25' AS Date), N'Pendiente', N'Necesito mi dinero.', N'Robo')
INSERT [dbo].[reclamacion] ([id], [id_siniestro], [fecha_reclamacion], [estado], [descripcion], [tipo]) VALUES (1023, 57, CAST(N'2024-11-25' AS Date), N'Pendiente', N'necesito mi pago', N'Robo')
INSERT [dbo].[reclamacion] ([id], [id_siniestro], [fecha_reclamacion], [estado], [descripcion], [tipo]) VALUES (1024, 58, CAST(N'2024-11-25' AS Date), N'Pendiente', N'das', N'Robo')
INSERT [dbo].[reclamacion] ([id], [id_siniestro], [fecha_reclamacion], [estado], [descripcion], [tipo]) VALUES (1025, 1061, CAST(N'2024-11-26' AS Date), N'Pendiente', N'h', N'Gastos Asociados')
SET IDENTITY_INSERT [dbo].[reclamacion] OFF
GO
SET IDENTITY_INSERT [dbo].[Siniestro] ON 

INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (41, 15, 1, 3, 1, 4, 14, 3, N'Choque', CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'Molina 123', N'Una moto choco mi carro')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (42, 15, 3, 7, 1, 4, 13, 3, N'Daños Menores', CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'Sur 123', N'Una persona me tiró un golpe')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (43, 15, 1, 1, 2, 5, 4, 1, N'Daños Menores', CAST(N'2024-11-09' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'Paseo la republica 111', N'Nos rompieron las lunas')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (44, 15, 1, 2, 2, 5, 4, 1, N'Daños Menores', CAST(N'2001-11-23' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'dsa', N'sda')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (45, 15, 1, 1, 2, 5, 4, 1, N'Daños Menores', CAST(N'2000-10-20' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), N'UV STA MARINA ASDASD', N'SE CHOCO EL CARRO ')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (46, 15, 1, 2, 2, 5, 10, 1, N'Daños Menores', CAST(N'2024-12-03' AS Date), CAST(N'2024-11-15' AS Date), CAST(N'2024-11-15' AS Date), N'uv sta marina perro', N'no hubo mas cosas')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (47, 15, 1, 3, 2, 5, 15, 1, N'Pérdida Total', CAST(N'2025-10-25' AS Date), CAST(N'2024-11-16' AS Date), CAST(N'2024-11-16' AS Date), N'MOLLES 16345', N'SE MURIO MI CARRO')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (48, 15, 2, 4, 2, 5, 4, 1, N'Choque', CAST(N'2026-12-21' AS Date), CAST(N'2024-11-16' AS Date), CAST(N'2024-11-16' AS Date), N'UV STA MARINA ASDASDasdas', N'niuevo choque')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (49, 15, 1, 1, 2, 5, 4, 1, N'Robo', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-18' AS Date), CAST(N'2024-11-18' AS Date), N'av miraflores', N'-')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (50, 15, 1, 2, 2, 5, 13, 1, N'Choque', CAST(N'2004-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'san isidro', N'sdsdsd')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (51, 15, 1, 2, 2, 5, 11, 1, N'Choque', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'chancay', N'-')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (57, 15, 1, 2, 1, 15, 15, 1, N'Choque', CAST(N'2024-11-11' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'chancay', N'-')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (58, 15, 1, 2, 1, 15, 10, 1, N'Daños Menores', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'av san isidro', N'Me choco una combi')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (59, 15, 1, 2, 1, 15, 12, 1, N'Robo', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'miraflores av', N'necesito un pago')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (60, 15, 1, 1, 1, 15, 4, 1, N'Daños Menores', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'miraflores av', N'necesito un pago')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (61, 15, 2, 4, 1, 15, 1, 1, N'Robo', CAST(N'2024-11-21' AS Date), CAST(N'2024-11-19' AS Date), CAST(N'2024-11-19' AS Date), N'miraflores av', N'ME ROBARON')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (1059, 15, 1, 3, 1, 15, 14, 1, N'Robo', CAST(N'2025-11-21' AS Date), CAST(N'2024-11-25' AS Date), CAST(N'2024-11-25' AS Date), N'chancay', N'me robaron mi gucci')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (1060, 15, 1, 1, 1, 15, 1, 1, N'Robo', CAST(N'2024-02-11' AS Date), CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), N's', N'sss')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (1061, 15, 1, 2, 1, 15, 10, 1, N'Robo', CAST(N'2026-01-11' AS Date), CAST(N'2024-11-26' AS Date), CAST(N'2024-11-26' AS Date), N's', N'sa')
SET IDENTITY_INSERT [dbo].[Siniestro] OFF
GO
SET IDENTITY_INSERT [dbo].[Taller] ON 

INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (1, 1, N'Taller Ejemplo', N'Dirección Ejemplo', N'123456789', N'taller@ejemplo.com', N'Ciudad Ejemplo', N'Tipo Ejemplo', 140, N'Descripción del taller', N'5', N'Activo')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (4, 1, N'Taller de Prueba', N'Av. Ejemplo 123', N'123456789', N'taller@ejemplo.com', N'Ciudad Ejemplo', N'General', 100, N'Descripción de prueba', N'5', N'Activo')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (5, 1, N'Taller Ejemplo', N'Dirección Ejemplo', N'123456789', N'ejemplo@taller.com', N'Ciudad Ejemplo', N'General', 100, N'Descripción del taller', N'5', N'Activo')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (10, 1, N'da', N'das', N'da', N'dassd', N'das', N'sad', 2, N'sda', N'13', N'sa')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (11, 4, N'da', N'da', N'dsadas', N'daniel', N'das', N'da', 2, N'das', N'das', N'sda')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (12, 1, N'segurosnuevo', N'seguros', N'seguros2', N'mau@gmail.com', N'lima', N'541243', 235235, N'235235', N'235235', N'3232')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (13, 1, N'segurosviejo', N'uv asdasdasdas', N'45435345', N'an2a@gmail.com', N'lima', N'seda', 21111, N'antiguo pabellon', N'b', N'reciente')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (14, 6, N'NuevoTaller2', N'UV STA MARINA', N'5561221', N'correito@gmail.com', N'Lima', N'Daños menores', 1000, N'Es una muyt', N' buena taller', N'buena')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (15, 7, N'TallerCus1', N'seguros av z', N'234523', N'cusnuevo@gmail.com', N'Junin', N'B', 2134, N'nuevo cus 4', N'B', N'activo2')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (17, 4, N'dnaiel', N'da', N'938240845', N'daniael@gmail.com', N'lima', N'Pintura', 2, N'daniel', N'A', N'Activo')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (18, 4, N'hola', N'daniel', N'938240845', N'daniel@gmail.com', N'lima', N'Mecánico', 1, N'd', N'B', N'Activo')
SET IDENTITY_INSERT [dbo].[Taller] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoPoliza] ON 

INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (1, N'Esencial', 100.0000)
INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (2, N'Intermedia', 200.0000)
INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (3, N'Premium', 300.0000)
SET IDENTITY_INSERT [dbo].[TipoPoliza] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (2, N'Luke', N'Vader', N'Woks', N'348482', N'123341', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (3, N'Lucas', N'Moura', N'Calex', N'1111122', N'3333442', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (4, N'Loks', N'Artorias', N'Catarina', N'222554', N'9811123', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (5, N'Kiop', N'muxi', N'castle', N'122221', N'8712821', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (6, N'Juax', N'Iot', N'mix', N'12223', N'45212', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (7, N'Vic', N'gaethje', N'wox', N'22344', N'1546', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (8, N'Jux', N'Kule', N'maxio', N'23131', N'34441', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (9, N'juan', N'castro', N'rupel', N'220001', N'565842', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (10, N'Piox', N'Julan', N'Watex', N'211134', N'9484381', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (11, N'Maurivyc', N'Aldana', N'Chipana', N'224111', N'958574', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (12, N'John', N'Castillo', N'Reupo', N'758281', N'2945821', N'administrador')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (13, N'dsa', N'das', N'HSHA', N'72224229', N'222132', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (15, N'daniel', N'escribas', N'escribas', N'12345678', N'222132', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (16, N'345', N'345', N'345', N'345', N'345', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (17, N'admin2', N'admin', N'admin', N'4544444', N'123', N'administrador')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (18, N'nuevoadmin', N'nuevo', N'admin', N'280603', N'1231211', N'administrador')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (19, N'Enrique', N'Aldana ', N'Ascorbe', N'42123520', N'9479677', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (20, N'Yolanda', N'Chipana', N'Callizaya', N'20707278', N'9740088', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (21, N'Miguel', N'Chipana', N'condor', N'14526321', N'9651365', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (22, N'Jose', N'condori', N'jimenez', N'123456', N'9652553', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (23, N'yONI', N'CHIPANA', N'CALLIZAYA', N'987654', N'1220035', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (25, N'María Fernanda', N'RAMIREZ', N'TORRES', N'87654321', N'7895442', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (26, N'ROBERTO', N'GOMEZ', N'SANCHEZ', N'11223344', N'5651235', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (27, N'MAURI', N'ASDSAD', N'ASDASD', N'ASDASD', N'1242355', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (29, N'MauricioJose', N'fernando', N'chipana', N'41215134', N'9956326', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (30, N'Roberto', N'Juan', N'carlos', N'123412412', N'1245234', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (31, N'alanperro', N'jesusss', N'asdasd', N'23432454', N'123', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (32, N'alansida', N'sdafaf', N'safasdf', N'sadasda', N'1245744', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (33, N'perrodragon', N'12rsdg', N'sadfgsd', N'3456124', N'2323234', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (34, N'perrosarna', N'sdgsdg', N'asdasd', N'245234423', N'1231235', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (35, N'SARNOSO', N'423423DSF', N'ASDFASF', N'13434643', N'3454364', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (36, N'sarnita', N'asdasd', N'asdasd', N'2212441', N'2141242', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (37, N'Alonso', N'perro', N'sarnoso', N'12362', N'4156163', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (38, N'gABI', N'123T', N'135WET', N'4634561', N'12435', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (39, N'PRUEBA17', N'ASDSA', N'ASFSAF', N'327632', N'213454', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (40, N'PRUEBA18', N'43TCDD', N'SDFWERQ', N'235123', N'43668', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (41, N'PRUEBA20', N'SDFFSD', N'DG3RWER', N'3534534', N'24234', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (42, N'PRUEBA21', N'35WET', N'SFDSDG', N'5476783', N'2332525', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (43, N'prueba22', N'3rdsgf', N'qwdsasd', N'435435', N'3232213', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (44, N'PRUEBA23', N'4EFGDFG', N'SDGSDG', N'2352452', N'235234', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (45, N'PRUEBA26', N'ASFASF', N'DSFSDF', N'43456435', N'346345', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (46, N'MauricioPreuba', N'32423dsf', N'sdfasdf', N'3463', N'4353453', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (47, N'NuevoBeneficiario', N'wewefsd', N'hola', N'43264236', N'345324', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (48, N'MauricioFrio', N'ewsdffds', N'dsgsdfsd', N'2352345', N'234234', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (49, N'AYUDAAMIGO', N'COLGANDO', N'AEA', N'1243521', N'356454', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (50, N'UsuarioCUS4y5', N'cus', N'maria', N'24351233', N'9388652', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (51, N'wisneria', N'valdiviezo', N'goicochea', N'72960902', N'6526236', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (52, N'wisner', N'ia', N'ia', N'12345', N'123', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (53, N'CHIPANA', N'RATA', N'RATA', N'777', N'9810891', N'administrador')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (54, N'JALAND ', N'PERRI', N'PERRI', N'111', N'7854754', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (55, N'WISSS', N'WISSS', N'WISSS', N'987', N'123', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (56, N'danile', N'escsribas', N'alan', N'72224221', N'222132', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (57, N'sdj', N'sasa', N's', N'72224211', N'222132', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (59, N'72224211', N'72224211', N'72224211', N'72224210', N'7222421', N'personal')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (60, N'sds', N'esc', N'd', N'ds', N'7222422', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (61, N'sds', N'ds', N'ds', N'dsa', N'7222422', N'beneficiario')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono], [tipousuario]) VALUES (62, N'dans', N'das', N'esd', N'72222', N'7222422', N'beneficiario')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehiculo] ON 

INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (1, N'KIO222', N'Toyota', N'2019', N'Sedan', 998121)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (2, N'rty111', N'Nissan', N'2018', N'Sedan', 11122)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (4, N'vfg121', N'Subaru', N'2021', N'Sedan', 22239)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (5, N'IOK999', N'toyota', N'2023', N'Sedan', 1445121)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (6, N'KLO111', N'Toyota', N'2018', N'Sedan', 23341)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (8, N'WER111', N'toyota', N'2017', N'4x4', 44441212)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (9, N'RTY222', N'Nissan', N'2024', N'Sedan', 433331)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (10, N'FGH111', N'Volskvagen', N'1998', N'Cupe', 443312)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (11, N'w', N'sa', N'sa', N'sa', -2)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (12, N'ALA123', N'KIA', N'2024', N'NEWMODEL', 256321)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (13, N'MAU456', N'TOYOTE', N'2022', N'SIDAN', 5236523)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (15, N'54343', N'YARIST', N'1022', N'LITERAL', 5464752)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (16, N'EDU523', N'YARIS', N'2002', N'SDAN', 562325235)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (17, N'EDU52323', N'YARISt', N'2004', N'SDANA', 1234423)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (18, N'MAU243', N'TOYOTI', N'2003', N'SED', 6256112)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (19, N'ZORRO1', N'tsdf', N'2211', N'ASD', 1244421)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (20, N'DEF456', N'FORD', N'2019', N'PICKUP', 15353125)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (21, N'PRUEBA1', N'ASDASD', N'ASDASD', N'SAFASF', 13543543)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (22, N'PRUEBA2', N'ASDASD', N'2022', N'PERRO2', 1241513523)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (23, N'PRUEBA3', N'ALANPERRO', N'2323', N'SDAZ', 45235578)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (24, N'PRUEBA5', N'ASDAS', N'32EF', N'DSFSDF', 234567849)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (25, N'PRUEBA6', N'ydfb', N'dfgdfg', N'sdfsdf', 21423564)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (26, N'PRUEBA9', N'ADSFFA', N'ASFAS', N'DSGSDF', 46765845)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (27, N'PRUEBA10', N'32523', N'ESFSDF', N'SDFSDF', 23523556)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (28, N'PRUEBA11', N'436DGF', N'GDS', N'DSF', 45657456)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (29, N'PRUEBA12', N'ALSA', N'12451', N'SDFSDF', 21347854)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (30, N'PRUEBA13', N'TOYOTE2', N'2021', N'DSFSDF', 2612621)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (31, N'PRUEBA15', N'325WET', N'1234', N'WE5234', 2354326)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (32, N'PRUEBA16', N'AEA', N'2323', N'234SF', 23457578)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (33, N'PRUEBA18', N'436DFG', N'34634', N'DFGDCX', 756564)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (35, N'PRUEBA22', N'245SD', N'SSDSDGSG', N'H5325FD', 4654744)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (36, N'PRUEBA23', N'DSFSDF', N'SDFSDF', N'3R23D', 151322)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (37, N'prueba24', N'32452', N'sdfsdfsd', N'asdasd', 5477695)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (38, N'PRUEBA27', N'345345TGD', N'DFGDFG', N'DFGDFG', 4364356)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (39, N'MauriciopRUEBA2', N'3223ds', N'345', N'sdfg', 325745)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (40, N'NuevoCarro', N'caro1', N'askkas', N'dsfdsfsd', 325233)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (41, N'ALANFRIO', N'ASDAS', N'DSFSF2', N'32423', 235435)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (42, N'ALANMUY', N'YAFUE', N'FRIO', N'CALIENTE', 2352311)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (43, N'14FRIO', N'CALIENTE12', N'FRIO12', N'345R432', 4355764)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (44, N'Pruebacus', N'CUS', N'2013', N'nuevo', 4323281)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (45, N'121313', N'yundai', N'212012', N'juns', 15)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (47, N'A3D42', N'MOTOROLA', N'2022', N'RATA', 3242424)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (48, N'we', N'ds', N'sa', N'sa', 123)
SET IDENTITY_INSERT [dbo].[Vehiculo] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoUsua__75E3EFCFF8744AF0]    Script Date: 26/11/2024 10:42:47 ******/
ALTER TABLE [dbo].[TipoUsuario] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__D87608A7EE3472CE]    Script Date: 26/11/2024 10:42:47 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehiculo__0C057425971F3B2C]    Script Date: 26/11/2024 10:42:47 ******/
ALTER TABLE [dbo].[Vehiculo] ADD UNIQUE NONCLUSTERED 
(
	[placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
ALTER TABLE [dbo].[DocumentosReclamacion]  WITH CHECK ADD FOREIGN KEY([IdReclamacion])
REFERENCES [dbo].[reclamacion] ([id])
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
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorDni]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ActualizarIdVehiculoBeneficiario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_AsignarIDVehiculo]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_AsignarPolizaABeneficiario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_AutenticarUsuario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_IdentificarTipoUsuario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_InsertarVehiculo]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerContraseñaHasheadaUsuario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerIdPorDNI]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarAdministrador]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarBeneficiario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarDocumentoReclamacion]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarDocumentoReclamacion]
    @Nombre VARCHAR(100),
    @Extension VARCHAR(50),
    @IdReclamacion INT,
	@Url VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DocumentosReclamacion (Nombre, Extension , IdReclamacion, Url)
    VALUES (@Nombre, @Extension , @IdReclamacion, @Url);
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPersonal]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPoliza]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarReclamacion]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarReclamacion]
    @IdSiniestro INT,
    @FechaReclamacion DATE,
    @Estado VARCHAR(10),
    @Descripcion TEXT,
	@Tipo VARCHAR(20),
    @IdReclamacion INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Reclamacion (id_siniestro, fecha_reclamacion, estado, descripcion, tipo)
    VALUES (@IdSiniestro, @FechaReclamacion, @Estado, @Descripcion, @Tipo);

    SET @IdReclamacion = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarSiniestro]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spActualizarProveedor]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spActualizarTaller]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spActualizarTaller]
    @Id INT, -- ID del Taller que se va a actualizar
    @id_proveedor INT, -- Proveedor asociado al taller
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
    -- Actualizar los datos del taller, incluyendo el proveedor asociado
    UPDATE Taller
    SET 
        id_proveedor = @id_proveedor, -- Actualizar el proveedor asociado
        nombre = @nombre,
        direccion = @direccion,
        telefono = @telefono,
        correo = @correo,
        ciudad = @ciudad,
        tipo = @tipo,
        capacidad = @capacidad,
        descripcion = @descripcion,
        calificacion = @calificacion,
        estado = @estado
    WHERE id = @Id; -- Filtrar por el ID del taller
END;
GO
/****** Object:  StoredProcedure [dbo].[spAgregarProveedor]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spAgregarTaller]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spEliminarProveedor]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spEliminarTaller]    Script Date: 26/11/2024 10:42:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarTaller]
    @Id INT
AS
BEGIN
    -- Verificar si el taller está asociado a algún siniestro
    IF EXISTS (SELECT 1 FROM Siniestro WHERE id_taller = @Id)
    BEGIN
        -- Lanzar un error si el taller está asociado
        RAISERROR ('No se puede eliminar el taller porque está asociado a uno o más siniestros.', 16, 1);
        RETURN;
    END

    -- Si no está asociado, proceder con la eliminación
    DELETE FROM Taller WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[spObtenerProveedores]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spObtenerTalleres]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spObtenerTalleresConProveedor]    Script Date: 26/11/2024 10:42:47 ******/
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
/****** Object:  StoredProcedure [dbo].[spObtenerTallerPorId]    Script Date: 26/11/2024 10:42:47 ******/
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
