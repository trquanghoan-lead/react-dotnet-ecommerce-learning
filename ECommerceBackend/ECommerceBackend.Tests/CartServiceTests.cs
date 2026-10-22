using Xunit;
using Moq;
using ECommerceBackend.Services;
using ECommerceBackend.Repository;
using ECommerceBackend.Models;
using System.Collections.Generic;

public class CartServiceTests
{
    [Fact]
    public void AddToCart_Should_Throw_When_Stock_Exceeded()
    {
        // Arrange
        var repoMock = new Mock<IECommerceRepository>();
        repoMock.Setup(r => r.GetProducts()).Returns(new List<Product>
        {
            new Product { Id = 1, Name = "Phone", Price = 10000, Stock = 2 }
        });

        var service = new ECommerceService(repoMock.Object);

        var item = new CartItem { ProductId = 1, Quantity = 5 };

        // Act & Assert
        var ex = Assert.Throws<Exception>(() =>
            service.AddOrUpdateCart(0, item)
        );

        Assert.Equal("Requested quantity exceeds stock", ex.Message);
    }
}
