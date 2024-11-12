SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SolicitudUsuario](
	[SolicitudUsuarioId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[PersonaNumeroDocumento] [nvarchar](24) NULL,
	[PersonaFechaExpedicionDocumento] [datetime2](7) NULL,
	[PersonaPrimerApellido] [nvarchar](255) NULL,
	[PersonaPrimerNombre] [nvarchar](255) NULL,
	[PersonaSegundoApellido] [nvarchar](255) NULL,
	[PersonaSegundoNombre] [nvarchar](255) NULL,
	[PersonaCorreoElectronico] [nvarchar](80) NULL,
	[PersonaMunicipioDireccionId] [int] NULL,
	[PersonaCelular] [nvarchar](40) NULL,
    [PersonaDireccion] [nvarchar](255) NULL,
	[EstadoSolicitudUsuarioId] [int] NOT NULL,
    [TipoCargoPersonaId] [int] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[FechaAprobacion] [datetime2](7) NULL,
	[MotivoRechazo] [nvarchar](255) NULL,
	[FechaRechazo] [datetime2](7) NULL,
)
GO
