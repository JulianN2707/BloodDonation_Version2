USE [SolicitudesUsuarios]
GO
/****** Object:  Schema [SolicitudUsuario]    Script Date: 15/11/2024 1:17:40 a. m. ******/
CREATE SCHEMA [SolicitudUsuario]
GO
/****** Object:  Table [SolicitudUsuario].[EstadoSolicitudUsuario]    Script Date: 15/11/2024 1:17:40 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SolicitudUsuario].[EstadoSolicitudUsuario](
	[EstadoSolicitudUsuarioId] [uniqueidentifier] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EstadoSolicitudUsuario] PRIMARY KEY CLUSTERED 
(
	[EstadoSolicitudUsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [SolicitudUsuario].[SolicitudUsuario]    Script Date: 15/11/2024 1:17:41 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SolicitudUsuario].[SolicitudUsuario](
	[SolicitudUsuarioId] [uniqueidentifier] NOT NULL,
	[PersonaMunicipioDireccionId] [uniqueidentifier] NULL,
	[TipoPersonaId] [uniqueidentifier] NOT NULL,
	[EstadoSolicitudUsuarioId] [uniqueidentifier] NOT NULL,
	[TipoSangre] [nvarchar](max) NOT NULL,
	[PersonaNumeroDocumento] [nvarchar](max) NULL,
	[PersonaFechaExpedicionDocumento] [datetime2](7) NULL,
	[PersonaPrimerApellido] [nvarchar](max) NULL,
	[PersonaPrimerNombre] [nvarchar](max) NULL,
	[PersonaSegundoApellido] [nvarchar](max) NULL,
	[PersonaSegundoNombre] [nvarchar](max) NULL,
	[PersonaCorreoElectronico] [nvarchar](max) NULL,
	[PersonaCelular] [nvarchar](max) NULL,
	[PersonaDireccion] [nvarchar](max) NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[FechaAprobacion] [datetime2](7) NULL,
	[MotivoRechazo] [nvarchar](max) NULL,
	[FechaRechazo] [datetime2](7) NULL,
 CONSTRAINT [PK_SolicitudUsuario] PRIMARY KEY CLUSTERED 
(
	[SolicitudUsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [SolicitudUsuario].[EstadoSolicitudUsuario] ([EstadoSolicitudUsuarioId], [Descripcion]) VALUES (N'b05a7f4a-4d21-4c4d-bf76-19c51f1f25c7', N'Rechazado')
INSERT [SolicitudUsuario].[EstadoSolicitudUsuario] ([EstadoSolicitudUsuarioId], [Descripcion]) VALUES (N'7c3e4d1f-6d59-431b-89f1-2e7f5bde9a8d', N'Pendiente')
INSERT [SolicitudUsuario].[EstadoSolicitudUsuario] ([EstadoSolicitudUsuarioId], [Descripcion]) VALUES (N'2d4b6a22-083d-4e9c-a3ae-8d1c0dbd51bc', N'Aprobado')
GO
ALTER TABLE [SolicitudUsuario].[SolicitudUsuario]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudUsuario_EstadoSolicitudUsuario_EstadoSolicitudUsuarioId] FOREIGN KEY([EstadoSolicitudUsuarioId])
REFERENCES [SolicitudUsuario].[EstadoSolicitudUsuario] ([EstadoSolicitudUsuarioId])
ON DELETE CASCADE
GO
ALTER TABLE [SolicitudUsuario].[SolicitudUsuario] CHECK CONSTRAINT [FK_SolicitudUsuario_EstadoSolicitudUsuario_EstadoSolicitudUsuarioId]
GO
