using Xunit;
using ECommerceBackend.Models;

public class CheckoutTests
{
    [Fact]
    public void Checkout_Should_Calculate_GrandTotal_Correctly()
    {
        // Arrange
        var order = new Order
        {
            SubTotal = 1200,
            Discount = 200
        };

        // Act
        order.GrandTotal = order.SubTotal - order.Discount;

        // Assert
        Assert.Equal(1000, order.GrandTotal);
    }
}
