USE [Archivos]
GO
/****** Object:  Schema [Archivo]    Script Date: 15/11/2024 1:11:59 a. m. ******/
CREATE SCHEMA [Archivo]
GO
/****** Object:  Table [Archivo].[Archivo]    Script Date: 15/11/2024 1:11:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Archivo].[Archivo](
	[ArchivoId] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Ruta] [nvarchar](max) NOT NULL,
	[TipoArchivoId] [uniqueidentifier] NOT NULL,
	[Extension] [nvarchar](max) NULL,
	[FechaCreacion] [datetime2](7) NULL,
 CONSTRAINT [PK_Archivo] PRIMARY KEY CLUSTERED 
(
	[ArchivoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Archivo].[TipoArchivo]    Script Date: 15/11/2024 1:11:59 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Archivo].[TipoArchivo](
	[TipoArchivoId] [uniqueidentifier] NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TipoArchivo] PRIMARY KEY CLUSTERED 
(
	[TipoArchivoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [Archivo].[TipoArchivo] ([TipoArchivoId], [Descripcion]) VALUES (N'aa382a00-c492-4259-8833-36a0442fb23a', N'PDF')
INSERT [Archivo].[TipoArchivo] ([TipoArchivoId], [Descripcion]) VALUES (N'431b8180-1e0b-4bf6-9630-7e258454d715', N'JPG')
INSERT [Archivo].[TipoArchivo] ([TipoArchivoId], [Descripcion]) VALUES (N'76d0b7b0-a8a7-4a51-9d6f-9d4f179f25f9', N'TXT')
GO
ALTER TABLE [Archivo].[Archivo]  WITH CHECK ADD  CONSTRAINT [FK_Archivo_TipoArchivo_TipoArchivoId] FOREIGN KEY([TipoArchivoId])
REFERENCES [Archivo].[TipoArchivo] ([TipoArchivoId])
ON DELETE CASCADE
GO
ALTER TABLE [Archivo].[Archivo] CHECK CONSTRAINT [FK_Archivo_TipoArchivo_TipoArchivoId]
GO
