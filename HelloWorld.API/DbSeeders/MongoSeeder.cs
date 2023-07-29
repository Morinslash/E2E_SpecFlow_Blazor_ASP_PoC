using HelloWorld.API.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HelloWorld.API.DbSeeders;

public class MongoSeeder
{
    private readonly IMongoClient _mongoClient;
    private readonly string _databaseName;
    private readonly string _collectionName;

    public MongoSeeder(IMongoClient mongoClient, IConfiguration configuration)
    {
        _mongoClient = mongoClient;
        _databaseName = configuration.GetValue<string>("MongoSettings:DatabaseName");
        _collectionName = configuration.GetValue<string>("MongoSettings:CollectionName");
    }

    public async Task SeedAsync()
    {


        var database = _mongoClient.GetDatabase(_databaseName);
        var collection = database.GetCollection<HelloMessage>(_collectionName);

        if (await collection.CountDocumentsAsync(new BsonDocument()) > 0)
            return;
        
        var seedData = new List<HelloMessage>
        {
            new HelloMessage { Message = "Hello World" },
        };

        await collection.InsertManyAsync(seedData);
    }

    public async Task DropDatabaseAsync()
    {
        await _mongoClient.DropDatabaseAsync(_databaseName);
    }
}