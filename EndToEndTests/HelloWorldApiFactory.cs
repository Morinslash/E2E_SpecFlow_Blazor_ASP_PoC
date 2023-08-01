using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EndToEndTests;

public class HelloWorldApiFactory : WebApplicationFactory<HelloWorld.API.ITestApiMarker> 
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // remove existing mongo client
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(MongoClient));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
        
            var connectionString = "mongodb://localhost:27017";
            services.AddScoped<IMongoClient>(_ => new MongoClient(connectionString));
        });
    }
}