using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using HelloWorld.API.DbSeeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EndToEndTests;

public class HelloWorldApiFactory : WebApplicationFactory<HelloWorld.API.ITestApiMarker>
{
    private readonly IContainer _mongoContainer = new ContainerBuilder()
        .WithImage("mongo:latest")
        .WithPortBinding(27017, 27017)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
        .Build();

    private MongoSeeder _seeder;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
            {
                _mongoContainer.StartAsync().Wait();
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(MongoClient));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var connectionString = "mongodb://localhost:27017";
                services.AddScoped<IMongoClient>(_ => new MongoClient(connectionString));
            })
            .ConfigureTestServices(services =>
            {
                services.AddScoped<MongoSeeder>();
            });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _mongoContainer.StopAsync().Wait();
        }
    }
}