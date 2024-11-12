using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DonacionSangre.Infrastructure.Repositories.MongoDbRepository
{
    public class PersonaMongoRepository : IPersonaMongoRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string nombreColeccion = "Personas";

        public PersonaMongoRepository(IConfiguration configuration)
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
        public async Task UpdatePersonaAsync(List<Persona> personas)
        {
            var collection = _database.GetCollection<Persona>(nombreColeccion);
            foreach (var donante in personas)
            {
                var filter = Builders<Persona>.Filter.Eq(d => d.PersonaId, donante.PersonaId);
                await collection.ReplaceOneAsync(filter, donante, new ReplaceOptions { IsUpsert = true });
            }
        }

        public async Task<Persona?> GetPersonaByIdAsync(Guid id)
        {
            var collection = _database.GetCollection<Persona>(nombreColeccion);
            var filter = Builders<Persona>.Filter.Eq(d => d.PersonaId, id);
            var persona = await collection.Find(filter).FirstOrDefaultAsync();
            return persona;
        }

        public async Task<List<Persona>> GetPersonasAsync()
        {
            var collection = _database.GetCollection<Persona>(nombreColeccion);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
