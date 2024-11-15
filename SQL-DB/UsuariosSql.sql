USE [Usuarios]
GO
/****** Object:  Schema [Usuario]    Script Date: 15/11/2024 1:16:09 a. m. ******/
CREATE SCHEMA [Usuario]
GO
/****** Object:  Table [Usuario].[Usuario]    Script Date: 15/11/2024 1:16:09 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Usuario].[Usuario](
	[UsuarioId] [uniqueidentifier] NOT NULL,
	[PersonaId] [uniqueidentifier] NOT NULL,
	[FechaRegistro] [datetime2](7) NOT NULL,
	[EstaActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
