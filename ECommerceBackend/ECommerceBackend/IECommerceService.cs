using ECommerceBackend.Models;

namespace ECommerceBackend.Services
{
    public interface IECommerceService
    {
        List<Product> GetProducts();
        int GetOrCreateCartId(int userId);
        Cart AddOrUpdateCart(int userId, int cartId, CartItem item);
        Cart ApplyCoupon(int userId, int cartId, string code);
        Order Checkout(int userId, int cartId);
        Order GetOrder(int userId, int orderId);
        Cart GetCart(int userId, int cartId);
    }
}
