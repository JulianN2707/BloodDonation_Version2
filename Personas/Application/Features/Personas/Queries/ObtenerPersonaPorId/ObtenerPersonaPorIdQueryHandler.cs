using MediatR;
using Personas.Domain.Dto;
using Personas.Domain.Entities;
using Personas.Infrastructure.Repositories.PersonaSpecification;

namespace Personas.Application.Features.Personas.Queries.ObtenerPersonaPorId
{
    public class ObtenerPersonaPorIdQueryHandler : IRequestHandler<ObtenerPersonaPorIdQuery, ObtenerPersonaPorIdResponse>
    {

        private readonly IRepository<Persona> _personaRepository;

        public ObtenerPersonaPorIdQueryHandler(IRepository<Persona> personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<ObtenerPersonaPorIdResponse> Handle(ObtenerPersonaPorIdQuery request, CancellationToken cancellationToken)
        {
            var persona = await _personaRepository.GetByIdAsync(request.PersonaId) ?? throw new Exception("Error, no existe una persona");
            return new ObtenerPersonaPorIdResponse
            {
                NumeroDocumento = persona.NumeroDocumento,
                Celular = persona.Celular,
                CorreoElectronico = persona.CorreoElectronico,
                Direccion = persona.Direccion,
                FechaExpedicionDocumento = persona.FechaExpedicionDocumento,
                MunicipioDireccionId = persona.MunicipioDireccionId,
                PersonaId = persona.PersonaId,
                PrimerApellido = persona.PrimerApellido,
                PrimerNombre = persona.PrimerNombre,
                SegundoApellido = persona.SegundoApellido,
                SegundoNombre = persona.SegundoNombre
            };
            
        }
    }
}
