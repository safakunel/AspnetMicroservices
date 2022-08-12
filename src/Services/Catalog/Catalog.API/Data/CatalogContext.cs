using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Product> _products;
        
        public CatalogContext(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            _database = _client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            _products = _database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(_products);
        }
        public IMongoCollection<Product> Products 
        { 
            get
            {
                return _products;
            }
        }
    }
}
