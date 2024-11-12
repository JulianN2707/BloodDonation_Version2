SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Persona](
    [PersonaId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [NumeroDocumento] NVARCHAR(MAX) NOT NULL,
    [FechaExpedicionDocumento] DATETIME2(7) NOT NULL,
    [PrimerApellido] NVARCHAR(255) NULL,
    [PrimerNombre] NVARCHAR(255) NULL,
    [SegundoApellido] NVARCHAR(255) NULL,
    [SegundoNombre] NVARCHAR(255) NULL,
    [CorreoElectronico] NVARCHAR(255) NULL,
    [Celular] NVARCHAR(40) NULL,
    [Direccion] NVARCHAR(255) NULL,
    [MunicipioDireccionId] INT NULL
)
GO
