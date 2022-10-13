using MongoDB.Driver;
using MongoDB;
using Calculator;

var db = new OperationDb();

namespace CalculatorWebApp.Models
{
    public class CalculatorDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CalculatorDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = client.GetDatabase("Calculator");
        }

        const string MongoDBConnectionString = "mongodb://localhost";

        public IMongoCollection<OperationDb> GetCollections(string collectionName)
        {
            var client = new MongoClient(MongoDBConnectionString);
            var database = client.GetDatabase("Calculator");
            return database.GetCollection<OperationDb>(collectionName);

        }

        private void Insert2Db(OperationDb operationDb, string collectionName)
        {
            var collections = GetCollections(collectionName);
            collections.InsertOne(operationDb);
        }
    }
}

