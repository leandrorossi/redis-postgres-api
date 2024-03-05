using BC = BCrypt.Net.BCrypt;

namespace RedisPostgres_Api.Auth
{
    public class PasswordHaser
    {
        private static readonly int _workFactor = 10;

        public static string HashPassword(string password)
        {
            return BC.HashPassword(password, _workFactor);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (BC.Verify(providedPassword, hashedPassword))
                return true;
            else
                return false;
        }
    }
}
