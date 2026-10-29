using Ecommerce.Domain.src.Events.Orders;
using Ecommerce.Infrastructure.src.EventSourcing.Orders;
using Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Infrastructure.src.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/order-events")]
public sealed class OrderEventsController : ControllerBase
{
    private readonly IDocumentStore _store;

    public OrderEventsController(IDocumentStore store)
    {
        _store = store;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderEventRequest request,
        CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        var created = new OrderCreated(orderId, request.UserId, DateTime.UtcNow);

        await using var session = _store.LightweightSession();
        session.Events.StartStream(orderId, created);
        await session.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetState), new { orderId }, new { orderId });
    }

    [HttpPost("{orderId:guid}/items")]
    public async Task<IActionResult> AddItem(
        Guid orderId,
        [FromBody] AddOrderItemRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, new OrderItemAdded(
            orderId,
            request.ProductId,
            request.Quantity,
            request.UnitPrice));
        await session.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpPost("{orderId:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid orderId,
        [FromBody] ChangeOrderStatusRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Status))
        {
            return BadRequest("Status is required.");
        }

        await using var session = _store.LightweightSession();
        session.Events.Append(orderId, new OrderStatusChanged(
            orderId,
            request.Status,
            DateTime.UtcNow));
        await session.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpGet("{orderId:guid}/history")]
    public async Task<IActionResult> GetHistory(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        await using var session = _store.LightweightSession();
        var events = await session.Events.FetchStreamAsync(
            orderId,
            token: cancellationToken);

        return Ok(events);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetState(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        await using var session = _store.QuerySession();
        var state = await session.LoadAsync<OrderState>(orderId, cancellationToken);

        return state is null ? NotFound() : Ok(state);
    }
}

public sealed record CreateOrderEventRequest(Guid UserId);

public sealed record AddOrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);

public sealed record ChangeOrderStatusRequest(string Status);
