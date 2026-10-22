using ECommerceBackend.Models;
using ECommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    AccountService accounts,
    JwtTokenService tokens,
    IECommerceService ecommerceService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        var account = accounts.ValidateCredentials(request.Username, request.Password);
        if (account is null)
            return Unauthorized(new { message = "Username hoặc password không đúng." });

        var cartId = ecommerceService.GetOrCreateCartId(account.UserId);
        return Ok(tokens.CreateToken(account, cartId));
    }
}
