using MongoDB.Driver;

namespace HelloWorld.API.Repositories;

public class MongoRepository : IDatabaseRepository
{
    private readonly IMongoCollection<HelloMessage> _collection;
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseName = "HelloDatabase";
    private const string CollectionName = "HelloMessages";
    public MongoRepository()
    {
        var client = new MongoClient(ConnectionString);
        var database = client.GetDatabase(DatabaseName);
        _collection = database.GetCollection<HelloMessage>(CollectionName);
    }
    public string GetHello()
    {
        var filter = Builders<HelloMessage>.Filter.Empty;
        var helloMessage = _collection.Find(filter).FirstOrDefault();

        return helloMessage.Message;
    }
}