using Dapper;
using Npgsql;
using RedisPostgres_Api.Auth;
using RedisPostgres_Api.Models;

namespace RedisPostgres_Api.Repository
{
    public class UserRepository(IConfiguration configuration) : IUserRepository
    {
        private readonly IConfiguration _configuration = configuration;

        private NpgsqlConnection _connection
        {
            get => new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);
        }

        public async Task<User> GetUserAsync(string name, string password)
        {
            await using var connection = _connection;

            var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Account WHERE Name = @Name", new { Name = name });

            if (user == null)
            {
                return null;
            }

            var resultHashPassword = PasswordHaser.VerifyHashedPassword(user.Password, password);

            if (resultHashPassword)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await using var connection = _connection;

            var passwordHashed = PasswordHaser.HashPassword(user.Password);

            var rowAffected = await connection.ExecuteAsync("INSERT INTO Account (Name, Password) VALUES (@Name, @Password)",
                new
                {
                    Name = user.Name,
                    Password = passwordHashed,
                });

            return rowAffected != 0;
        }
    }
}
