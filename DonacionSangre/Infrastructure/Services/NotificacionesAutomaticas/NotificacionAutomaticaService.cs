using DonacionSangre.Domain.Dtos;
using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.Interfaces.SqlServerRepository;
using System.Collections;

namespace DonacionSangre.Infrastructure.Services.NotificacionesAutomaticas
{
    public class NotificacionAutomaticaService : INotificacionAutomaticaService
    {
        private readonly ICentroSaludMongoRepository _centroSaludMongoRepository;
        private readonly ISolicitudDonacionMongoRepository _solicitudDonacionMongoRepository;
        private readonly IRepository<UsuarioDonacion> _usuarioDonacionRepository;

        public NotificacionAutomaticaService(ICentroSaludMongoRepository centroSaludMongoRepository,
            ISolicitudDonacionMongoRepository solicitudDonacionMongoRepository, IRepository<UsuarioDonacion> usuarioDonacionRepository)
        {
            _centroSaludMongoRepository = centroSaludMongoRepository;
            _solicitudDonacionMongoRepository = solicitudDonacionMongoRepository;
            _usuarioDonacionRepository = usuarioDonacionRepository;
        }

        public async Task<List<Recipient>> NotificarSolicitudesDonacion()
        {
            List<Recipient> notificaciones = new();
            var usuariosDonacion = await _usuarioDonacionRepository.ListAsync();
            foreach (var usuarioDonacion in usuariosDonacion)
            {
                var solicitudDonacion = await _solicitudDonacionMongoRepository.GetSolicitudDonacionByTipoSangreYMunicipio(usuarioDonacion.MunicipioId, usuarioDonacion.TipoSangre);
                if (solicitudDonacion is not null)
                {
                    notificaciones.Add(new Recipient
                    {
                        EMail = usuarioDonacion.CorreoElectronico,
                        TemplateVars = new Hashtable{
                                { "NOMBRE_DONANTE", usuarioDonacion.PrimerNombre + " " + usuarioDonacion.PrimerApellido },
                                { "CIUDAD", "Popayan" },
                            }
                    });
                }
            }
            return notificaciones;
        }
    }
}
