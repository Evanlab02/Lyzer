using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lyzer_BE.Database
{
    public class MongoController<T>
    {
        private String _collectionName;
        private MongoClient dbClient;
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;


        public MongoController(string databaseName, string collectionName)
        {
            CreateMongoDBClient();
            _database = dbClient.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(collectionName);
            _collectionName = collectionName;
        }

        private void CreateMongoDBClient()
        {
            string connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            dbClient = new MongoClient(settings);


            // Send a ping to confirm a successful connection
            try
            {
                var result = dbClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Debug.WriteLine("LyzerDB: Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LyzerDB: {ex}");
            }
        }

        public void SetCollection(string collectionName)
        {
            _collection = _database.GetCollection<T>(collectionName);
            _collectionName = collectionName;
        }

        public void CreateCollection([Optional] string collectionName)
        {
            var collection = _collectionName;
            if (!string.IsNullOrEmpty(collectionName))
            {
                collection = collectionName;
            }
            _database.CreateCollection(collection);
        }

        public void DestroyCollection([Optional] string collectionName)
        {
            var collection = _collectionName;
            if (!string.IsNullOrEmpty(collectionName))
            {
                collection = collectionName;
            }
            _database.DropCollection(collection);
        }

        public bool CollectionExists([Optional] string collectionName)
        {
            var filter = new BsonDocument("name", _collectionName);

            if (!string.IsNullOrEmpty(collectionName))
            {
                filter = new BsonDocument("name", collectionName);
            }

            var options = new ListCollectionNamesOptions { Filter = filter };
            return _database.ListCollectionNames(options).Any();
        }

        public void InsertManyIntoCollection(List<T> documents)
        {
            try
            {
                _collection.InsertMany(documents);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LyzerDB: {ex}");
            }
        }

        public void DeleteManyFromCollection(FilterDefinition<T> filter)
        {
            try
            {
                _collection.DeleteMany(filter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LyzerDB: {ex}");
            }
        }

        public async Task<List<T>> FindManyFromCollection(FilterDefinition<T> filter)
        {
            List<T> result = new List<T>();
            try
            {
                result = await _collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LyzerDB: {ex}");
            }

            return result;
        }
    }
}
