USE [DonacionSangre]
GO
/****** Object:  Schema [Donacion]    Script Date: 15/11/2024 1:14:01 a. m. ******/
CREATE SCHEMA [Donacion]
GO
/****** Object:  Table [Donacion].[CentroSalud]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[CentroSalud](
	[CentroSaludId] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[MunicipioId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CentroSalud] PRIMARY KEY CLUSTERED 
(
	[CentroSaludId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Donacion].[Departamento]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[Departamento](
	[DepartamentoId] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[DepartamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Donacion].[Municipio]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[Municipio](
	[MunicipioId] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[DepartamentoId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Municipio] PRIMARY KEY CLUSTERED 
(
	[MunicipioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Donacion].[ReservaDonacion]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[ReservaDonacion](
	[ReservaDonacionId] [uniqueidentifier] NOT NULL,
	[FechaReserva] [datetime2](7) NOT NULL,
	[PersonaId] [uniqueidentifier] NOT NULL,
	[EstadoReserva] [nvarchar](max) NOT NULL,
	[SolicitudDonacionId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ReservaDonacion] PRIMARY KEY CLUSTERED 
(
	[ReservaDonacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Donacion].[SolicitudDonacion]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[SolicitudDonacion](
	[SolicitudDonacionId] [uniqueidentifier] NOT NULL,
	[FechaSolicitud] [datetime2](7) NOT NULL,
	[CentroSaludId] [uniqueidentifier] NOT NULL,
	[TipoSangre] [nvarchar](max) NOT NULL,
	[Estado] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SolicitudDonacion] PRIMARY KEY CLUSTERED 
(
	[SolicitudDonacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Donacion].[UsuarioDonacion]    Script Date: 15/11/2024 1:14:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Donacion].[UsuarioDonacion](
	[UsuarioDonacionId] [uniqueidentifier] NOT NULL,
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[PersonaId] [uniqueidentifier] NOT NULL,
	[MunicipioId] [uniqueidentifier] NOT NULL,
	[TipoSangre] [nvarchar](max) NOT NULL,
	[PrimerApellido] [nvarchar](max) NOT NULL,
	[PrimerNombre] [nvarchar](max) NOT NULL,
	[CorreoElectronico] [nvarchar](max) NOT NULL,
	[Cargo] [nvarchar](max) NOT NULL,
	[Direccion] [nvarchar](max) NULL,
	[Celular] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UsuarioDonacion] PRIMARY KEY CLUSTERED 
(
	[UsuarioDonacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [Donacion].[CentroSalud] ([CentroSaludId], [Nombre], [Direccion], [MunicipioId]) VALUES (N'aa382a00-c492-4259-8833-36a0442fb23a', N'Nueva EPS', N'Calle 5 ?12-55', N'90764425-b8bb-4316-ae66-a8b64068cb77')
GO
INSERT [Donacion].[Departamento] ([DepartamentoId], [Nombre]) VALUES (N'02aefaff-403a-4d23-a866-05f44f6eb527', N'CAUCA')
INSERT [Donacion].[Departamento] ([DepartamentoId], [Nombre]) VALUES (N'2e20fb95-f78f-4114-90a9-a3ae962d6ef8', N'ANTIOQUIA')
INSERT [Donacion].[Departamento] ([DepartamentoId], [Nombre]) VALUES (N'6430a051-7c06-4cb5-a957-f60ebb447da2', N'BOGOTA D. C.')
GO
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'2d471499-44cf-4d46-ac72-18f056613961', N'FLORENCIA', N'02aefaff-403a-4d23-a866-05f44f6eb527')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'1d2d05f2-b938-43b0-b225-4e1f61593389', N'BELLO', N'2e20fb95-f78f-4114-90a9-a3ae962d6ef8')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'3d7d910d-0a90-43fc-b676-9edf47f0798d', N'LA VEGA', N'02aefaff-403a-4d23-a866-05f44f6eb527')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'90764425-b8bb-4316-ae66-a8b64068cb77', N'POPAYAN', N'02aefaff-403a-4d23-a866-05f44f6eb527')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'4bc0623c-1c42-46b4-9de6-d1eea2c2d30c', N'BOGOTA', N'6430a051-7c06-4cb5-a957-f60ebb447da2')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'92d64c35-c888-42be-9e5e-d64d1fe35a30', N'ANDES', N'2e20fb95-f78f-4114-90a9-a3ae962d6ef8')
INSERT [Donacion].[Municipio] ([MunicipioId], [Nombre], [DepartamentoId]) VALUES (N'56c1bee5-9eaf-47ae-939f-f471a005fe74', N'MEDELLIN', N'2e20fb95-f78f-4114-90a9-a3ae962d6ef8')
GO
ALTER TABLE [Donacion].[CentroSalud]  WITH CHECK ADD  CONSTRAINT [FK_CentroSalud_Municipio_MunicipioId] FOREIGN KEY([MunicipioId])
REFERENCES [Donacion].[Municipio] ([MunicipioId])
ON DELETE CASCADE
GO
ALTER TABLE [Donacion].[CentroSalud] CHECK CONSTRAINT [FK_CentroSalud_Municipio_MunicipioId]
GO
ALTER TABLE [Donacion].[Municipio]  WITH CHECK ADD  CONSTRAINT [FK_Municipio_Departamento_DepartamentoId] FOREIGN KEY([DepartamentoId])
REFERENCES [Donacion].[Departamento] ([DepartamentoId])
ON DELETE CASCADE
GO
ALTER TABLE [Donacion].[Municipio] CHECK CONSTRAINT [FK_Municipio_Departamento_DepartamentoId]
GO
ALTER TABLE [Donacion].[ReservaDonacion]  WITH CHECK ADD  CONSTRAINT [FK_ReservaDonacion_SolicitudDonacion_SolicitudDonacionId] FOREIGN KEY([SolicitudDonacionId])
REFERENCES [Donacion].[SolicitudDonacion] ([SolicitudDonacionId])
GO
ALTER TABLE [Donacion].[ReservaDonacion] CHECK CONSTRAINT [FK_ReservaDonacion_SolicitudDonacion_SolicitudDonacionId]
GO
ALTER TABLE [Donacion].[SolicitudDonacion]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudDonacion_CentroSalud_CentroSaludId] FOREIGN KEY([CentroSaludId])
REFERENCES [Donacion].[CentroSalud] ([CentroSaludId])
ON DELETE CASCADE
GO
ALTER TABLE [Donacion].[SolicitudDonacion] CHECK CONSTRAINT [FK_SolicitudDonacion_CentroSalud_CentroSaludId]
GO
