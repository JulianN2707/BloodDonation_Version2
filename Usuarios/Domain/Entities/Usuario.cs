using System;

namespace Usuarios.Domain.Entities;

public class Usuario
{
    public Guid UsuarioId { get; set; }

    public Guid PersonaId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int EstadoUsuarioId { get; set; }

}
