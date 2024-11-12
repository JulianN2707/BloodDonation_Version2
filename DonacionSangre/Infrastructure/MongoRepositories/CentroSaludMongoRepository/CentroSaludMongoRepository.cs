using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using MongoDB.Bson;
using MongoDB.Driver;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Infrastructure.Repositories.MongoDbRepository
{
    public class CentroSaludMongoRepository : ICentroSaludMongoRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string nombreColeccion = "CentrosSalud";

        public CentroSaludMongoRepository(IConfiguration configuration)
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
   
        public async Task<CentroSalud> GetCentroSaludByIdAsync(Guid id)
        {
            var collection = _database.GetCollection<CentroSalud>(nombreColeccion);
            var filter = Builders<CentroSalud>.Filter.Eq(d => d.CentroSaludId, id);
            var centroSalud = await collection.Find(filter).FirstOrDefaultAsync();
            return centroSalud;
        }
    }
}
