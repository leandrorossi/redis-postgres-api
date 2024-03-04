using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisPostgres_Api.Models;
using RedisPostgres_Api.Repository;

namespace RedisPostgres_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository repository, ICacheRepository cacheRepository) : ControllerBase
    {
        private readonly IProductRepository _repository = repository;
        private readonly ICacheRepository _cacheRepository = cacheRepository;   

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _cacheRepository.GetProductAsync(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                product = await _repository.GetProductAsync(id);                

                if (product == null)
                {
                    return NotFound($"Not found product with id = {id}");
                }

                await _cacheRepository.UpdateProductAsync(product);

                return Ok(product);
            }   
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _cacheRepository.GetProductsAsync();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                products = await _repository.GetProductsAsync();
                await _cacheRepository.UpdateProductsAsync(products);

                return Ok(products);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProductAsync(product);
            await _cacheRepository.UpdateProductAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var result = await _repository.UpdateProductAsync(id, product);            

            if (!result)
            {
                return NotFound($"Not found product with id = {id}");
            }

            await _cacheRepository.UpdateProductAsync(product);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _repository.DeleteProductAsync(id);

            if (!result)
            {
                return NotFound($"Not found product with id = {id}");
            }

            await _cacheRepository.DeleteProductAsync(id);

            return Ok();
        }
    }
}
