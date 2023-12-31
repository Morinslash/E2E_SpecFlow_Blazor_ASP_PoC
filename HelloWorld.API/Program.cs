using HelloWorld.API.DbSeeders;
using HelloWorld.API.Repositories;
using MongoDB.Driver;

namespace HelloWorld.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<IMongoClient>(client =>
        {
            var configuration = client.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("MongoDB");
            return new MongoClient(connectionString);
        });
        builder.Services.AddScoped<IDatabaseRepository,MongoRepository>();

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors("AllowAll");
        app.Run();
    }
}