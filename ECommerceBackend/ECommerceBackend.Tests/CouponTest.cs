using ECommerceBackend.Models;
using ECommerceBackend.Repository;
using ECommerceBackend.Services;
using Moq;
using Xunit;
using System.Collections.Generic;

public class CouponTests
{
    [Fact]
    public void ApplyCoupon_Should_Set_CouponCode_When_Coupon_Is_Valid()
    {
        // Arrange
        var repoMock = new Mock<IECommerceRepository>();

        repoMock.Setup(r => r.GetCoupons()).Returns(new List<Coupon>
        {
            new Coupon { Code = "FLAT50", Type = "FLAT", Value = 50, MinAmount = 500 }
        });

        var service = new ECommerceService(repoMock.Object);
        int cartId = 0;

        // Cart must exist before applying coupon
        service.AddOrUpdateCart(cartId, new CartItem
        {
            ProductId = 1,
            Quantity = 1
        });

        // Act
        var cart = service.ApplyCoupon(cartId, "FLAT50");

        // Assert
        Assert.NotNull(cart);
        Assert.Equal("FLAT50", cart.CouponCode);
    }
}
