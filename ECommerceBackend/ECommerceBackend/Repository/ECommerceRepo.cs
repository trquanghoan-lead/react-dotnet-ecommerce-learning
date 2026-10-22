using ECommerceBackend.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace ECommerceBackend.Repository
{
    public class ECommerceRepo : IECommerceRepository
    {
        
        private readonly string _productsPath = Path.Combine(Directory.GetCurrentDirectory(), "Database/products.json");
        private readonly string _couponsPath = Path.Combine(Directory.GetCurrentDirectory(), "Database/coupons.json");
        private static readonly object _lock = new();

        public List<Product> GetProducts()
        {
            var json = File.ReadAllText(_productsPath);
            return JsonSerializer.Deserialize<List<Product>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                        ) ?? new List<Product>();

        }

        public List<Coupon> GetCoupons()
        {
            var json = File.ReadAllText(_couponsPath);
            
            return JsonSerializer.Deserialize<List<Coupon>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                        ) ?? new List<Coupon>();
        }

        public void UpdateStock(int productId, int quantity)
        {
            lock (_lock)
            {
                var products = GetProducts();
                var product = products.First(p => p.Id == productId);

                if (product.Stock < quantity)
                    throw new Exception("Insufficient stock");

                product.Stock -= quantity;
                File.WriteAllText(_productsPath, JsonSerializer.Serialize(products));
            }
        }
    }
}
