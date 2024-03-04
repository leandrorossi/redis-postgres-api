using Dapper;
using Npgsql;
using RedisPostgres_Api.Models;

namespace RedisPostgres_Api.Repository
{
    public class ProductRepository(IConfiguration configuration) : IProductRepository
    {
        private readonly IConfiguration _configuration = configuration;

        private NpgsqlConnection _connection
        {
            get => new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);
        }
      
        public async Task<Product> GetProductAsync(int id)
        {
            await using var connection = _connection;

            var product = await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE Id = @Id", new { Id = id });

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            await using var connection = _connection;

            var products = await connection.QueryAsync<Product>("SELECT * FROM Product");

            return products;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await using var connection = _connection;

            var rowAffected = await connection.ExecuteAsync(
                "INSERT INTO Product (ProductName, Description, Price, Amount) VALUES (@ProductName, @Description, @Price, @Amount)",
                new
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Amount = product.Amount
                });

            return rowAffected != 0;
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            await using var connection = _connection;

            var rowAffected = await connection.ExecuteAsync(
                "UPDATE Product SET ProductName = @ProductName, Description = @Description, Price = @Price, Amount = @Amount WHERE Id = @Id",
                new
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Amount = product.Amount,
                    Id = id
                });

            return rowAffected != 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            await using var connection = _connection;

            var rowAffected = await connection.ExecuteAsync(
                "DELETE FROM Product WHERE Id = @Id", new { Id = id });

            return rowAffected != 0;
        }
    }
}
