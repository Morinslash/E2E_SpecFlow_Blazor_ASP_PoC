using MongoDB.Driver;

namespace HelloWorld.API.Repositories;

public class MongoRepository : IDatabaseRepository
{
    private readonly IMongoCollection<HelloMessage> _collection;
    public MongoRepository(IMongoClient client, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("MongoSettings:DatabaseName");
        var collectionName = configuration.GetValue<string>("MongoSettings:CollectionName");
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<HelloMessage>(collectionName);
    }
    public string GetHello()
    {
        var filter = Builders<HelloMessage>.Filter.Empty;
        var helloMessage = _collection.Find(filter).FirstOrDefault();

        return helloMessage.Message;
    }
}