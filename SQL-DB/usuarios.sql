CREATE TABLE Usuario
(
    UsuarioId UNIQUEIDENTIFIER PRIMARY KEY,
    PersonaId INT NOT NULL,
    FechaRegistro DATETIME NOT NULL,
    EstadoUsuarioId INT NOT NULL
);
