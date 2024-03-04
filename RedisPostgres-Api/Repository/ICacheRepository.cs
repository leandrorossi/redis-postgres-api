using RedisPostgres_Api.Models;

namespace RedisPostgres_Api.Repository
{
    public interface ICacheRepository
    {
        public Task<Product> GetProductAsync(int id);

        public Task<IEnumerable<Product>> GetProductsAsync();

        public Task UpdateProductAsync(Product product);

        public Task UpdateProductsAsync(IEnumerable<Product> product);

        public Task DeleteProductAsync(int id);
    }
}
