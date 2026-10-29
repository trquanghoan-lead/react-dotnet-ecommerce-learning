namespace Ecommerce.Domain.src.Events.Orders;

public sealed record OrderItemAdded(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);
