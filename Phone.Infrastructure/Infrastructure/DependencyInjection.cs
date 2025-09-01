using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Phone.Domain.Interfaces;
using Phone.Infrastructure.Configuration;
using Phone.Infrastructure.Repositories;

namespace Phone.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection s, IConfiguration c)
    {
        var section = c.GetSection("MongoDbSettings");
        var set = new MongoDbSettings
        {
            ConnectionString = section["ConnectionString"],
            DatabaseName = section["DatabaseName"]
        };
        s.AddSingleton<IMongoClient>(_ => new MongoClient(set.ConnectionString));
        s.AddScoped<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>()
                                            .GetDatabase(set.DatabaseName));
        s.AddScoped<IContactRepository, ContactRepository>();
        return s;
    }
}
