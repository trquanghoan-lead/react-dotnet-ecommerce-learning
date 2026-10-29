namespace Ecommerce.Domain.src.Events.Orders;

public sealed record OrderStatusChanged(
    Guid OrderId,
    string Status,
    DateTime ChangedAt);
