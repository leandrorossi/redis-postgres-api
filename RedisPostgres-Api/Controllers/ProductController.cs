using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisPostgres_Api.Models;
using RedisPostgres_Api.Repository;

namespace RedisPostgres_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository repository) : ControllerBase
    {
        private readonly IProductRepository _repository = repository;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound($"Not found product with id = {id}");
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _repository.GetProductsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProductAsync(product);

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

            return Ok();
        }
    }
}
