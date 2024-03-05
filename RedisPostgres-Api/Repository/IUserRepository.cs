using RedisPostgres_Api.Models;

namespace RedisPostgres_Api.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(string login, string password);

        public Task<bool> CreateUserAsync(User user); 
    }
}
