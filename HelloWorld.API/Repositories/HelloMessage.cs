namespace HelloWorld.API.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class HelloMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Message")]
    public string Message { get; set; }
}
