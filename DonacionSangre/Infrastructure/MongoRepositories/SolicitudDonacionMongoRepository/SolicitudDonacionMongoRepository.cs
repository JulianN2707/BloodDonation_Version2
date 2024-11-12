using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using DonacionSangre.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DonacionSangre.Infrastructure.MongoRepositories.SolicitudDonacionMongoRepository
{
    public class SolicitudDonacionMongoRepository : ISolicitudDonacionMongoRepository
    {
        private readonly IMongoDatabase _database;

        public SolicitudDonacionMongoRepository(IConfiguration configuration)
        {
            var connectionUri = configuration.GetConnectionString("MongoDbConnection");
            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);

            try
            {
                // Verifica la conexión enviando un ping al servidor
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
                throw;
            }
            _database = client.GetDatabase(configuration["MongoDbDatabaseName"]);
        }

        public async Task<SolicitudDonacion> GetSolicitudDonacionByTipoSangreYMunicipio(Guid municipioId, TipoSangre tipoSangre)
        {
            var collection = _database.GetCollection<SolicitudDonacion>("SolicitudDonacion");

            var filter = Builders<SolicitudDonacion>.Filter.And(
                Builders<SolicitudDonacion>.Filter.Eq(d => d.TipoSangre, tipoSangre),
                Builders<SolicitudDonacion>.Filter.Eq(d => d.CentroSalud.MunicipioId, municipioId)
            );

            // Buscar la primera solicitud que coincida con los filtros
            var solicitudDonacion = await collection.Find(filter).FirstOrDefaultAsync();

            return solicitudDonacion;
        }

        public async Task<SolicitudDonacion> GetSolicitudesDonacionByIdAsync(Guid id)
        {
            var collection = _database.GetCollection<SolicitudDonacion>("SolicitudDonacion");
            var filter = Builders<SolicitudDonacion>.Filter.Eq(d => d.SolicitudDonacionId, id);
            var solicitudDonacion = await collection.Find(filter).FirstOrDefaultAsync();
            return solicitudDonacion;
        }

        public async Task UpdateSolicitudesDonacionAsync(List<SolicitudDonacion> solicitudesDonacion)
        {
            var collection = _database.GetCollection<SolicitudDonacion>("SolicitudDonacion");
            foreach (var solicitudDonacion in solicitudesDonacion)
            {
                var filter = Builders<SolicitudDonacion>.Filter.Eq(d => d.SolicitudDonacionId, solicitudDonacion.SolicitudDonacionId);
                await collection.ReplaceOneAsync(filter, solicitudDonacion, new ReplaceOptions { IsUpsert = true });
            }
        }
    }
}
