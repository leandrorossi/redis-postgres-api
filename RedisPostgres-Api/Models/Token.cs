namespace RedisPostgres_Api.Models
{
    public class Token
    {
        public string token { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}
