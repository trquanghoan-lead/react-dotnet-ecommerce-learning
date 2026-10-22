using ECommerceBackend.Models;
using ECommerceBackend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Services
{
    public class ECommerceService : IECommerceService
    {
        private static readonly Dictionary<int, Cart> _carts = new();
        private static readonly Dictionary<int, Order> _orders = new();
        private static readonly object _cartLock = new();

        private readonly IECommerceRepository _repo;

        public ECommerceService(IECommerceRepository repo)
        {
            _repo = repo;
        }

        public List<Product> GetProducts() => _repo.GetProducts();

        public int GetOrCreateCartId(int userId)
        {
            lock (_cartLock)
            {
                var existingCart = _carts.Values.FirstOrDefault(cart => cart.UserId == userId);
                if (existingCart is not null)
                    return existingCart.CartId;

                var cartId = _carts.Keys.DefaultIfEmpty(0).Max() + 1;
                _carts[cartId] = new Cart
                {
                    CartId = cartId,
                    UserId = userId
                };

                return cartId;
            }
        }

        public Cart AddOrUpdateCart(int userId, int cartId, CartItem item)
        {
            if (item.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            var products = _repo.GetProducts();
            var product = products.First(p => p.Id == item.ProductId);

            var existingItem = _carts.TryGetValue(cartId, out var existingCart)
                ? existingCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId)
                : null;

            var totalQuantity = (existingItem?.Quantity ?? 0) + item.Quantity;
            if (totalQuantity > product.Stock)
                throw new Exception("Requested quantity exceeds stock");

            if (!_carts.ContainsKey(cartId))
                _carts[cartId] = new Cart { CartId = cartId, UserId = userId };

            var cart = _carts[cartId];
            if (cart.UserId != userId)
                throw new UnauthorizedAccessException("Cart does not belong to the authenticated user.");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (cartItem is null)
                cart.Items.Add(item);
            else
                cartItem.Quantity += item.Quantity;

            return cart;
        }

        public Cart AddOrUpdateCart(int cartId, CartItem item)
        {
            if (!(_repo.GetProducts()?.Any() ?? false))
            {
                if (!_carts.ContainsKey(cartId))
                    _carts[cartId] = new Cart { CartId = cartId, UserId = 0 };

                _carts[cartId].Items.RemoveAll(i => i.ProductId == item.ProductId);
                _carts[cartId].Items.Add(item);
                return _carts[cartId];
            }

            return AddOrUpdateCart(0, cartId, item);
        }

        public Cart ApplyCoupon(int userId, int cartId, string code)
        {
            var cart = _carts[cartId];
            if (cart.UserId != userId)
                throw new UnauthorizedAccessException("Cart does not belong to the authenticated user.");
            var coupon = _repo.GetCoupons().FirstOrDefault(c => c.Code == code);

            if (coupon == null)
                throw new Exception("Invalid coupon");

            cart.CouponCode = code;
            return cart;
        }

        public Cart ApplyCoupon(int cartId, string code)
            => ApplyCoupon(0, cartId, code);

        public Order Checkout(int userId, int cartId)
        {
            var cart = _carts[cartId];
            if (cart.UserId != userId)
                throw new UnauthorizedAccessException("Cart does not belong to the authenticated user.");
            var products = _repo.GetProducts();

            decimal subtotal = 0;

            foreach (var item in cart.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                if (product.Stock < item.Quantity)
                    throw new Exception("Stock changed. Checkout failed.");

                subtotal += product.Price * item.Quantity;
            }

            decimal discount = 0;
            if (cart.CouponCode != null)
            {
                var coupon = _repo.GetCoupons().First(c => c.Code == cart.CouponCode);
                if (subtotal >= coupon.MinAmount)
                {
                    discount = coupon.Type == "FLAT"
                        ? coupon.Value
                        : Math.Min(subtotal * coupon.Value / 100, coupon.MaxDiscount);
                }
            }

            foreach (var item in cart.Items)
                _repo.UpdateStock(item.ProductId, item.Quantity);

            var order = new Order
            {
                UserId = userId,
                Items = cart.Items,
                SubTotal = subtotal,
                Discount = discount,
                GrandTotal = subtotal - discount
            };

            _orders[order.OrderId] = order;
            return order;
        }
        public Cart GetCart(int userId, int id)
        {
            if (!_carts.ContainsKey(id) || _carts[id].UserId != userId)
            {
                return null;
            }

            return _carts[id];
        }


        public Order GetOrder(int userId, int orderId)
        {
            if (!_orders.ContainsKey(orderId) || _orders[orderId].UserId != userId)
            {
                return null;
            }
            return _orders[orderId];
        }
    }
}
