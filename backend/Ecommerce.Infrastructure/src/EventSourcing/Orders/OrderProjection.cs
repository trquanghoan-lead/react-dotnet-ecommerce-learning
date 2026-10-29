using Ecommerce.Domain.src.Events.Orders;
using Marten.Events.Aggregation;

namespace Ecommerce.Infrastructure.src.EventSourcing.Orders;

public sealed class OrderProjection : SingleStreamProjection<OrderState>
{
    public static OrderState Create(OrderCreated created)
    {
        return new OrderState
        {
            Id = created.OrderId,
            UserId = created.UserId,
            CreatedAt = created.CreatedAt,
            Status = "Created"
        };
    }

    public static void Apply(OrderItemAdded item, OrderState state)
    {
        state.Items.Add(new OrderLineState
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        });

        state.Total += item.Quantity * item.UnitPrice;
    }

    public static void Apply(OrderStatusChanged changed, OrderState state)
    {
        state.Status = changed.Status;
        state.LastStatusChangedAt = changed.ChangedAt;
    }
}
