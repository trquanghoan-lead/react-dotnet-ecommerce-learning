using ECommerceBackend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IECommerceService _service;
    public ProductsController(IECommerceService service) => _service = service;

    [HttpGet]
    public IActionResult Get() => Ok(_service.GetProducts());
}
