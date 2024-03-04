namespace RedisPostgres_Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public double? Price { get; set; }
        public int? Amount { get; set; }
    }
}
