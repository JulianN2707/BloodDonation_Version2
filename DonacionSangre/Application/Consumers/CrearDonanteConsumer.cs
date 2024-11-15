using System;
using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using MassTransit;
using MassTransitMessages.Messages;
using ETipoSangre = DonacionSangre.Domain.ValueObjects.TipoSangre;

namespace DonacionSangre.Application.Consumers;

public class CrearDonanteConsumer : IConsumer<EnviarCreacionDonanteMessage>
{
    private IRepository<UsuarioDonacion> _usuarioDonacionRepository;

    public CrearDonanteConsumer(IRepository<UsuarioDonacion> usuarioDonacionRepository)
    {
        _usuarioDonacionRepository = usuarioDonacionRepository;
    }

    public async Task Consume(ConsumeContext<EnviarCreacionDonanteMessage> context)
    {
        var data = context.Message;
        if (data is not null)
        {
            var usuario = new UsuarioDonacion
            {
                UsuarioId = data.UsuarioId,
                PersonaId = data.PersonaId,
                MunicipioId = data.MunicipioDireccionId,
                TipoSangre = ETipoSangre.Crear(data.GrupoSanguineo,data.FactorRh),
                CorreoElectronico = data.CorreoElectronicoPersona,
                Cargo = data.Cargo,
                Direccion = data.PersonaDireccion,
                Celular = data.PersonaCelular,
                PrimerNombre = data.PersonaPrimerNombre,
                PrimerApellido = data.PersonaPrimerApellido,
            };
            await _usuarioDonacionRepository.AddAsync(usuario);
            await _usuarioDonacionRepository.SaveChangesAsync();

            DonanteCreadoEvent evento = new DonanteCreadoEvent
            {
                CorrelationId = data.CorrelationId,
                DonanteId = NewId.NextSequentialGuid(),
                SolicitudUsuarioId = data.SolicitudUsuarioId,

            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:saga-aprobar-donante"));
            await endpoint.Send(evento);

        }

    }
}
