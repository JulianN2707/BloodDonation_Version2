using DonacionSangre.Domain.Interfaces.MongoRepository;
using MongoDB.Bson;
using MongoDB.Driver;
using EDonante = DonacionSangre.Domain.Entities.Test.Donante;

namespace DonacionSangre.Infrastructure.Repositories.MongoDbRepository
{
    public class DonanteMongoRepository : IDonanteMongoRepository
    {
        private readonly IMongoDatabase _database;

        public DonanteMongoRepository(IConfiguration configuration)
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
        public async Task UpdateDonantesAsync(List<EDonante> donantes)
        {
            var collection = _database.GetCollection<EDonante>("Donantes");
            foreach (var donante in donantes)
            {
                var filter = Builders<EDonante>.Filter.Eq(d => d.Id, donante.Id);
                await collection.ReplaceOneAsync(filter, donante, new ReplaceOptions { IsUpsert = true });
            }
        }

        public async Task<EDonante> GetDonanteByIdAsync(Guid id)
        {
            var collection = _database.GetCollection<EDonante>("Donantes");
            var filter = Builders<EDonante>.Filter.Eq(d => d.Id, id);
            var donante = await collection.Find(filter).FirstOrDefaultAsync();
            return donante;
        }
    }
}
