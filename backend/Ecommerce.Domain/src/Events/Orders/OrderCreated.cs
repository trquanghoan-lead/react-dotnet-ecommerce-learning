namespace Ecommerce.Domain.src.Events.Orders;

public sealed record OrderCreated(
    Guid OrderId,
    Guid UserId,
    DateTime CreatedAt);
