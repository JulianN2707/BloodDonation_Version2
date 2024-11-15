USE [Personas]
GO
/****** Object:  Schema [Persona]    Script Date: 15/11/2024 1:15:06 a. m. ******/
CREATE SCHEMA [Persona]
GO
/****** Object:  Table [Persona].[Persona]    Script Date: 15/11/2024 1:15:06 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Persona].[Persona](
	[PersonaId] [uniqueidentifier] NOT NULL,
	[MunicipioDireccionId] [uniqueidentifier] NULL,
	[NumeroDocumento] [nvarchar](max) NOT NULL,
	[FechaExpedicionDocumento] [datetime2](7) NOT NULL,
	[PrimerApellido] [nvarchar](max) NULL,
	[PrimerNombre] [nvarchar](max) NULL,
	[SegundoApellido] [nvarchar](max) NULL,
	[SegundoNombre] [nvarchar](max) NULL,
	[CorreoElectronico] [nvarchar](max) NULL,
	[Celular] [nvarchar](max) NULL,
	[Direccion] [nvarchar](max) NULL,
	[TipoPersonaId] [uniqueidentifier] NOT NULL,
	[TipoSangre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[PersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Persona].[TipoPersona]    Script Date: 15/11/2024 1:15:06 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Persona].[TipoPersona](
	[TipoPersonaId] [uniqueidentifier] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TipoPersona] PRIMARY KEY CLUSTERED 
(
	[TipoPersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [Persona].[TipoPersona] ([TipoPersonaId], [Descripcion]) VALUES (N'9ff25fe2-0a6e-4d3c-a346-cfdeea651c93', N'Enfermero')
INSERT [Persona].[TipoPersona] ([TipoPersonaId], [Descripcion]) VALUES (N'c1c11fc4-0fc2-40a0-b496-fc7c465a5684', N'Usuario')
GO
ALTER TABLE [Persona].[Persona]  WITH CHECK ADD  CONSTRAINT [FK_Persona_TipoPersona_TipoPersonaId] FOREIGN KEY([TipoPersonaId])
REFERENCES [Persona].[TipoPersona] ([TipoPersonaId])
ON DELETE CASCADE
GO
ALTER TABLE [Persona].[Persona] CHECK CONSTRAINT [FK_Persona_TipoPersona_TipoPersonaId]
GO
