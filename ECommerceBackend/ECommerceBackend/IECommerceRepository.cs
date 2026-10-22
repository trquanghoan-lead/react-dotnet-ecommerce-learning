using ECommerceBackend.Models;

namespace ECommerceBackend.Repository
{
    public interface IECommerceRepository
    {
        List<Product> GetProducts();
        void UpdateStock(int productId, int quantity);
        List<Coupon> GetCoupons();
    }
}
