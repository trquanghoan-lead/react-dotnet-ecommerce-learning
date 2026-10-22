using ECommerceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceBackend.Controllers
{
    public class OrderController(IECommerceService _ie) : Controller 
    {
        [Authorize]
        [HttpGet("api/orders/{id}")]
        public IActionResult Index(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var res=_ie.GetOrder(userId, id);
            if (res == null) return NotFound("No Order exist with this id");


                    return Ok(res);
        }
    }
}
