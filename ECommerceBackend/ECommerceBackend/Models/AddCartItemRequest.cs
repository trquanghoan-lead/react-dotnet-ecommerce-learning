namespace ECommerceBackend.Models;

public sealed class AddCartItemRequest
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}