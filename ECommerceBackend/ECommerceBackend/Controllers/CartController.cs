using ECommerceBackend.Models;
using ECommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IECommerceService _service;
    public CartController(IECommerceService service) => _service = service;

    [HttpPost("items")]
    public IActionResult AddItem([FromQuery] int? cartId, [FromBody] AddCartItemRequest request)
    {
        // The frontend sends cartId in the JSON body. Keep query-string support
        // as a fallback for existing clients.
        var effectiveCartId = cartId ?? request.CartId;
        if (effectiveCartId <= 0)
            return BadRequest("A valid cartId is required.");

        var item = new CartItem
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        return Ok(_service.AddOrUpdateCart(UserId(), effectiveCartId, item));
    }

    [HttpPost("{cartId}/apply-coupon")]
    public IActionResult ApplyCoupon(int cartId, string code)
        => Ok(_service.ApplyCoupon(UserId(), cartId, code));

    [HttpPost("{cartId}/checkout")]
    public IActionResult Checkout(int cartId)
        => Ok(_service.Checkout(UserId(), cartId));
    [HttpGet("{id}")]
    public IActionResult CartDetails(int id)
    {
        var res=_service.GetCart(UserId(), id);
        if (res == null)
        {
            // A cart that does not exist, or is not owned by the caller, is not
            // a malformed request. Keep ownership failures indistinguishable
            // from missing carts and return the standard resource status.
            return NotFound("Cart not found");
        }
        return Ok(res);
    }

    private int UserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
