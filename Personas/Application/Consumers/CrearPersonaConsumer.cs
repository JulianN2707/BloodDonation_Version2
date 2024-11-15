using System;
using MassTransit;
using MassTransitMessages.Messages;
using Personas.Domain.Entities;
using Personas.Domain.ValueObjects;
using Personas.Infrastructure.Repositories.SpecificationUnitOfWork;
using ETipoSangrePersona= Personas.Domain.ValueObjects.TipoSangre;

namespace Personas.Application.Consumers;

public class CrearPersonaConsumer : IConsumer<CrearPersonaMessage>
{
    private readonly IPersonaSpecificationUnitOfWork _personaSpecificationUnitOfWork;

    public CrearPersonaConsumer(IPersonaSpecificationUnitOfWork personaSpecificationUnitOfWork)
    {
        _personaSpecificationUnitOfWork = personaSpecificationUnitOfWork;
    }

    public async Task Consume(ConsumeContext<CrearPersonaMessage> context)
    {
        var data = context.Message;
        if (data is not null){
            var persona = new Persona{
                NumeroDocumento=data.NumeroDocumento,
                FechaExpedicionDocumento=data.FechaExpedicionDocumento,
                PrimerApellido = data.PrimerApellido,
                SegundoApellido = data.SegundoApellido,
                PrimerNombre = data.PrimerNombre,
                SegundoNombre = data.SegundoNombre,
                Celular = data.Celular,
                CorreoElectronico = data.CorreoElectronico,
                Direccion = data.Direccion,
                MunicipioDireccionId = data.MunicipioDireccionId,
                TipoPersonaId = data.TipoPersonaId,
                TipoSangre = ETipoSangrePersona.Crear(data.GrupoSanguineo,data.FactorRh),

            };
            await _personaSpecificationUnitOfWork._personaRepository.AddAsync(persona);
            await _personaSpecificationUnitOfWork._personaRepository.SaveChangesAsync();

            PersonaCreadaEvent evento = new PersonaCreadaEvent(){
                CorrelationId = data.CorrelationId,
                CorreoElectronico = persona.CorreoElectronico,
                Direccion = persona.Direccion,
                NumeroCelular = persona.Celular,
                PrimerApellido = persona.PrimerApellido,
                PrimerNombre = persona.PrimerNombre,
                PersonaId = persona.PersonaId,
                SolicitudUsuarioId = data.SolicitudUsuarioId,
                MunicipioDireccionId = (Guid)persona.MunicipioDireccionId,
                GrupoSanguineo = data.GrupoSanguineo,
                FactorRh = data.FactorRh,
                Cargo = await ObtenerCarguoTipoPersona(data.TipoPersonaId),
            };
            var endpoint = await context.GetSendEndpoint(new Uri($"queue:saga-aprobar-donante"));         
            await endpoint.Send(evento);
        }
    }
    public async Task<string> ObtenerCarguoTipoPersona(Guid id){
        return (await _personaSpecificationUnitOfWork._tipoPersonaRepository.GetByIdAsync(id))!.Descripcion;

    }
}
