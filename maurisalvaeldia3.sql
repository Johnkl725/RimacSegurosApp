USE [BDSeguros]
GO
/****** Object:  Table [dbo].[administrador]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[beneficiario]    Script Date: 8/11/2024 20:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[beneficiario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_vehiculo] [int] NOT NULL,
	[contraseña] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Distrito]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[documento]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[ListaBeneficiarios]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[personal]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Poliza]    Script Date: 8/11/2024 20:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Poliza](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
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
/****** Object:  Table [dbo].[presupuesto]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Proveedor]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Provincia]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[reclamacion]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Siniestro]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Taller]    Script Date: 8/11/2024 20:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taller](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[nombre] [varchar](20) NULL,
	[direccion] [varchar](30) NULL,
	[telefono] [varchar](10) NULL,
	[correo] [varchar](30) NULL,
	[ciudad] [varchar](20) NULL,
	[tipo] [varchar](25) NULL,
	[capacidad] [int] NULL,
	[descripcion] [text] NULL,
	[calificacion] [varchar](15) NULL,
	[estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPoliza]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/11/2024 20:22:43 ******/
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
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculo]    Script Date: 8/11/2024 20:22:43 ******/
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

INSERT [dbo].[administrador] ([id], [id_usuario], [contraseña]) VALUES (1, 5, N'$2a$11$sJfCjd9Aui9OrP4e3m2M8umdo1ChWFcfjDhhzsZJET/')
SET IDENTITY_INSERT [dbo].[administrador] OFF
GO
SET IDENTITY_INSERT [dbo].[beneficiario] ON 

INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (1, 1, 1, N'12345678')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (2, 8, 2, N'$2a$11$proakTcK3SiySpFQQRaTRePHhL.3A27S2Ijy7wNUvDmIrbpjf.uY2')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (9, 36, 3, N'asd')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (10, 37, 5, N'asd')
INSERT [dbo].[beneficiario] ([id], [id_usuario], [id_vehiculo], [contraseña]) VALUES (11, 38, 6, N'$2a$11$lcURpts7pu0QoOpsXfP.CuP0Xci8LCgyuc2A1ZCJPYcL65vwmmF4S')
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
SET IDENTITY_INSERT [dbo].[personal] ON 

INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (1, 4, N'$2a$11$OW7PfybkCfwJtBWHQVOM0e6FL/hfRiviy5RprrC1s2f')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (2, 6, N'$2a$11$3YguONmNbmTtsQn4td1hXeqPgIMDoOiL2Qwm9ThhU3zn4ycZ4aNUC')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (3, 7, N'$2a$11$kHwdlExWijjnvOI7lhaMf.8yRiX9jKNY2hov44BNvGuPq4yVZ6o7W')
INSERT [dbo].[personal] ([id], [id_usuario], [contraseña]) VALUES (4, 9, N'$2a$11$7JL7UHEWRW.AbKeDQRM.quMqZ0kQ8NSqcWsiUAUYsPMIQZqQsuzd6')
SET IDENTITY_INSERT [dbo].[personal] OFF
GO
SET IDENTITY_INSERT [dbo].[Poliza] ON 

INSERT [dbo].[Poliza] ([id], [id_usuario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (1, 1, 1, CAST(N'2024-01-01' AS Date), CAST(N'2025-01-01' AS Date), N'Activa')
INSERT [dbo].[Poliza] ([id], [id_usuario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (2, 2, 2, CAST(N'2024-02-01' AS Date), CAST(N'2025-02-01' AS Date), N'Activa')
INSERT [dbo].[Poliza] ([id], [id_usuario], [id_tipo], [fecha_inicio], [fecha_fin], [estado]) VALUES (3, 3, 3, CAST(N'2024-03-01' AS Date), CAST(N'2025-03-01' AS Date), N'Vencida')
SET IDENTITY_INSERT [dbo].[Poliza] OFF
GO
SET IDENTITY_INSERT [dbo].[presupuesto] ON 

INSERT [dbo].[presupuesto] ([id], [fecha_emision], [monto_total], [detalles], [impuestos], [costo_sin_impuestos], [estado], [descuento], [enlace]) VALUES (1, CAST(N'2024-11-05' AS Date), 0.0000, N'Detalles de prueba', 0.0000, 0.0000, N'Activo', 0.0000, N'enlace_prueba')
SET IDENTITY_INSERT [dbo].[presupuesto] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedor] ON 

INSERT [dbo].[Proveedor] ([id], [nombre], [ruc], [telefono], [correo], [calificacion], [descripcion]) VALUES (1, N'Proveedor Ejemplo', N'12345678901', N'123456789', N'proveedor@ejemplo.com', N'5', N'Proveedor de prueba')
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
SET IDENTITY_INSERT [dbo].[Siniestro] ON 

INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (1, 15, 1, 1, 1, 1, NULL, NULL, N'Accidente', CAST(N'2024-01-15' AS Date), CAST(N'2024-01-16' AS Date), CAST(N'2024-01-17' AS Date), N'Av. Siempre Viva 742', N'Accidente en la avenida principal')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (2, 15, 2, 4, 2, 2, NULL, NULL, N'Incendio', CAST(N'2024-02-20' AS Date), CAST(N'2024-02-21' AS Date), CAST(N'2024-02-22' AS Date), N'Calle Falsa 123', N'Incendio en el área comercial')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (3, 4, 7, 6, 3, 3, NULL, NULL, N'Robo', CAST(N'2024-03-10' AS Date), CAST(N'2024-03-11' AS Date), CAST(N'2024-03-12' AS Date), N'Plaza Mayor', N'Robo en un almacén')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (18, 15, 1, 1, 1, 1, NULL, NULL, N'jsjs', CAST(N'2002-12-23' AS Date), CAST(N'0001-01-01' AS Date), CAST(N'0001-01-01' AS Date), N'ssa', N'snsa')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (19, 1, 1, 1, NULL, NULL, NULL, NULL, N'Choque', CAST(N'2001-12-23' AS Date), CAST(N'2024-11-04' AS Date), CAST(N'2024-11-04' AS Date), N'dskaksd', N'djdakdjadk')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (20, 1, 1, 1, NULL, NULL, NULL, NULL, N'Daños Menores', CAST(N'2001-02-11' AS Date), CAST(N'2024-11-04' AS Date), CAST(N'2024-11-04' AS Date), N'sjsdksa', N'djkadjad')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (21, 1, 1, 1, NULL, NULL, NULL, NULL, N'Robo', CAST(N'2000-11-11' AS Date), CAST(N'2024-11-04' AS Date), CAST(N'2024-11-04' AS Date), N'sjjdsak11', N'jjsajdka11')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (22, 1, 1, 1, NULL, NULL, NULL, NULL, N'Robo', CAST(N'2001-12-23' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'jkasdka', N'jhdad')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (23, 1, 1, 2, NULL, NULL, NULL, NULL, N'Daños Menores', CAST(N'2001-12-11' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'dhjas', N'pruebagaa')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (24, 2, 2, 2, NULL, NULL, NULL, NULL, N'Robo', CAST(N'2001-01-01' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'ijakjs', N'iadjkjkas')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (25, 2, 2, 2, NULL, NULL, NULL, NULL, N'Robo', CAST(N'2001-01-01' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'ijakjs', N'iadjkjkas')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (31, 1, 1, 1, 1, 1, 1, 1, N'Robo', CAST(N'2001-01-11' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'1', N'hss')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (32, 1, 1, 1, 1, 1, 1, 1, N'Robo', CAST(N'1980-11-12' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'kwkw', N'jjw')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (33, 1, 1, 1, 1, 1, 1, 1, N'Robo', CAST(N'2001-12-01' AS Date), CAST(N'2024-11-05' AS Date), CAST(N'2024-11-05' AS Date), N'jsjasj', N'jsjas')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (34, 15, 3, 6, 1, 1, 1, 1, N'Choque', CAST(N'2024-11-06' AS Date), CAST(N'2024-11-06' AS Date), CAST(N'2024-11-06' AS Date), N'Jiron las palmas 123', N'Un hombre a mucha velocidad, chocó mi vehículo para luego irse a la fuga')
INSERT [dbo].[Siniestro] ([id_siniestro], [id_departamento], [id_provincia], [id_distrito], [id_documento], [id_poliza], [id_taller], [id_presupuesto], [tipo], [fecha_siniestro], [fecha_creacion], [fecha_actualizacion], [ubicacion], [descripcion]) VALUES (35, 15, 2, 4, 1, 1, 1, 1, N'Daños Menores', CAST(N'2024-11-08' AS Date), CAST(N'2024-11-08' AS Date), CAST(N'2024-11-08' AS Date), N'Jiron las palmas 123', N'Un niño flotó sobre mí y voló un auto con su rasho laser')
SET IDENTITY_INSERT [dbo].[Siniestro] OFF
GO
SET IDENTITY_INSERT [dbo].[Taller] ON 

INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (1, 1, N'Taller Ejemplo', N'Dirección Ejemplo', N'123456789', N'taller@ejemplo.com', N'Ciudad Ejemplo', N'Tipo Ejemplo', 100, N'Descripción del taller', N'5', N'Activo')
INSERT [dbo].[Taller] ([id], [id_proveedor], [nombre], [direccion], [telefono], [correo], [ciudad], [tipo], [capacidad], [descripcion], [calificacion], [estado]) VALUES (4, 1, N'Taller de Prueba', N'Av. Ejemplo 123', N'123456789', N'taller@ejemplo.com', N'Ciudad Ejemplo', N'General', 100, N'Descripción de prueba', N'5', N'Activo')
SET IDENTITY_INSERT [dbo].[Taller] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoPoliza] ON 

INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (1, N'Esencial', 100.0000)
INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (2, N'Intermedia', 200.0000)
INSERT [dbo].[TipoPoliza] ([id], [descripcion], [monto_mensual]) VALUES (3, N'Premium', 300.0000)
SET IDENTITY_INSERT [dbo].[TipoPoliza] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (1, N'daniel', N'escribas', N'sjs', N'72224229', N'23423')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (2, N'Maria', N'Lopez', N'Diaz', N'0987654321', N'876543210')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (3, N'Carlos', N'Sanchez', N'Rojas', N'5678901234', N'765432109')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (4, N'Jose', N'Rivera', N'Rivas', N'8585759', N'9443321')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (5, N'martin', N'castro', N'castro', N'865747', N'9991231')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (6, N'lucas', N'vasquez', N'sigo', N'485748', N'9948371')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (7, N'Lucas', N'martel', N'wuax', N'568549', N'134542')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (8, N'dasda', N'dasd', N'dsadsa', N'43848', N'1313')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (9, N'Lucas', N'Martel', N'Second', N'94844831', N'9998371')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (10, N'John Javier', N'Castillo', N'Reupo', N'74916039', N'447531')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (11, N'Lucas', N'Martel', N'Javier', N'3447634', N'12331')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (12, N'Marilu Samira', N'Martel', N'Javier', N'344111', N'948348')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (13, N'Javier', N'Martel', N'Huarcaya', N'222143', N'567742')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (15, N'Javier', N'Martel', N'Huarcaya', N'2221436', N'567742')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (16, N'Marilu Samira', N'Reupo', N'Yrat', N'133111', N'333342')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (17, N'Cichar', N'Reupo', N'Yrat', N'11112231', N'342221')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (19, N'Cichar', N'Martel', N'Yrat', N'093412', N'111134')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (21, N'wats', N'marq', N'werx', N'8511111', N'9123112')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (22, N'Jorge', N'Maki', N'Tuyi', N'0831221', N'9712213')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (23, N'Retyu', N'Jox', N'Max', N'0012211', N'222222')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (24, N'Lucas', N'qw', N'ert', N'21100021', N'122131')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (25, N'zxccv', N'czxcz', N'dsda', N'0098888', N'1322132')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (27, N'Jorx', N'asw', N'ert', N'1111102', N'1209999')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (28, N'work', N'arok', N'ark', N'100002331', N'222318')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (29, N'erq', N'rty', N'asd', N'12224411', N'456443')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (30, N'dasd', N'dqwe', N'das', N'22221des', N'22311')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (31, N'df', N'adsad', N'dasda', N'122213', N'45453')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (33, N'Victoria', N'Reupo', N'Benavides', N'7776331', N'0546738')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (35, N'eqweq', N'dfsa', N'vcxb', N'11113333', N'5647381')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (36, N'cffgf', N'dsad', N'eqwq', N'123121', N'56766')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (37, N'lux', N'marti', N'rtyu', N'230311', N'844741')
INSERT [dbo].[Usuario] ([id], [nombres], [apellido1], [apellido2], [dni], [telefono]) VALUES (38, N'John Luis Alberto', N'Castillo', N'Reupo', N'111111222', N'9740528')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehiculo] ON 

INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (1, N'2398443', N'hjjjdkas', N'njadha', N'jjdajjds', 1111)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (2, N'ARTU23', N'NISAN', N'WASO', N'CHATO', 9857434)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (3, N'ERT111', N'Nissan', N'porshe', N'4x4', 111222312)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (4, N'tyu567', N'porshe', N'nissan', N'wax', 113141312)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (5, N'dfg111', N'masda', N'guarti', N'wuix', 958574123)
INSERT [dbo].[Vehiculo] ([id], [placa], [marca], [modelo], [tipo], [tarjeta_vehiculo]) VALUES (6, N'AKG123', N'Subaru', N'Porshe', N'4x4', 1222431)
SET IDENTITY_INSERT [dbo].[Vehiculo] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TipoUsua__75E3EFCF327AC552]    Script Date: 8/11/2024 20:22:43 ******/
ALTER TABLE [dbo].[TipoUsuario] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__D87608A75075524E]    Script Date: 8/11/2024 20:22:43 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehiculo__0C0574253AF9B447]    Script Date: 8/11/2024 20:22:43 ******/
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
ALTER TABLE [dbo].[Poliza]  WITH CHECK ADD  CONSTRAINT [fk_id_tipo_poliza] FOREIGN KEY([id_tipo])
REFERENCES [dbo].[TipoPoliza] ([id])
GO
ALTER TABLE [dbo].[Poliza] CHECK CONSTRAINT [fk_id_tipo_poliza]
GO
ALTER TABLE [dbo].[Poliza]  WITH CHECK ADD  CONSTRAINT [fk_id_usuario_poliza] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Poliza] CHECK CONSTRAINT [fk_id_usuario_poliza]
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
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorDni]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ActualizarIdVehiculoBeneficiario]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_AutenticarUsuario]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_IdentificarTipoUsuario]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_InsertarVehiculo]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerContraseñaHasheadaUsuario]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ObtenerIdPorDNI]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarAdministrador]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarBeneficiario]    Script Date: 8/11/2024 20:22:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RegistrarBeneficiario]
    @id_usuario INT,
    @IdVehiculo VARCHAR(10),
    @Password VARCHAR(60)
AS
BEGIN
    INSERT INTO Beneficiario (id_usuario, id_vehiculo, contraseña)
    VALUES (@id_usuario, @IdVehiculo, @Password);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarPersonal]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarSiniestro]    Script Date: 8/11/2024 20:22:43 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_RegistrarUsuario]    Script Date: 8/11/2024 20:22:43 ******/
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
    @IdUsuario INT OUTPUT
AS
BEGIN
    INSERT INTO Usuario (nombres, apellido1, apellido2, dni, telefono)
    VALUES (@Nombres, @Apellido1, @Apellido2, @DNI, @Telefono);

    SET @IdUsuario = SCOPE_IDENTITY();
END
GO
