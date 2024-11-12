using DonacionSangre.Domain.Entities;
using DonacionSangre.Domain.Interfaces.MongoRepository;
using MongoDB.Bson;
using MongoDB.Driver;
using static DonacionSangre.Application.Common.Tags;
using EReservaDonacion = DonacionSangre.Domain.Entities.ReservaDonacion;

namespace DonacionSangre.Infrastructure.MongoRepositories.ReservaDonacionMongoRepository
{
    public class ReservaDonacionMongoRepository : IReservaDonacionMongoRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string nombreColeccion = "ReservasDonacion";

        public ReservaDonacionMongoRepository(IConfiguration configuration)
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

        public async Task UpdateReservasDonacionAsync(List<EReservaDonacion> reservasDonacion)
        {
            var collection = _database.GetCollection<EReservaDonacion>("ReservasDonacion");
            foreach (var reservaDonacion in reservasDonacion)
            {
                var filter = Builders<EReservaDonacion>.Filter.Eq(d => d.ReservaDonacionId, reservaDonacion.ReservaDonacionId);
                await collection.ReplaceOneAsync(filter, reservaDonacion, new ReplaceOptions { IsUpsert = true });
            }
        }
    }
}
