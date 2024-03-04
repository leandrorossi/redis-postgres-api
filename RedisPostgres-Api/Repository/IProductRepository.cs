using RedisPostgres_Api.Models;

namespace RedisPostgres_Api.Repository
{
    public interface IProductRepository
    {
        public Task<Product> GetProductAsync(int id);

        public Task<IEnumerable<Product>> GetProductsAsync();

        public Task<bool> CreateProductAsync(Product product);

        public Task<bool> UpdateProductAsync(int id, Product product);

        public Task<bool> DeleteProductAsync(int id);
    }
}
