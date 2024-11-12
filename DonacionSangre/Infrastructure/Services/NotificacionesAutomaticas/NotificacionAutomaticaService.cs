using DonacionSangre.Domain.Dtos;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using System.Collections;

namespace DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas
{
    public class NotificacionAutomaticaService : INotificacionAutomaticaService
    {
        private readonly ICentroSaludMongoRepository _centroSaludMongoRepository;
        private readonly ISolicitudDonacionMongoRepository _solicitudDonacionMongoRepository;
        private readonly IPersonaMongoRepository _personaMongoRepository;

        public NotificacionAutomaticaService(ICentroSaludMongoRepository centroSaludMongoRepository,
            ISolicitudDonacionMongoRepository solicitudDonacionMongoRepository, IPersonaMongoRepository personaMongoRepository)
        {
            _centroSaludMongoRepository = centroSaludMongoRepository;
            _solicitudDonacionMongoRepository = solicitudDonacionMongoRepository;
            _personaMongoRepository = personaMongoRepository;
        }

        public async Task<List<Recipient>> NotificarSolicitudesDonacion()
        {
            List<Recipient> notificaciones = new();
            var personas = await _personaMongoRepository.GetPersonasAsync();
            foreach (var persona in personas)
            {
                var solicitudDonacion = await _solicitudDonacionMongoRepository.GetSolicitudDonacionByTipoSangreYMunicipio(persona.MunicipioId, persona.TipoSangre);
                if (solicitudDonacion is not null)
                {
                    notificaciones.Add(new Recipient
                    {
                        EMail = persona.Correo,
                        TemplateVars = new Hashtable{
                                { "NOMBRE_DONANTE", persona.Nombre + " " + persona.Apellido },
                                { "CIUDAD", "Popayan" },
                            }
                    });
                }
            }
            return notificaciones;
        }
    }
}
