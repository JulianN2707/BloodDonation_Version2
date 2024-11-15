using System;
using MassTransit;
using MassTransitMessages.Messages;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Repositories.SpecificationUnitOfWork;

namespace Usuarios.Application.Consumers;

public class CrearUsuarioConsumer : IConsumer<CrearUsuarioMessage>
{
    private readonly IUsuariosSpecificationUnitOfWork _usuariosSpecificationUnitOfWork;

    public CrearUsuarioConsumer(IUsuariosSpecificationUnitOfWork usuariosSpecificationUnitOfWork)
    {
        _usuariosSpecificationUnitOfWork = usuariosSpecificationUnitOfWork;
    }

    public async Task Consume(ConsumeContext<CrearUsuarioMessage> context)
    {
        var data = context.Message;
        if (data is not null){
            var usuario = new Usuario{
                PersonaId = data.PersonaId,
                FechaRegistro = DateTime.UtcNow,
                EstaActivo = true
            };
            await _usuariosSpecificationUnitOfWork._usuarioRepository.AddAsync(usuario);
            await _usuariosSpecificationUnitOfWork._usuarioRepository.SaveChangesAsync();
            UsuarioCreadoEvent message = new UsuarioCreadoEvent{
                CorrelationId = data.CorrelationId,
                SolicitudUsuarioId = data.SolicitudUsuarioId,
                UsuarioId = usuario.UsuarioId
            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:saga-aprobar-donante"));         
            await endpoint.Send(message);

        }
    }
}
