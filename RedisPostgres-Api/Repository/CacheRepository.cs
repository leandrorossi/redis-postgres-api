using Microsoft.Extensions.Caching.Distributed;
using RedisPostgres_Api.Models;
using System.Text.Json;

namespace RedisPostgres_Api.Repository
{
    public class CacheRepository(IDistributedCache cache) : ICacheRepository
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _cache.GetAsync(id.ToString());

            if (product == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Product>(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _cache.GetAsync("Products");

            if (products == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<IEnumerable<Product>>(products);
        }       

        public async Task UpdateProductAsync(Product product)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            await _cache.SetStringAsync(product.Id.ToString(), JsonSerializer.Serialize(product), options);
        }

        public async Task UpdateProductsAsync(IEnumerable<Product> products)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            await _cache.SetStringAsync("Products", JsonSerializer.Serialize(products), options);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _cache.RemoveAsync(id.ToString());
        }
    }
}
