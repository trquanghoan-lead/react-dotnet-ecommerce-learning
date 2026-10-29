namespace Ecommerce.Infrastructure.src.EventSourcing.Orders;

public sealed class OrderState
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Created";
    public DateTime CreatedAt { get; set; }
    public DateTime? LastStatusChangedAt { get; set; }
    public decimal Total { get; set; }
    public List<OrderLineState> Items { get; set; } = [];
}

public sealed class OrderLineState
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
