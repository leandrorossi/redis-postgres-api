using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RedisPostgres_Api.Auth;
using RedisPostgres_Api.Models;
using RedisPostgres_Api.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RedisPostgres_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController(IUserRepository repository) : ControllerBase
    {
        private readonly IUserRepository _repository = repository;

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] User user)
        {
            var User = await _repository.GetUserAsync(user.Name, user.Password);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(TokenGenerator.Generate(user.Name));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _repository.CreateUserAsync(user);

            return Created("", user);
        }
    }
}
